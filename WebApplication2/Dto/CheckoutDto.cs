using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Dto
{
    public class CheckoutDto
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
