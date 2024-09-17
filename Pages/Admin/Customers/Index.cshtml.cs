using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.Customers
{
    public class IndexModel : PageModel
    {
        private readonly BookStoreContext _context;

        public IndexModel(BookStoreContext context)
        {
            _context = context;
        }

        public IList<Models.Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Customer = await _context.Customers.ToListAsync();
        }
    }
}
