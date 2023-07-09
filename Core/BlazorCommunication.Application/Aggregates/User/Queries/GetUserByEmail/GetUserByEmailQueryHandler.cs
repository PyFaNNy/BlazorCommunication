using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorCommunication.Application.Abstractions;
using BlazorCommunication.Application.Exceptions;
using BlazorCommunication.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorCommunication.Application.Aggregates.User.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler : AbstractRequestHandler, IRequestHandler<GetUserByEmailQuery, User>
{
    public GetUserByEmailQueryHandler(IMediator mediator, IBlazorCommunicationDbContext dbContext, IMapper mapper) : base(mediator, dbContext, mapper)
    {
    }

    public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await DbContext.Users
            .Where(x => x.Email == request.Email)
            .ProjectTo<User>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.User), request.Email);
        }

        return user;
    }
}