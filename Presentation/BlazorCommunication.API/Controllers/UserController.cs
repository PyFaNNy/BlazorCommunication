using MediatR;
using Microsoft.AspNetCore.Mvc;
using BlazorCommunication.Application.Aggregates.User.Commands.CreateUser;
using BlazorCommunication.Application.Aggregates.User.Commands.DeleteUser;
using BlazorCommunication.Application.Aggregates.User.Commands.UpdateUser;
using BlazorCommunication.Application.Aggregates.User.Queries.GetUserByEmail;
using BlazorCommunication.Application.Aggregates.User.Queries.GetUsers;
using BlazorCommunication.Application.Exceptions;
using BlazorCommunication.Application.Models;
using CreateUser = BlazorCommunication.Application.Aggregates.User.Commands.CreateUser.User;
using PaginatedUser = BlazorCommunication.Application.Aggregates.User.Queries.GetUsers.User;
using User = BlazorCommunication.Application.Aggregates.User.Queries.GetUserByEmail.User;

namespace BlazorCommunication.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController
{
    public UserController(IMediator mediator, ILogger<UserController> logger) : base(logger,mediator)
    {
    }

    /// <summary>
    /// Get user bu Email
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        try
        {
            var result = await Mediator.Send(new GetUserByEmailQuery(email));
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }
    
    /// <summary>
    /// Get users
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<PaginatedUser>))]
    public async Task<IActionResult> GetUsers(int? pageIndex = 1, int? pageSize = 10)
    {
        var result = await Mediator.Send(new GetUsersQuery(pageIndex, pageSize));
        return Ok(result);
    }
    
    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUser))]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete User
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        try
        {
            await Mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }
    

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlazorCommunication.Application.Aggregates.User.Commands.UpdateUser.User))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
    {
        try
        {
            var user = await Mediator.Send(command);
            return Ok(user);
        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }
}