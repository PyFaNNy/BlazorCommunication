using System.Text.Json.Serialization;

namespace BlazorCommunication.Client.Models.Auth;

public class LoginResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}