using AutoMapper;
using BlazorCommunication.Application.Abstractions;
using BlazorCommunication.Application.Exceptions;
using BlazorCommunication.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorCommunication.Application.Aggregates.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : AbstractRequestHandler, IRequestHandler<DeleteUserCommand, Unit>
    {
        public DeleteUserCommandHandler(IMediator mediator, IBlazorCommunicationDbContext dbContext, IMapper mapper) : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await DbContext.Users
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.User), request.Id);
            }

            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
