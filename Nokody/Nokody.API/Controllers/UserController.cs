using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nokody.API.Domain.Handlers;
using Nokody.API.Domain.Handlers.Users;
using Nokody.API.Domain.Model;
using Nokody.API.Domain.Model.Models;

namespace Nokody.API.Controllers
{
  [Route("[controller]")]
  public class UserController : Controller
  {
    private readonly GetUsers _getUsers;
    private readonly AuthenticateUser _authenticateUser;
    private readonly UpdateDeviceId _updateDeviceId;
    private readonly AddUser _addUser;
    public UserController(GetUsers getFets, AuthenticateUser authenticateUser, UpdateDeviceId updateDeviceId, AddUser addUser)
    {
      _getUsers = getFets;
      _authenticateUser = authenticateUser;
      _updateDeviceId = updateDeviceId;
      _addUser = addUser;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
      try
      {
        var usersResult = await _getUsers.Execute();
        return StatusCode((int)usersResult.ResultType, usersResult.Object);
      }
      catch (System.Exception exc)
      {
        return StatusCode((int)ResultType.InternalServerError, exc.Message);
      }
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult> Authenticate([FromBody] UserIdentity userIdentity)
    {
      try
      {
        var authenticationResult = await _authenticateUser.Execute(userIdentity);
        return StatusCode((int)authenticationResult.ResultType, authenticationResult.Object);
      }
      catch (System.Exception exc)
      {
        return StatusCode((int)ResultType.InternalServerError, exc.Message);
      }
    }

    [HttpPost("updateDeviceId")]
    public async Task<ActionResult> UpdateUser([FromBody] User user)
    {
      try
      {
        var updateResult = await _updateDeviceId.Execute(user);
        return StatusCode((int)updateResult.ResultType, updateResult.Object);
      }
      catch (System.Exception exc)
      {
        return StatusCode((int)ResultType.InternalServerError, exc.Message);
      }
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddUser([FromBody] UserDetails userDetails)
    {
      try
      {
        var addUserResult = await _addUser.Execute(userDetails);
        return StatusCode((int)addUserResult.ResultType, addUserResult.Object);
      }
      catch (System.Exception exc)
      {
        return StatusCode((int)ResultType.InternalServerError, exc.Message);
      }
    }
  }
}
