using System.Security.Claims;
using BlazorCommunication.Application.Interfaces;
using BlazorCommunication.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorCommunication.IdentityServer.Infrastructure;

public class ProfileService : IProfileService
{
    private readonly IBlazorCommunicationDbContext _dbContext;
    private readonly SuperAdmin _supAdmin;
    public ProfileService(IBlazorCommunicationDbContext dbContext, IOptions<SuperAdmin> superAdmin)
    {
        _dbContext = dbContext;
        _supAdmin = superAdmin.Value;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        if (context.Subject.Identity.Name == _supAdmin.Email)
        {
            var claimsSup = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, _supAdmin.Email),
                //new Claim(JwtClaimTypes.Role, UserRole.SuperAdmin.ToName()),
                new Claim(JwtClaimTypes.Email, _supAdmin.Email),
            };

            context.IssuedClaims.AddRange(claimsSup);
            return;
        }
        
        var email = context.Subject.Identity.Name;
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
            //new Claim(JwtClaimTypes.Role, user.Role.ToName()),
            new Claim(JwtClaimTypes.Email, user.Email),
        };

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        if (context.Subject.Identity.Name == _supAdmin.Email)
        {
            context.IsActive = true;
            return;
        }
        
        
        var email = context.Subject.Identity.Name;
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        context.IsActive = user is {RemovedAt: null};
    }
}