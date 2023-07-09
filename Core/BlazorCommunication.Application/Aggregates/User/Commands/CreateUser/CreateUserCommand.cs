using AutoMapper;
using BlazorCommunication.Application.Mappings;
using MediatR;

namespace BlazorCommunication.Application.Aggregates.User.Commands.CreateUser;

public class CreateUserCommand  : IRequest<User>,  IMapTo<Domain.Entities.User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserCommand, Domain.Entities.User>()
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName));
    }
}