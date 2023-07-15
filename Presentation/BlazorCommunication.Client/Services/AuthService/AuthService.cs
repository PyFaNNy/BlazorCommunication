using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BlazorCommunication.Client.Models.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorCommunication.Client.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ITokenService _tokenService;
    private const string OAUTH_CLIENT = "blazor_client";
    private const string OAUTH_SECRET = "blazor_client";
    
    public AuthService(IHttpClientFactory httpClientFactory,  AuthenticationStateProvider authStateProvider,
        ITokenService tokenService)
    {
        _http = httpClientFactory.CreateClient(HttpClientNames.BlazorIdentityAPI);
        _authStateProvider = authStateProvider;
        _tokenService = tokenService;
        var base64AuthString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{OAUTH_CLIENT}:{OAUTH_SECRET}"));
        _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64AuthString);
    }

    public async Task<LoginResponse> Login(UserLogin request)
    {
        await _tokenService.RemoveToken();
        await _tokenService.RemoveRefreshToken();

        var body = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", request.Username),
            new KeyValuePair<string, string>("password", request.Password),
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", OAUTH_CLIENT),
            new KeyValuePair<string, string>("client_secret", OAUTH_SECRET)
        });

        try
        {
            var response = await _http.PostAsync("/connect/token", body);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponse>(content);

            if (!string.IsNullOrEmpty(result.Error))
            {
                return new LoginResponse()
                {
                    IsSuccess = false,
                    Error = result.ErrorDescription
                };
            }
            
            await _tokenService.SaveToken(result.AccessToken);
            await _tokenService.SaveRefreshToken(result.RefreshToken);
        }
        catch (HttpRequestException ex)
        {
            // Обработка ошибок
            throw;
        }

        return new()
        {
            IsSuccess = true
        };
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }

    public async Task Logout()
    {
        await _tokenService.RemoveToken();
        await _tokenService.RemoveRefreshToken();
    }
    
    public async Task RefreshTokenAsync()
    {
        var refreshData = await _tokenService.GetRefreshToken();
        await _tokenService.RemoveToken();
        await _tokenService.RemoveRefreshToken();

        var body = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("refresh_token", refreshData),
            new KeyValuePair<string, string>("grant_type", "refresh_token")
        });

        try
        {
            var response = await _http.PostAsync("/connect/token", body);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponse>(content);

            await _tokenService.SaveToken(result.AccessToken);
            await _tokenService.SaveRefreshToken(result.RefreshToken);
        }
        catch (HttpRequestException ex)
        {
            // Обработка ошибок
            throw;
        }
    }
    
    
}