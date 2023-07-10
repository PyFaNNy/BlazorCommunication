using BlazorCommunication.Client.Models.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorCommunication.Client.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;
    
    public AuthService(HttpClient http,  AuthenticationStateProvider authStateProvider)
    {
        _http = http;
        _authStateProvider = authStateProvider;
    }
    
    public async Task<LoginResponse> Login(UserLogin request)
    {
        var result = await _http.PostAsJsonAsync("api/auth/login", request);
        return await result.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }
}