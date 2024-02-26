using LaborationBlazorShop.Models;
using Microsoft.AspNetCore.Identity;

namespace LaborationBlazorShop.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
   
    public class ApplicationUser : IdentityUser
    {
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }
    }
}
