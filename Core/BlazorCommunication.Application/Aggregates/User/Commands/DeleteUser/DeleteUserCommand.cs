using MediatR;

namespace BlazorCommunication.Application.Aggregates.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
