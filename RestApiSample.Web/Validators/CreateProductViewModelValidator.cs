using FluentValidation;
using RestApiSample.Web.Data.ViewModels;

namespace RestApiSample.Web.Validators
{
    public class CreateProductViewModelValidator : AbstractValidator<CreateProductViewModel>
    {
        public CreateProductViewModelValidator()
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

            RuleFor(s => s.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(800);
        }
    }
}
