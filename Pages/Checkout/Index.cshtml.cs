using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Pages.Customer.Checkout
{
	public class IndexModel : PageModel
	{
		public List<OrderDetail> CartItems { get; set; } = new List<OrderDetail>();

		public IActionResult OnGet()
		{
			// Retrieve cart items from session
			CartItems = SessionHelper.GetCartItems(HttpContext);
			return Page();
		}

		public IActionResult OnPostAddToCart(Guid productId)
		{
			CartItems = SessionHelper.GetCartItems(HttpContext);

			// Check if item already exists in cart
			var existingItem = CartItems.FirstOrDefault(item => item.ProductId == productId);
			if (existingItem != null)
			{
				existingItem.Quantity++;
			}
			else
			{
				// Add new item to cart
				CartItems.Add(new OrderDetail
				{
					ProductId = productId,
					Quantity = 1
				});
			}

			SessionHelper.SetCartItems(HttpContext, CartItems);

			return Partial("_Cart", CartItems);
		}

		public IActionResult OnPostUpdateCartItemQuantity(Guid productId, int changeAmount)
		{
			CartItems = SessionHelper.GetCartItems(HttpContext);

			var itemToUpdate = CartItems.FirstOrDefault(item => item.ProductId == productId);
			if (itemToUpdate != null)
			{
				itemToUpdate.Quantity += (short)changeAmount;
				if (itemToUpdate.Quantity <= 0)
				{
					CartItems.Remove(itemToUpdate);
				}
			}

			SessionHelper.SetCartItems(HttpContext, CartItems);

			return Partial("_Cart", CartItems);
		}

		public IActionResult OnPostRemoveFromCart(Guid productId)
		{
			CartItems = SessionHelper.GetCartItems(HttpContext);

			var itemToRemove = CartItems.FirstOrDefault(item => item.ProductId == productId);
			if (itemToRemove != null)
			{
				CartItems.Remove(itemToRemove);
			}

			SessionHelper.SetCartItems(HttpContext, CartItems);

			return Partial("_Cart", CartItems);
		}
	}
}
