using System.Text.Json.Serialization;

namespace BlazorCommunication.Client.Models.Auth;

public class UserLogin
{
    [JsonPropertyName("username")]
    public string Username { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = "password";
}