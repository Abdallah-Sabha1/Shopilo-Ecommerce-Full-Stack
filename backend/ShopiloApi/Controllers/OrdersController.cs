using Microsoft.AspNetCore.Mvc;
using ShopiloApi.DTOs.Orders;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService) => _orderService = orderService;

    [HttpPost]
    public async Task<ActionResult<OrderDto>> PlaceOrder(
        PlaceOrderDto request,
        CancellationToken cancellationToken)
    {
        Order order = await _orderService.PlaceOrderAsync(
            request.CartId,
            request.CouponCode,
            cancellationToken);

        return CreatedAtAction(nameof(GetOrder), new { orderId = order.Id }, ToDto(order));
    }

    [HttpGet("{orderId:guid}")]
    public async Task<ActionResult<OrderDto>> GetOrder(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        Order order = await _orderService.GetOrderByIdAsync(orderId, cancellationToken);
        return Ok(ToDto(order));
    }

    private static OrderDto ToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            CreatedAt = order.CreatedAt,
            Status = order.Status.ToString(),
            Subtotal = order.Subtotal,
            DiscountAmount = order.DiscountAmount,
            ShippingCost = order.ShippingCost,
            Total = order.Total,
            CouponCode = order.CouponCode,
            Items = order.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                ProductTitle = item.ProductTitle,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                LineTotal = item.LineTotal
            }).ToList()
        };
    }
}
