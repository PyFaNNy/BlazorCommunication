

using IdentityModel;
using IdentityServer4.Models;

namespace BlazorCommunication.IdentityServer;

public static class Configuration
{
    public static IEnumerable<Client> GetClients(IConfiguration configuration) =>
        new List<Client>
        {
            new Client
            {
                ClientId = "blazor_client",
                ClientSecrets = {new Secret("blazor_client".ToSha256())},
                AllowedCorsOrigins =
                {
                    configuration.GetValue<string>("FRONTEND_URL"),
                    configuration.GetValue<string>("BACKEND_URL")
                },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowedScopes =
                {
                    "BlazorCommunicationAPI"
                },
                AccessTokenLifetime = 3600,
                AllowOfflineAccess = true
            }
        };

    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new ApiResource("TrainerAPI")
        };

    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        yield return new ApiScope("TrainerAPI", "Trainer API");
    }
}