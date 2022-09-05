namespace RestApiSample.Web.Utilities.Statics
{
    public static class StaticDetails
    {
        public static string CurrentVersion = "1.0";

        public static string ApiBaseUrl = "https://localhost:7003";

        public static string SiteAddress = "https://localhost:7185";

        public static string ProductsApiUrl = new Uri(ApiBaseUrl + $"/api/v{CurrentVersion}/products").ToString();

        public static readonly string ProductImageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/");

        public static readonly string ProductImagePath = "/images/";

        public static readonly string DefaultImagePath = "/images/NoImage.jpg";
    }
}
