using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Mappers;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Queries;
using Microsoft.Extensions.Configuration;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Services;

public class OrderService : IOrderService
{
    private readonly IRepositoryManager _manager;
    private readonly UserManager<User> _userManager;
    private readonly string _defaultCustomerName;
    private readonly ICurrentLoggedInUser _currentLoggedInUser;

    public OrderService(IRepositoryManager repoManager, UserManager<User> userManager, ICurrentLoggedInUser currentLoggedInUser, IConfiguration configuration)
    {
        _manager = repoManager;
        _userManager = userManager;
        _currentLoggedInUser = currentLoggedInUser;
        _defaultCustomerName = configuration.GetSection("DefaultCustomerName").Value ?? string.Empty;

    }

    public async Task<Result<IEnumerable<OrderDTO>>> GetAll(DateOnly? from, DateOnly? to)
    {
        User user = await _currentLoggedInUser.GetUser();
        DateOnly todaysDate = DateOnly.FromDateTime(DateTime.Today.Date);
        return await _userManager.IsInRoleAsync(user, Roles.Employee) switch
        {
            true => Result.Success(
                await _manager.Orders.GetAll(new UserOrdersSpecification(user.Id, from ?? todaysDate, to ?? todaysDate))
            ),
            false => Result.Success(
                await _manager.Orders.GetAll(
                    new OrderWithUserSpecification(from ?? new DateOnly(todaysDate.Year, todaysDate.Month, 1), to ?? todaysDate)
                )
            )
        };
    }

    public async Task<Result<OrderDTO>> GetById(Guid id)
    {
        OrderDTO? order = await _manager.Orders.GetOne(new OrderWithUserSpecification(id));
        return order switch
        {
            null => Result.Fail<OrderDTO>(AppResponses.NotFoundResponse(id, nameof(Order))),
            _ => Result.Success(order)
        };
    }

    public async Task<Result<IEnumerable<OrderItemDTO>>> GetItems(Guid id)
    {
        Order? order = await _manager.Orders.GetById(id);
        if(order is null) return Result.Fail<IEnumerable<OrderItemDTO>>(AppResponses.NotFoundResponse(id, nameof(Order)));

        IEnumerable<OrderItem> items = await _manager.OrderItems.GetAll(
            new OrderItemWithProductSpecification(order.Id)
        );
        return Result.Success(items.ConvertAll(OrderItemMapper.ToDTO));
    }

    public async Task<Result<OrderDTO>> Create(OrderCreateDTO orderDTO)
    {
        Customer? customer;
        if(orderDTO.CustomerId is not null)
        {
            customer = await _manager.Customers.GetById(orderDTO.CustomerId);
            if(customer is null) return Result.Fail<OrderDTO>(AppResponses.NotFoundResponse(orderDTO.CustomerId, nameof(Customer)));
        }
        else customer = await _manager.Customers.GetOne(new Specification<Customer>(c => c.Name == _defaultCustomerName));
        User user = await _currentLoggedInUser.GetUser();

        Result validItems = await InternalEventHandler.ValidateOrderItems(_manager, orderDTO.Items);
        if (!validItems.Succeeded) return Result.Fail<OrderDTO>(validItems.Response);

        Order order = await _manager.Orders.Add(orderDTO.ToModel(user.Id));

        await InternalEventHandler.OrderPreSave(_manager, order, orderDTO);

        customer!.Dept += order.TotalPrice - order.Paid;
        _manager.Customers.Update(customer);
        await _manager.Customers.Save();

        return Result.Success
        (
            order.ToDTO(orderDTO.CustomerId is null ? null : customer.Name, customer.Id , user.GetFullName()),
            StatusCodes.Status201Created
        );
    }

    public async Task<Result<OrderDTO>> Update(Guid id, OrderUpdateDTO orderDTO)
    {
        Order? order = await _manager.Orders.GetById(id);
        if(order is null) return Result.Fail<OrderDTO>(AppResponses.NotFoundResponse(id, nameof(Order)));

        Customer? customer = await _manager.Customers.GetById(order.CustomerId);
        if(customer is null) customer = await _manager.Customers.GetOne(new Specification<Customer>(c => c.Name == _defaultCustomerName));

        customer!.Dept -= order.TotalPrice - order.Paid;
        _manager.Customers.Update(customer);

        Result validItems = await InternalEventHandler.ValidateOrderItems(_manager, orderDTO.Items);
        if (!validItems.Succeeded) return Result.Fail<OrderDTO>(validItems.Response);

        await InternalEventHandler.OrderPreUpdate(_manager, order, orderDTO);

        customer.Dept += order.TotalPrice - order.Paid;
        _manager.Customers.Update(customer);
        await _manager.Customers.Save();

        User user = await _currentLoggedInUser.GetUser();
        return Result.Success
        (
            order.ToDTO(customer?.Name, customer!.Id, user.GetFullName()),
            StatusCodes.Status201Created
        );
    }

    public async Task<Result> Delete(Guid id)
    {
        Order? order = await _manager.Orders.GetById(id);
        if(order is null) return Result.Fail(AppResponses.NotFoundResponse(id, nameof(Order)));

        await InternalEventHandler.OrderPreDelete(_manager, order);
        _manager.Orders.Delete(order);

        await _manager.Save();
        return Result.Success(StatusCodes.Status204NoContent);
    }
}
