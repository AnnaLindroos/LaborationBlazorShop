using LaborationBlazorShop.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaborationBlazorShop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
