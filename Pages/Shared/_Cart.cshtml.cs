using System.Collections.Generic;
using System.Linq;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using BookStore.Models;
namespace BookStore.Pages.Shared
{
    public class _CartModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Partial("_Cart", SessionHelper.GetCartItems(HttpContext));
        }
    }
}
