using Microsoft.AspNetCore.Identity;

namespace BookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } 
    }
}
