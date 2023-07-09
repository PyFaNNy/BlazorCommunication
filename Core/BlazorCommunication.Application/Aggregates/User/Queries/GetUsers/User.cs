using AutoMapper;
using BlazorCommunication.Application.Mappings;

namespace BlazorCommunication.Application.Aggregates.User.Queries.GetUsers;

public class User : IMapFrom<Domain.Entities.User>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.User, User>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => s.PasswordHash));
    }
}