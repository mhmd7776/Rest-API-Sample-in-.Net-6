using FluentValidation;
using RestApiSample.Api.Data.DTOs;

namespace RestApiSample.Api.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(s => s.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);

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
