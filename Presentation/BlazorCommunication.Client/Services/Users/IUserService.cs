using BlazorCommunication.Application.Aggregates.User.Commands.CreateUser;
using BlazorCommunication.Application.Aggregates.User.Commands.UpdateUser;
using BlazorCommunication.Application.Models;

namespace BlazorCommunication.Client.Services.Users;

public interface IUserService
{
    Task<Application.Aggregates.User.Queries.GetUserByEmail.User> GetUserByEmail(string email);
    Task<PaginatedList<Application.Aggregates.User.Queries.GetUsers.User>> GetUsers(int? pageIndex = 1, int? pageSize = 10);
    Task<Application.Aggregates.User.Commands.UpdateUser.User> UpdateUser(UpdateUserCommand command);
    Task<Application.Aggregates.User.Commands.CreateUser.User> CreateUser(CreateUserCommand command);
}