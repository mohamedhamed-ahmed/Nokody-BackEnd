using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nokody.API.Domain.Model;
using Nokody.API.Domain.Repositories;

namespace Nokody.API.Domain.Handlers.Users
{
  public class GetUsers
  {
    private readonly IRepository<Model.Models.User> _repository;

    public GetUsers(IRepository<Model.Models.User> repository)
    {
      _repository = repository;
    }

    public async Task<Result> Execute()
    {
      var users = await _repository.GetAll().ToListAsync();

      if (users == null)
        return new Result { ResultType = ResultType.NotFound, Object = "No clients found." };

      return new Result { ResultType = ResultType.Success, Object = users };
    }
  }
}
