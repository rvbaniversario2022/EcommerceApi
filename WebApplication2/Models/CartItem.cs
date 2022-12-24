using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Dto;
using WebApplication2.Enums;

namespace WebApplication2.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Orders")]
        public Guid OrderId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
