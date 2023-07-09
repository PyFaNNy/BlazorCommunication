using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCommunication.Controllers;

public class BaseController : ControllerBase
{
    protected ILogger Logger { get; }
    protected IMediator Mediator { get; set; }
    
    protected BaseController(ILogger logger, IMediator mediator)
    {
        Logger = logger;
        Mediator = mediator;
    }
}