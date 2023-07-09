using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorCommunication.Application.Abstractions;
using BlazorCommunication.Application.Interfaces;
using BlazorCommunication.Application.Models;
using MediatR;

namespace BlazorCommunication.Application.Aggregates.User.Queries.GetUsers;

public class GetUsersQueryHandler : AbstractRequestHandler, IRequestHandler<GetUsersQuery, PaginatedList<User>>
{
    public GetUsersQueryHandler(IMediator mediator, IBlazorCommunicationDbContext dbContext, IMapper mapper) : base(mediator, dbContext, mapper)
    {
    }

    public async Task<PaginatedList<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var baseUsers = DbContext.Users
            .ProjectTo<User>(Mapper.ConfigurationProvider);

        var paginatedList =
            await PaginatedList<User>.CreateAsync(baseUsers, request.PageIndex, request.PageSize);
        
        return paginatedList;
    }
}