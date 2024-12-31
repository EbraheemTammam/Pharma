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
        User user = (await _userManager.FindByEmailAsync(_currentLoggedInUser.Email))!;
        Order order = await _manager.Orders.Add(orderDTO.ToModel(user.Id));
        /* ------- Add Order Items ------- */
        Result<OrderDTO> result = await _manager.AddAllOrderItems(orderDTO.Items, order);
        if(!result.Succeeded)
            return result;
        /* ------- Update Order Paid ------- */
        order.Paid = (double?)orderDTO.Paid ?? order.TotalPrice;
        _manager.Orders.Update(order);
        /* ------- Update Customer Dept ------- */
        if(customer is not null)
        {
            customer.Dept += order.TotalPrice - order.Paid;
            _manager.Customers.Update(customer);
        }
        /* ------- Save Changes ------- */
        await _manager.Save();
        /* ------- Return Response ------- */
        return Result.Success
        (
            order.ToDTO(customer?.Name, user.GetFullName()),
            StatusCodes.Status201Created
        );
    }

    public async Task<Result<OrderDTO>> Update(Guid id, OrderUpdateDTO orderDTO)
    {
        /* ------- Check if Order Exists ------- */
        Order? order = await _manager.Orders.GetById(id);
        if(order is null) return Result.Fail<OrderDTO>(AppResponses.NotFoundResponse(id, nameof(Order)));
        /* ------- Update Customer ------- */
        Customer? customer = await _manager.Customers.GetById(order.CustomerId);
        if(customer is not null)
        {
            customer.Dept -= order.TotalPrice - order.Paid;
            _manager.Customers.Update(customer);
        }
        /* ------- Update Order Items ------- */
        await _manager.DeleteAllOrderItems(id);
        order.TotalPrice = 0;
        await _manager.AddAllOrderItems(orderDTO.Items, order);
        /* ------- Save Changes ------- */
        await _manager.Save();
        /* ------- Get User ------- */
        User user = (await _userManager.FindByEmailAsync(_currentLoggedInUser.Email))!;
        /* ------- Return Response ------- */
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
