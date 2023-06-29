using FluentValidation;
using Reservation.Application.Constants;
using Reservation.Infrastructure.Abstracts;

namespace Reservation.Application.Features.Approver;

public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
{
    public CreateAssetCommandValidator(IDataContext db)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Name)
            .Must((name) =>
            {
                return !db.Assets
                    .Any(x => x.Name == name);
            })
            .WithMessage(Errors.DuplicateAssetName);
    }
}
