using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiSample.Api.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
