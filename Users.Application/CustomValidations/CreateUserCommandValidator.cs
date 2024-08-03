using FluentValidation;
using Users.Application.Commands;

namespace Users.Application.CustomValidations
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x=>x.Email).NotEmpty()
                               .WithMessage("L'email è obbligatoria");

            RuleFor(x=>x.Password).NotEmpty().WithMessage("La password è obbligatoria")
                                  .MinimumLength(12).WithMessage("La password deve contenere almeno 15 caratteri");

            RuleFor(x=>x.CodiceFiscale).NotEmpty().WithMessage("Codice Fiscale è obbligatorio")
                                       .Length(16).WithMessage("Codice Fiscale deve avere esattamente 16 caratteri");

            RuleFor(x => x.Nome).NotEmpty()
                                .WithMessage("Il nome è obbligatorio");

            RuleFor(x=>x.Cognome).NotEmpty()
                                 .WithMessage("Il cognome è obbligatorio");

            RuleFor(x=>x.DataNascita).NotEmpty()
                                     .WithMessage("Data Nascita è obbligatoria");

            RuleFor(x=>x.Indirizzo).NotEmpty()
                                   .WithMessage("L'indirizzo è obbligatorio");
        }
    }
}
