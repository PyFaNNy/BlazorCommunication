using System.Security.Claims;
using BlazorCommunication.Application.Interfaces;
using BlazorCommunication.Common;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;

namespace BlazorCommunication.IdentityServer.Infrastructure;

public class UserValidator : IResourceOwnerPasswordValidator
{
    private readonly IBlazorCommunicationDbContext _dbContext;
    public UserValidator(IBlazorCommunicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == context.UserName);

        if (user != null)
        {
            var result = PasswordsHelper.VerifyPasswordHash(context.Password, user.PasswordHash, user.PasswordSalt);
            if (result)
            {
                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: "custom",
                    claims: new[]
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        //Add role to claim
                        //new Claim(ClaimTypes.Role, user.Role.ToName())
                    }
                );
                return;
            }
        }

        // context set to Failure        
        context.Result = new GrantValidationResult(
            TokenRequestErrors.UnauthorizedClient, "Invalid Credentials");
    }
}