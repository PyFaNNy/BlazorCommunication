using BlazorCommunication.Client.Services.Users;

namespace BlazorCommunication.Client.Services;

public static class DependenciesBootstrapper
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var BlazorCommunicationAPI = new Uri(configuration.GetValue<string>("BlazorCommunicationAPI"));
        
        services.AddHttpClient<IUserService, UserService>(c => c.BaseAddress = BlazorCommunicationAPI);
        
        return services;
    }
}