using System.Text.Json.Serialization;

namespace BlazorCommunication.Client.Models.Auth;

public class AuthResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    
    [JsonPropertyName("error")]
    public string Error { get; set; }
    
    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; set; }

}