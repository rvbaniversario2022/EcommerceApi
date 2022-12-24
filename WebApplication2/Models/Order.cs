using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Enums;

namespace WebApplication2.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Status Status { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
