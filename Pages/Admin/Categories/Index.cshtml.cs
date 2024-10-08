﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;


namespace BookStore.Pages.Categories
{
    public class IndexModel : PageModel
    {
		private readonly BookStoreContext _context;

		public IndexModel(BookStoreContext context)
		{
			_context = context;
		}

		public IList<Category> Category { get;set; } = default!;

		[Authorize(Policy = "RequireAdminRole")]
        public async Task OnGetAsync()
        {
            if (_context.Categories != null)
            {
                Category = await _context.Categories.ToListAsync();
            }
        }
    }
}
