using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Dto
{
    public class UpdateCartItemDto
    {  
        [Required]
        public string? ProductName { get; set; }
    }
}
