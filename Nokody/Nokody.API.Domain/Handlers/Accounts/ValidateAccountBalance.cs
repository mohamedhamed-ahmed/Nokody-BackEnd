using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nokody.API.Domain.Model;
using Nokody.API.Domain.Model.Models;
using Nokody.API.Domain.Repositories;

namespace Nokody.API.Domain.Handlers.Accounts
{
  public class ValidateAccountBalance
  {
    private readonly IRepository<Account> _repository;

    public ValidateAccountBalance(IRepository<Account> repository)
    {
      _repository = repository;
    }

    public async Task<Result> Execute(string identification, float amount)
    {
      var account = await _repository.GetAll().FirstOrDefaultAsync(_ => _.BraceletNumber == identification || _.PassportNumber == identification);

      if (account == null)
        return new Result { ResultType = ResultType.NotFound, Object = $"Account with id {identification} was not found." };

      if (account.Balance < amount)
        return new Result { ResultType = ResultType.BadRequest, Object = $"Account with id {identification} does not have enough credit." };

      return new Result { ResultType = ResultType.Success, Object = account.PinCode };
    }
  }
}
