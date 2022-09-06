using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiSample.Api.Data.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public bool IsAdmin { get; set; } = false;

        public string? Token { get; set; }
    }
}
