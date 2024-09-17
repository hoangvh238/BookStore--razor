using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Pages
{
	public class _ProductListModel : PageModel
	{
		public IEnumerable<Product> Products { get; set; }

		public List<OrderDetail> CartItems { get; set; } = new List<OrderDetail>();

		public void OnGet()
		{
			// Retrieve the products from the data source
			Products = GetProducts();

			// Retrieve cart items from session
			CartItems = SessionHelper.GetCartItems(HttpContext);
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

			// Redirect to the same page to show the updated cart
			return RedirectToPage();
		}

		private IEnumerable<Product> GetProducts()
		{
			// This should retrieve products from your database or data source.
			// For demonstration, let's return a sample list of products.
			return new List<Product>
			{
				new Product { ProductId = Guid.NewGuid(), ProductName = "Book 1", ProductImage = "path/to/image1.jpg", UnitPrice = 10.0m },
				new Product { ProductId = Guid.NewGuid(), ProductName = "Book 2", ProductImage = "path/to/image2.jpg", UnitPrice = 15.0m },
                // Add more products as needed
            };
		}
	}
}
