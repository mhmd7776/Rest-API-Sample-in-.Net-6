using System.ComponentModel.DataAnnotations;

namespace RestApiSample.Web.Data.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string? Title { get; set; }

        public double Price { get; set; }

        public ProductType Type { get; set; }

        public string? ImagePath { get; set; }

        public string? Description { get; set; }

        public DateTime CreateDate { get; set; }
    }

    public enum ProductType
    {
        [Display(Name = "Digital")] Digital = 1,

        [Display(Name = "Physical")] Physical = 2
    }
}
