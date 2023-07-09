using BlazorCommunication.Application.Abstractions;
using BlazorCommunication.Application.Models;

namespace BlazorCommunication.Application.Aggregates.User.Queries.GetUsers;

public class GetUsersQuery : GetPaginatedListBaseQuery<PaginatedList<User>>
{
    public GetUsersQuery(int? pageIndex, int? pageSize) : base(pageIndex, pageSize)
    {
    }
}