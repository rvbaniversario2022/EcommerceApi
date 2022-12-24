using System.ComponentModel.DataAnnotations;
using WebApplication2.Enums;

namespace WebApplication2.Dto
{
    public class UpdateOrderDto
    {
        [Required]
        public Status Status { get; set; }
    }
}
