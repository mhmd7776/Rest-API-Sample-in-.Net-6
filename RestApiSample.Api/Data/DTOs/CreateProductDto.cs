using System.ComponentModel.DataAnnotations;
using RestApiSample.Api.Data.Models;

namespace RestApiSample.Api.Data.DTOs
{
    public class CreateProductDto
    {
        public string? Title { get; set; }

        public double Price { get; set; }

        public ProductType Type { get; set; }

        public string? ImagePath { get; set; }

        public string? Description { get; set; }
    }
}
