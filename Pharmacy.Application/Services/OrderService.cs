using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Mappers;
using Pharmacy.Application.Utilities;
using Pharmacy.Domain.Specifications;
using Pharmacy.Application.Queries;

namespace Pharmacy.Application.Services;

public class OrderService : IOrderService
{
    private readonly IRepositoryManager _manager;
    private readonly UserManager<User> _userManager;
    private readonly ICurrentLoggedInUser _currentLoggedInUser;

    public OrderService(IRepositoryManager repoManager, UserManager<User> userManager, ICurrentLoggedInUser currentLoggedInUser) =>
        (_manager, _userManager, _currentLoggedInUser) = (repoManager, userManager, currentLoggedInUser);

    public async Task<Result<IEnumerable<OrderDTO>>> GetAll()
    {
        User user = await _currentLoggedInUser.GetUser();
        return await _userManager.IsInRoleAsync(user, Roles.Employee) switch
        {
            true => Result.Success(await _manager.Orders.GetAll(new OrderWithUserSpecification())),
            false => Result.Success(await _manager.Orders.GetAll(new UserOrdersSpecification(user.Id)))
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
        Customer? customer = await _manager.Customers.GetById(orderDTO.CustomerId);
        User user = await _currentLoggedInUser.GetUser();

        Result validItems = await InternalEventHandler.ValidateOrderItems(_manager, orderDTO.Items);
        if (!validItems.Succeeded) return Result.Fail<OrderDTO>(validItems.Response);

        Order order = await _manager.Orders.Add(orderDTO.ToModel(user.Id));
        if(customer is not null)
        {
            customer.Dept += order.TotalPrice - order.Paid;
            _manager.Customers.Update(customer);
        }

        await _manager.Save();
        await InternalEventHandler.OrderPostSave(_manager, order, orderDTO);

        await _manager.Save();
        return Result.Success
        (
            order.ToDTO(customer?.Name, user.GetFullName()),
            StatusCodes.Status201Created
        );
    }

    public async Task<Result<OrderDTO>> Update(Guid id, OrderUpdateDTO orderDTO)
    {
        Order? order = await _manager.Orders.GetById(id);
        if(order is null) return Result.Fail<OrderDTO>(AppResponses.NotFoundResponse(id, nameof(Order)));

        Result validItems = await InternalEventHandler.ValidateOrderItems(_manager, orderDTO.Items);
        if (!validItems.Succeeded) return (Result<OrderDTO>)validItems;

        Customer? customer = await _manager.Customers.GetById(order.CustomerId);
        if(customer is not null)
        {
            customer.Dept -= order.TotalPrice - order.Paid;
            _manager.Customers.Update(customer);
        }

        await InternalEventHandler.OrderPreUpdate(_manager, order, orderDTO);

        if(customer is not null)
        {
            customer.Dept += order.TotalPrice - order.Paid;
            _manager.Customers.Update(customer);
        }

        await _manager.Save();
        User user = await _currentLoggedInUser.GetUser();
        return Result.Success
        (
            order.ToDTO(customer?.Name, user.GetFullName()),
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
