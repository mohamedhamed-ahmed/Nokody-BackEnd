using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nokody.API.Domain.Model;
using Nokody.API.Domain.Repositories;
using System.Linq;
using Nokody.API.Domain.Model.Models;

namespace Nokody.API.Domain.Handlers.Transactions
{
  public class GetTransactions
  {
    private readonly IRepository<Transaction> _repository;

    public GetTransactions(IRepository<Transaction> repository)
    {
      _repository = repository;
    }

    public async Task<Result> Execute(int accountId)
    {
      var transactions = await _repository.GetAll().Where(_ => _.FromAccountId == accountId || _.ToAccountId == accountId).ToListAsync();

      if (transactions == null)
        return new Result { ResultType = ResultType.NotFound, Object = "No transactions found." };

      return new Result { ResultType = ResultType.Success, Object = transactions };
    }

  }
}
