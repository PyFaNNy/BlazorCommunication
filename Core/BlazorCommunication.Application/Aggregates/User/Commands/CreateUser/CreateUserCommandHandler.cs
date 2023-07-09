using AutoMapper;
using BlazorCommunication.Application.Abstractions;
using BlazorCommunication.Application.Interfaces;
using MediatR;
using BlazorCommunication.Common;

namespace BlazorCommunication.Application.Aggregates.User.Commands.CreateUser;

public class CreateUserCommandHandler : AbstractRequestHandler, IRequestHandler<CreateUserCommand, User>
{
    public CreateUserCommandHandler(IMediator mediator, IBlazorCommunicationDbContext dbContext, IMapper mapper) : base(mediator, dbContext, mapper)
    {
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = Mapper.Map<Domain.Entities.User>(request);
        
        PasswordsHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        
        await DbContext.Users.AddAsync(user, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        
        return Mapper.Map<User>(user);
    }
}