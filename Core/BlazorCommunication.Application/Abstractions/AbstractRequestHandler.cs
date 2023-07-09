using AutoMapper;
using BlazorCommunication.Application.Interfaces;
using MediatR;

namespace BlazorCommunication.Application.Abstractions;

public abstract class AbstractRequestHandler
{
    protected IMediator Mediator
    {
        get;
    }

    protected IBlazorCommunicationDbContext DbContext
    {
        get;
    }

    protected IMapper Mapper
    {
        get;
    }

    public AbstractRequestHandler(
        IMediator mediator,
        IBlazorCommunicationDbContext dbContext,
        IMapper mapper)
    {
        Mediator = mediator;
        DbContext = dbContext;
        Mapper = mapper;
    }
}