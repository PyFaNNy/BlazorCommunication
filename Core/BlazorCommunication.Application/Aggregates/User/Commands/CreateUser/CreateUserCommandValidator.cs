using BlazorCommunication.Application.Interfaces;
using FluentValidation;
using BlazorCommunication.Common;

namespace BlazorCommunication.Application.Aggregates.User.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private IBlazorCommunicationDbContext DbContext
    {
        get;
    }
    public CreateUserCommandValidator(IBlazorCommunicationDbContext dbContext)
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

        RuleFor(x => x.Password)
            .Must(PasswordsHelper.IsMeetsRequirements);
    }
    
    private bool IsUniqueEmail(string email)
    {
        return !DbContext.Users.Any(x => x.Email.Equals(email));
    }
}