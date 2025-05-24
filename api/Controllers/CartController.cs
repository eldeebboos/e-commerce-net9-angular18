using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CartController(ICartService cartService) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ShoppingCart>> GetCart(string id)
    {
        var cart = await cartService.GetCartAsync(id);
        return Ok(cart ?? new ShoppingCart { Id = id });
    }


    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
    {
        if (cart == null || string.IsNullOrEmpty(cart.Id))
        {
            return BadRequest("Invalid cart data.");
        }

        var updatedCart = await cartService.SetCartAsync(cart);
        if (updatedCart == null)
            return BadRequest("Proplem with cart.");
        return Ok(updatedCart);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteCart(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid cart ID.");
        }

        var result = await cartService.DeleteCartAsync(id);
        if (!result)
            return NotFound("Cart not found or could not be deleted.");

        return Ok();
    }
}
