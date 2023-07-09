using AutoMapper;
using BlazorCommunication.Application.Abstractions;
using BlazorCommunication.Application.Exceptions;
using BlazorCommunication.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorCommunication.Application.Aggregates.User.Commands.UpdateUser;

public class UpdateUserCommandHandler : AbstractRequestHandler, IRequestHandler<UpdateUserCommand, User>
{
    public UpdateUserCommandHandler(IMediator mediator, IBlazorCommunicationDbContext dbContext, IMapper mapper) : base(mediator, dbContext, mapper)
    {
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await DbContext.Users
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.User), request.Id);
        }
        
        Mapper.Map(request, user);
        await DbContext.SaveChangesAsync(cancellationToken);
        
        return Mapper.Map<User>(user);
    }
}