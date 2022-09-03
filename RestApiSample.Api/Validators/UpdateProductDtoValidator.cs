using FluentValidation;
using RestApiSample.Api.Data.DTOs;
using RestApiSample.Api.Data.Models;

namespace RestApiSample.Api.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(s => s.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(s => s.ProductId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(s => s.Price)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .InclusiveBetween(double.MinValue, double.MaxValue);

            RuleFor(s => s.Type)
                .NotNull()
                .IsInEnum()
                .WithMessage("The selected item is not exists.");

            RuleFor(s => s.ImageName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(s => s.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(800);
        }
    }
}
