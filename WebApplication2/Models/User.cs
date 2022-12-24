using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
