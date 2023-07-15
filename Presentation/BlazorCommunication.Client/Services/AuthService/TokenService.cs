using Blazored.LocalStorage;

namespace BlazorCommunication.Client.Services.AuthService;

public class TokenService : ITokenService
{
    private const string ACCESS_TOKEN  = "access_token";
    private const string REFRESH_TOKEN = "refresh_token";
    private readonly ILocalStorageService _localStorage;
    public TokenService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    public async Task<string> GetToken()
    {
        try
        {
            var token = await _localStorage.GetItemAsStringAsync(ACCESS_TOKEN);
            return token;
        }
        catch (InvalidOperationException)
        {
        }

        return null;
    }

    public async Task<string> GetRefreshToken()
    {
        try
        {
            return await _localStorage.GetItemAsync<string>(REFRESH_TOKEN);
        }
        catch (InvalidOperationException)
        {
        }

        return null;
    }

    public async Task SaveToken(string token)
    {
        try
        {
            await _localStorage.SetItemAsync(ACCESS_TOKEN, token);
        }
        catch (InvalidOperationException)
        {
        }
    }

    public async Task SaveRefreshToken(string refreshToken)
    {
        try
        {
            await _localStorage.SetItemAsync(REFRESH_TOKEN, refreshToken);
        }
        catch (InvalidOperationException)
        {
        }
    }

    public async Task RemoveToken()
    {
        try
        {
            await _localStorage.RemoveItemAsync(ACCESS_TOKEN);
        }
        catch (InvalidOperationException)
        {
        }
    }

    public async Task RemoveRefreshToken()
    {
        try
        {
            await _localStorage.RemoveItemAsync(REFRESH_TOKEN);
        }
        catch (InvalidOperationException)
        {
        }
    }
}