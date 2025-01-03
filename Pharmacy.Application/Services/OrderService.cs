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

namespace Pharmacy.Application.Services;

public class OrderService : IOrderService
{
    private readonly IRepositoryManager _manager;
    private readonly UserManager<User> _userManager;
    private readonly ICurrentLoggedInUser _currentLoggedInUser;

    public OrderService(IRepositoryManager repoManager, UserManager<User> userManager, ICurrentLoggedInUser currentLoggedInUser) =>
        (_manager, _userManager, _currentLoggedInUser) = (repoManager, userManager, currentLoggedInUser);

    public async Task<Result<IEnumerable<OrderDTO>>> GetAll() =>
        Result.Success(
            (await _manager.Orders.GetAll())
            .ConvertAll(OrderMapper.ToDTO)
        );

    public async Task<Result<OrderDTO>> GetById(Guid id)
    {
        Order? order = await _manager.Orders.GetById(id);
        return order switch
        {
            null => Result.Fail<OrderDTO>(AppResponses.NotFoundResponse(id, nameof(Order))),
            _ => Result.Success(order.ToDTO())
        };
    }

    public async Task<Result<IEnumerable<OrderItemDTO>>> GetItems(Guid id)
    {
        Order? order = await _manager.Orders.GetById(id);
        if(order is null) return Result.Fail<IEnumerable<OrderItemDTO>>(AppResponses.NotFoundResponse(id, nameof(Order)));

        IEnumerable<OrderItem> items = await _manager.OrderItems.GetAll(
            new Specification<OrderItem>(obj => obj.OrderId == id)
        );
        return Result.Success(items.ConvertAll(OrderItemMapper.ToDTO));
    }

    public async Task<Result<OrderDTO>> Create(OrderCreateDTO orderDTO)
    {
        Customer? customer = await _manager.Customers.GetById(orderDTO.CustomerId);
        User user = await _currentLoggedInUser.GetUser();
        Order order = await _manager.Orders.Add(orderDTO.ToModel(user.Id));

        Result<OrderDTO> result = await _manager.AddAllOrderItems(orderDTO.Items, order);
        if(!result.Succeeded)
            return result;

        order.Paid = (double?)orderDTO.Paid ?? order.TotalPrice;
        _manager.Orders.Update(order);

        if(customer is not null)
        {
            customer.Dept += order.TotalPrice - order.Paid;
            _manager.Customers.Update(customer);
        }

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

        Customer? customer = await _manager.Customers.GetById(order.CustomerId);
        if(customer is not null)
        {
            customer.Dept -= order.TotalPrice - order.Paid;
            _manager.Customers.Update(customer);
        }

        await _manager.DeleteAllOrderItems(id);
        order.TotalPrice = 0;

        Result<OrderDTO> result = await _manager.AddAllOrderItems(orderDTO.Items, order);
        if(!result.Succeeded)
            return result;

        order.Paid = (double?)orderDTO.Paid ?? order.TotalPrice;
        _manager.Orders.Update(order);

        if(customer is not null)
        {
            customer.Dept += order.TotalPrice - order.Paid;
            _manager.Customers.Update(customer);
        }

        await _manager.Save();
        User user = (await _userManager.FindByEmailAsync(_currentLoggedInUser.Email))!;
        return Result.Success(order.ToDTO(customer?.Name, user.GetFullName()));
    }

    public async Task<Result> Delete(Guid id)
    {
        /* ------- Check if Order Exist ------- */
        Order? order = await _manager.Orders.GetById(id);
        if(order is null) return Result.Fail(AppResponses.NotFoundResponse(id, nameof(Order)));
        /* ------- Remove Items from Owned Elements ------- */
        await _manager.PreDeleteOrder(id);
        /* ------- Delete ------- */
        _manager.Orders.Delete(order);
        /* ------- Save Changes ------- */
        await _manager.Save();
        /* ------- Return Response ------- */
        return Result.Success(StatusCodes.Status204NoContent);
    }
}
