using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Dto
{
    public class CreateUserDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
