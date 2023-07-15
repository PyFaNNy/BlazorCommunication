using BlazorCommunication.Application.Aggregates.User.Commands.CreateUser;
using BlazorCommunication.Application.Aggregates.User.Commands.UpdateUser;
using BlazorCommunication.Application.Models;
using User = BlazorCommunication.Application.Aggregates.User.Queries.GetUsers.User;

namespace BlazorCommunication.Client.Services.Users;

public class UserService : IUserService
{
    private readonly HttpClient _http;
    public event Action? UsersChanged;
    public int CurrentPage { get; set; } = 1;
    public int PageCount { get; set; } = 0;
    public int PageSize { get; set; } = 5;
    public List<User> Users { get; set; } = new List<User>();

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient(HttpClientNames.BlazorCommunicationAPI);
    }
    
    public async Task<Application.Aggregates.User.Queries.GetUserByEmail.User> GetUserByEmail(string email)
    {
        var result = await _http.GetFromJsonAsync<Application.Aggregates.User.Queries.GetUserByEmail.User>($"/User/{email}");
        return result;
    }
    
    public async Task GetUsers(int? pageIndex = 1, int? pageSize = 10)
    {
        var result = await _http.GetAsync($"/User?pageIndex={pageIndex}&pageSize={pageSize}");
        var list = await result.Content.ReadFromJsonAsync<PaginatedList<User>>();
        if (list is not null)
        {
            PageCount = list.TotalPages;
            Users = list.Items;
            CurrentPage = list.PageIndex;
        }

        UsersChanged?.Invoke();
    }

    public async Task<Application.Aggregates.User.Commands.UpdateUser.User> UpdateUser(UpdateUserCommand command)
    {
        var result = await _http.PutAsJsonAsync($"/User", command);
        var updatedUser = await result.Content.ReadFromJsonAsync<Application.Aggregates.User.Commands.UpdateUser.User>();
        return updatedUser;
    }

    public async Task<Application.Aggregates.User.Commands.CreateUser.User> CreateUser(CreateUserCommand command)
    {
        var result = await _http.PostAsJsonAsync("/User", command);
        var newUser = (await result.Content
            .ReadFromJsonAsync<Application.Aggregates.User.Commands.CreateUser.User>());
        return newUser;
    }
}