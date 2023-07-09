using System.Security.Claims;
using BlazorCommunication.Application.Interfaces;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;

namespace BlazorCommunication.IdentityServer.Infrastructure;

public class ProfileService : IProfileService
{
    private readonly IBlazorCommunicationDbContext _dbContext;
    public ProfileService(IBlazorCommunicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var email = context.Subject.Identity.Name;
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
            new Claim(JwtClaimTypes.Email, user.Email)
            //Add role to claim
            //new Claim(JwtClaimTypes.Role, user.Role.ToName()),
        };

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var email = context.Subject.Identity.Name;
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        //Check active status user
        context.IsActive = user is {RemovedAt: null };
    }
}