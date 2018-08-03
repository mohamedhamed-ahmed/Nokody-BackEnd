using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nokody.API.Domain.Handlers.Accounts;
using Nokody.API.Domain.Model;

namespace Nokody.API.Controllers
{
  [Route("[controller]")]
  public class AccountController : Controller
  {
    private readonly ValidateAccountBalance _validateAccountBalance;
    private readonly GetAccount _getAccount;

    public AccountController(ValidateAccountBalance validateAccountBalance, GetAccount getAccount)
    {
      _validateAccountBalance = validateAccountBalance;
      _getAccount = getAccount;
    }

    [HttpGet("validate/{identification}/{amount}")]
    public async Task<ActionResult> Validate(string identification, float amount)
    {
      try
      {
        var validationResult = await _validateAccountBalance.Execute(identification, amount);
        return StatusCode((int)validationResult.ResultType, validationResult.Object);
      }
      catch (System.Exception exc)
      {
        return StatusCode((int)ResultType.InternalServerError, exc.Message);
      }
    }

    [HttpGet("{accountId}")]
    public async Task<ActionResult> Get(int accountId)
    {
      try
      {
        var account = await _getAccount.Execute(accountId);
        return StatusCode((int)account.ResultType, account.Object);
      }
      catch (System.Exception exc)
      {
        return StatusCode((int)ResultType.InternalServerError, exc.Message);
      }
    }

  }
}
