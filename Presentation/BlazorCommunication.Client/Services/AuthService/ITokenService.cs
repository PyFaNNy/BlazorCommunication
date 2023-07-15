namespace BlazorCommunication.Client.Services.AuthService;

public interface ITokenService
{
    public Task<string> GetToken();
    public Task<string> GetRefreshToken();
    public Task SaveToken(string token);
    public Task SaveRefreshToken(string refreshToken);
    public Task RemoveToken();
    public Task RemoveRefreshToken();
    
}