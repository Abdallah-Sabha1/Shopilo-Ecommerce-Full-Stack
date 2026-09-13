using Microsoft.AspNetCore.Mvc;
using ShopiloApi.DTOs.Cart;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Controllers;

[Route("api/carts")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService) => _cartService = cartService;

    [HttpPost]
    public async Task<ActionResult<CartDto>> CreateCart(CancellationToken cancellationToken)
    {
        Cart cart = await _cartService.CreateCartAsync(cancellationToken);
        return CreatedAtAction(nameof(GetCart), new { cartId = cart.Id }, ToDto(cart));
    }

    [HttpGet("{cartId:guid}")]
    public async Task<ActionResult<CartDto>> GetCart(
        Guid cartId,
        CancellationToken cancellationToken)
    {
        Cart cart = await _cartService.GetCartByIdAsync(cartId, cancellationToken);
        return Ok(ToDto(cart));
    }

    [HttpPost("{cartId:guid}/items")]
    public async Task<ActionResult<CartDto>> AddItem(
        Guid cartId,
        AddToCartDto request,
        CancellationToken cancellationToken)
    {
        Cart cart = await _cartService.AddItemAsync(
            cartId,
            request.ProductId,
            request.Quantity,
            cancellationToken);

        return Ok(ToDto(cart));
    }

    [HttpPut("{cartId:guid}/items/{cartItemId:int}")]
    public async Task<ActionResult<CartDto>> UpdateQuantity(
        Guid cartId,
        int cartItemId,
        UpdateQuantityDto request,
        CancellationToken cancellationToken)
    {
        Cart cart = await _cartService.UpdateItemQuantityAsync(
            cartId,
            cartItemId,
            request.Quantity,
            cancellationToken);

        return Ok(ToDto(cart));
    }

    [HttpDelete("{cartId:guid}/items/{cartItemId:int}")]
    public async Task<ActionResult<CartDto>> RemoveItem(
        Guid cartId,
        int cartItemId,
        CancellationToken cancellationToken)
    {
        Cart cart = await _cartService.RemoveItemAsync(cartId, cartItemId, cancellationToken);
        return Ok(ToDto(cart));
    }

    [HttpDelete("{cartId:guid}/items")]
    public async Task<IActionResult> ClearCart(Guid cartId, CancellationToken cancellationToken)
    {
        await _cartService.ClearCartAsync(cartId, cancellationToken);
        return NoContent();
    }

    private static CartDto ToDto(Cart cart)
    {
        List<CartItemDto> items = cart.CartItems.Select(item => new CartItemDto
        {
            Id = item.Id,
            ProductId = item.ProductId,
            ProductTitle = item.Product?.Title ?? string.Empty,
            Brand = item.Product?.Brand ?? string.Empty,
            Thumbnail = item.Product?.Thumbnail ?? string.Empty,
            UnitPrice = item.Product?.Price ?? 0m,
            Quantity = item.Quantity,
            LineTotal = (item.Product?.Price ?? 0m) * item.Quantity,
            Stock = item.Product?.Stock ?? 0
        }).ToList();

        return new CartDto
        {
            Id = cart.Id,
            CartItems = items,
            Subtotal = items.Sum(item => item.LineTotal),
            ItemCount = items.Sum(item => item.Quantity)
        };
    }
}
