using BlazorCommunication.Application.Aggregates.User.Commands.CreateUser;
using BlazorCommunication.Application.Aggregates.User.Commands.UpdateUser;
using BlazorCommunication.Application.Models;
using User = BlazorCommunication.Application.Aggregates.User.Queries.GetUsers.User;

namespace BlazorCommunication.Client.Services.Users;

public interface IUserService
{
    event Action? UsersChanged;
    int CurrentPage { get; set; }
    int PageCount { get; set; }
    int PageSize { get; set; }
    List<User> Users { get; set; }
    
    Task<Application.Aggregates.User.Queries.GetUserByEmail.User> GetUserByEmail(string email);
    Task GetUsers(int? pageIndex = 1, int? pageSize = 10);
    Task<Application.Aggregates.User.Commands.UpdateUser.User> UpdateUser(UpdateUserCommand command);
    Task<Application.Aggregates.User.Commands.CreateUser.User> CreateUser(CreateUserCommand command);
}