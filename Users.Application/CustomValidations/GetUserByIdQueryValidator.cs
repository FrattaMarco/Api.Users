using FluentValidation;
using Users.Application.Queries;

namespace Users.Application.CustomValidations
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.IdUtente).GreaterThan(0)
                                    .WithMessage("IdUtente deve essere maggiore di 0");
        }
    }
}
