using BlazorCommunication.Client.Services.AuthService;
using BlazorCommunication.Client.Services.Users;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorCommunication.Client.Services;

public static class DependenciesBootstrapper
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var BlazorIdentityAPI = new Uri(configuration.GetValue<string>(HttpClientNames.BlazorIdentityAPI));
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService.AuthService>();
        
        services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        
        return services;
    }
}