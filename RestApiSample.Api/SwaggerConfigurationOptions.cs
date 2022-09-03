using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RestApiSample.Api
{
    public class SwaggerConfigurationOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public SwaggerConfigurationOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"Product Api - v{description.ApiVersion}",
                    Description = "Rest Api sample in asp.net core",
                    Version = description.ApiVersion.ToString(),
                    Contact = new OpenApiContact
                    {
                        Email = "mohammadmahdavi.dev@gmail.com",
                        Name = "Mohammad Mahdavi",
                        Url = new Uri("https://mohammadmahdavi.com")
                    }
                });
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "ProductApi.xml");
            options.IncludeXmlComments(path);
        }
    }
}
