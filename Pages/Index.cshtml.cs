using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Pages
{
	public class IndexModel : PageModel
	{
		private readonly BookStoreContext _context;
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger, BookStoreContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IList<Product> Products { get; set; } = default!;
		public int CurrentPage { get; set; }
		public int PageSize { get; set; } = 12;

		public async Task OnGetAsync(string searchString, int sortBy, decimal? minPrice, decimal? maxPrice, int currentPage = 1)
		{
			CurrentPage = currentPage;
			IQueryable<Product> productsQuery = _context.Products
				.Include(p => p.Category)
				.Include(p => p.Supplier);

			if (!string.IsNullOrEmpty(searchString))
			{
				Guid searchId;
				if (Guid.TryParse(searchString, out searchId))
				{
					productsQuery = productsQuery.Where(p => p.ProductId == searchId);
				}
				else
				{
					productsQuery = productsQuery.Where(p => p.ProductName.ToLower().Contains(searchString.ToLower()));
				}
			}

			if (minPrice.HasValue)
			{
				productsQuery = productsQuery.Where(p => p.UnitPrice >= minPrice.Value);
			}

			if (maxPrice.HasValue)
			{
				productsQuery = productsQuery.Where(p => p.UnitPrice <= maxPrice.Value);
			}

			switch (sortBy)
			{
				case 1: // Name
					productsQuery = productsQuery.OrderBy(p => p.ProductName);
					break;
				case 2: // Id
					productsQuery = productsQuery.OrderBy(p => p.ProductId);
					break;
				default:
					break;
			}

			Products = await productsQuery
				.Skip((CurrentPage - 1) * PageSize)
				.Take(PageSize)
				.ToListAsync();
		}

		public async Task<IActionResult> OnGetLoadMoreProductsAsync(int currentPage)
		{
			var products = await _context.Products
				.Include(p => p.Category)
				.Include(p => p.Supplier)
				.Skip((currentPage - 1) * PageSize)
				.Take(PageSize)
				.ToListAsync();

			return Partial("_ProductList", products);
		}
	}
}
