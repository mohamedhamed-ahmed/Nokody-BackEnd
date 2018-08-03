using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nokody.API.Domain.Model;
using Nokody.API.Domain.Model.Models;
using Nokody.API.Domain.Repositories;
using System.Linq;

namespace Nokody.API.Domain.Handlers.Users
{
  public class GetUser
  {
        private readonly IRepository<User> _repository;

        public GetUser(IRepository<Model.Models.User> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Execute(int userId)
        {
            var user = await _repository.GetAll().Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (user == null)
                return new Result { ResultType = ResultType.NotFound, Object = "No clients found." };

            return new Result { ResultType = ResultType.Success, Object = user };
        }
    }
}
