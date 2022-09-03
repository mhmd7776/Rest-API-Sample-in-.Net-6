using System.ComponentModel.DataAnnotations;
using RestApiSample.Api.Data.Models;

namespace RestApiSample.Api.Data.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string? Title { get; set; }

        public double Price { get; set; }

        public ProductType Type { get; set; }

        public string? ImageName { get; set; }

        public DateTime? ExpireDate { get; set; }

        public string? Description { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
