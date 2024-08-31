using Microsoft.AspNetCore.Identity;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Mappers;
using Pharmacy.Application.Utilities;

namespace Pharmacy.Application.Services;



public class OrderService : IOrderService
{
    private readonly IRepositoryManager _manager;
    private readonly UserManager<User> _userManager;

    public OrderService(IRepositoryManager repoManager, UserManager<User> userManager)
    {
        _manager = repoManager;
        _userManager = userManager;
    }

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<OrderDTO>>(
            (await _manager.Orders.GetAll()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        /* ------- Check if Order Exist ------- */
        Order? order = await _manager.Orders.GetById(id);
        if(order is null) return new NotFoundResponse(id, nameof(Order));
        return new OkResponse<OrderDTO>(order.ToDTO());
    }

    public async Task<BaseResponse> GetItems(Guid id)
    {
        /* ------- Check if Order Exist ------- */
        Order? Order = await _manager.Orders.GetById(id);
        if(Order is null) return new NotFoundResponse(id, nameof(Order));
        /* ------- Get Items ------- */
        IEnumerable<OrderItem> items = await _manager.OrderItems.Filter(obj => obj.OrderId == id);
        return new OkResponse<IEnumerable<OrderItemDTO>>(items.ConvertAll(obj => obj.ToDTO()));
    }

    public async Task<BaseResponse> Create(OrderCreateDTO orderDTO, string userName)
    {
        /* ------- Check if Customer Exists ------- */
        Customer? customer = await _manager.Customers.GetById(orderDTO.CustomerId);
        if(customer is null) return new NotFoundResponse(orderDTO.CustomerId, nameof(Customer));
        /* ------- Get User ------- */
        User user = (await _userManager.FindByNameAsync(userName))!;
        /* ------- Create Order ------- */
        Order order = await _manager.Orders.Add(orderDTO.ToModel(user.Id));
        /* ------- Add Order Items ------- */
        BaseResponse response = await _manager.AddAllOrderItems(orderDTO.Items, order);
        if(response.StatusCode != 200)
            return response;
        /* ------- Update Order Paid ------- */
        order.Paid = (double?)orderDTO.Paid ?? order.TotalPrice;
        _manager.Orders.Update(order);
        /* ------- Update Customer Dept ------- */
        customer.Dept += order.TotalPrice - order.Paid;
        _manager.Customers.Update(customer);
        /* ------- Save Changes ------- */
        await _manager.Save();
        /* ------- Return Response ------- */
        return new CreatedResponse<OrderDTO>(
            order.ToDTO(customer.Name, user.GetFullName())
        );
    }

    public async Task<BaseResponse> Update(Guid id, OrderUpdateDTO orderDTO, string userName)
    {
        /* ------- Check if Order Exists ------- */
        Order? order = await _manager.Orders.GetById(id);
        if(order is null) return new NotFoundResponse(id, nameof(Order));
        /* ------- Update Customer ------- */
        Customer customer = (await _manager.Customers.GetById(order.CustomerId))!;
        customer.Dept -= order.TotalPrice - order.Paid;
        _manager.Customers.Update(customer);
        /* ------- Update Order Items ------- */
        await _manager.DeleteAllOrderItems(id);
        order.TotalPrice = 0;
        await _manager.AddAllOrderItems(orderDTO.Items, order);
        /* ------- Save Changes ------- */
        await _manager.Save();
        /* ------- Get User ------- */
        User user = (await _userManager.FindByNameAsync(userName))!;
        /* ------- Return Response ------- */
        return new OkResponse<OrderDTO>(
            order.ToDTO(customer.Name, user.GetFullName())
        );
    }

    public async Task<BaseResponse> Delete(Guid id)
    {
        /* ------- Check if Order Exist ------- */
        Order? Order = await _manager.Orders.GetById(id);
        if(Order is null) return new NotFoundResponse(id, nameof(Order));
        /* ------- Remove Items from Owned Elements ------- */
        await _manager.PreDeleteOrder(id);
        /* ------- Delete ------- */
        _manager.Orders.Delete(Order);
        /* ------- Save Changes ------- */
        await _manager.Save();
        /* ------- Return Response ------- */
        return new NoContentResponse();
    }
}
