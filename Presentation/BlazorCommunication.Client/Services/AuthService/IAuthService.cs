using BlazorCommunication.Client.Models.Auth;

namespace BlazorCommunication.Client.Services.AuthService;

public interface IAuthService
{
    Task<LoginResponse> Login(UserLogin request);
    Task<bool> IsUserAuthenticated();
}