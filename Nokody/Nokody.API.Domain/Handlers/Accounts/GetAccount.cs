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
  public class GetAccount
  {
    private readonly IRepository<Account> _repository;

    public GetAccount(IRepository<Account> repository)
    {
      _repository = repository;
    }

    public async Task<Result> Execute(int accountId)
    {
      var account = await _repository.GetAll().FirstOrDefaultAsync(_ => _.Id == accountId);

      if (account == null)
        return new Result { ResultType = ResultType.NotFound, Object = $"No account with id {accountId} was found" };

      return new Result { ResultType = ResultType.Success, Object = account };
    }
  }
}
