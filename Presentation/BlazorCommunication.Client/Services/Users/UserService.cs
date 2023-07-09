using BlazorCommunication.Application.Aggregates.User.Commands.CreateUser;
using BlazorCommunication.Application.Aggregates.User.Commands.UpdateUser;
using BlazorCommunication.Application.Models;

namespace BlazorCommunication.Client.Services.Users;

public class UserService : IUserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<Application.Aggregates.User.Queries.GetUserByEmail.User> GetUserByEmail(string email)
    {
        var result = await _http.GetFromJsonAsync<Application.Aggregates.User.Queries.GetUserByEmail.User>($"api/User/{email}");
        return result;
    }
    
    public async Task<PaginatedList<Application.Aggregates.User.Queries.GetUsers.User>> GetUsers(int? pageIndex = 1, int? pageSize = 10)
    {
        var result = await _http.GetFromJsonAsync<PaginatedList<Application.Aggregates.User.Queries.GetUsers.User>>($"api/User?pageIndex={pageIndex}&pageSize={pageSize}");
        return result;
    }

    public async Task<Application.Aggregates.User.Commands.UpdateUser.User> UpdateUser(UpdateUserCommand command)
    {
        var result = await _http.PutAsJsonAsync($"api/User", command);
        var updatedUser = await result.Content.ReadFromJsonAsync<Application.Aggregates.User.Commands.UpdateUser.User>();
        return updatedUser;
    }

    public async Task<Application.Aggregates.User.Commands.CreateUser.User> CreateUser(CreateUserCommand command)
    {
        var result = await _http.PostAsJsonAsync("api/User", command);
        var newUser = (await result.Content
            .ReadFromJsonAsync<Application.Aggregates.User.Commands.CreateUser.User>());
        return newUser;
    }
}