using BlazorCommunication.Application.Interfaces;
using FluentValidation;

namespace BlazorCommunication.Application.Aggregates.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private IBlazorCommunicationDbContext DbContext { get; }

    public UpdateUserCommandValidator(IBlazorCommunicationDbContext dbContext)
    {
        DbContext = dbContext;

        RuleFor(x => x.Email)
            .EmailAddress()
            .Must(IsUniqueEmail);

        RuleFor(x => x.LastName)
            .NotNull()
            .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$");

        RuleFor(x => x.FirstName)
            .NotNull()
            .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$");
    }

    private bool IsUniqueEmail(string email)
    {
        return !DbContext.Users.Any(x => x.Email.Equals(email));
    }
}