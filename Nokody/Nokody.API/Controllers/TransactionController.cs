using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nokody.API.Domain.Handlers.Transactions;
using Nokody.API.Domain.Model;

namespace Nokody.API.Controllers
{
  [Route("[controller]")]
  public class TransactionController : Controller
  {
    private readonly ExecuteTransaction _executeTransaction;
    private readonly GetTransactions _getTransactions;
    public TransactionController(ExecuteTransaction executeTransaction, GetTransactions getTransactios)
    {
      _executeTransaction = executeTransaction;
      _getTransactions = getTransactios;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody]TransactionSummary transactionSummary)
    {
      try
      {
        var executionResult = await _executeTransaction.Execute(transactionSummary.FromAccount, transactionSummary.ToAccount, transactionSummary.Amount, transactionSummary.PinCode);
        return StatusCode((int)executionResult.ResultType, executionResult.Object);
      }
      catch (System.Exception exc)
      {
        return StatusCode((int)ResultType.InternalServerError, exc.Message);
      }
    }


    [HttpGet("history/{accountId}")]
    public async Task<ActionResult> GetHistory(int accountId)
    {
      try
      {
        var getTransactiosResult = await _getTransactions.Execute(accountId);
        return StatusCode((int)getTransactiosResult.ResultType, getTransactiosResult.Object);
      }
      catch (System.Exception exc)
      {
        return StatusCode((int)ResultType.InternalServerError, exc.Message);
      }
    }
  }
}
