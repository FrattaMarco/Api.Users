using FluentValidation;
using Users.Application.Commands;

namespace Users.Application.CustomValidations
{

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.IdUtente).GreaterThan(0)
                                    .WithMessage("IdUtente deve essere maggiore di 0");
        }
    }
}
