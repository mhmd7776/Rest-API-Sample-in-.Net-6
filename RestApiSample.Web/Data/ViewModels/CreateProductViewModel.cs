namespace RestApiSample.Web.Data.ViewModels
{
    public class CreateProductViewModel
    {
        public string? Title { get; set; }

        public double Price { get; set; }

        public ProductType Type { get; set; }

        public string? ImagePath { get; set; }

        public string? Description { get; set; }
    }
}
