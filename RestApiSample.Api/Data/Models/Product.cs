using System.ComponentModel.DataAnnotations;

namespace RestApiSample.Api.Data.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public ProductType Type { get; set; }

        public string? ImageName { get; set; }

        public DateTime? ExpireDate { get; set; }

        public string? Description { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }

    public enum ProductType
    {
        [Display(Name = "Digital")] Digital = 1,

        [Display(Name = "Physical")] Physical = 2
    }
}
