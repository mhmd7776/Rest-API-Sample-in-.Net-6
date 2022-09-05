using System.Text.Encodings.Web;
using System.Text.Unicode;
using FluentValidation;
using FluentValidation.AspNetCore;
using RestApiSample.Web.Repositories.Implementations;
using RestApiSample.Web.Repositories.Interfaces;
using RestApiSample.Web.Validators;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

#region Fluent Validation

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductViewModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductViewModelValidator>();

#endregion

#region Encode

builder.Services.AddSingleton<HtmlEncoder>(
    HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

#endregion

#endregion

#region Middlewares

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion
