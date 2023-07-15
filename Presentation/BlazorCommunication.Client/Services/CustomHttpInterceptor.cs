using System.Net.Http.Headers;
using BlazorCommunication.Client.Services.AuthService;
using Microsoft.AspNetCore.Components;

namespace BlazorCommunication.Client.Services;

public class CustomHttpInterceptor : DelegatingHandler
{
    private readonly ITokenService _tokenService;
    private readonly IAuthService _authService;
    private readonly NavigationManager _navigationManager;

    public CustomHttpInterceptor(ITokenService tokenService, IAuthService authService,
        NavigationManager navigationManager)
    {
        _tokenService = tokenService;
        _authService = authService;
        _navigationManager = navigationManager;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _tokenService.GetToken();
        var refreshToken = await _tokenService.GetRefreshToken();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _authService.RefreshTokenAsync();
                _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true);
            }
            else
            {
                await _tokenService.RemoveToken();
                await _tokenService.RemoveRefreshToken();
                _navigationManager.NavigateTo("login");
            }
        }
        else if ((int) response.StatusCode >= 500)
        {
            _navigationManager.NavigateTo("error");
        }

        return response;
    }
}