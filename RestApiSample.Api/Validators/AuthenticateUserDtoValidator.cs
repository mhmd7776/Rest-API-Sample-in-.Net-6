using FluentValidation;
using RestApiSample.Api.Data.DTOs;

namespace RestApiSample.Api.Validators
{
    public class AuthenticateUserDtoValidator : AbstractValidator<AuthenticateUserDto>
    {
        public AuthenticateUserDtoValidator()
        {
            RuleFor(s => s.UserName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(s => s.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
