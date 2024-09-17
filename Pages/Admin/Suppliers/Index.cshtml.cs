using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Suppliers
{
    public class IndexModel : PageModel
    {
		private readonly BookStoreContext _context;

		[BindProperty(SupportsGet = true)]
		public string CurrentFilter { get; set; }

		public IndexModel(BookStoreContext context)
		{
			_context = context;
		}

		public IList<Supplier> Supplier { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Suppliers != null)
            {
                Supplier = await _context.Suppliers.ToListAsync();
            }
        }
    }
}
