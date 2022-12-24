using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Dto
{
    public class CreateCartItemDto
    {
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
