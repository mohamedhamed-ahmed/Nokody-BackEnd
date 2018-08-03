using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nokody.API.Domain.Model;
using Nokody.API.Domain.Model.Models;
using Nokody.API.Domain.Repositories;

namespace Nokody.API.Domain.Handlers.Users
{
  public class AuthenticateUser
  {
    private readonly IRepository<UserIdentity> _repository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Account> _accountRepository;

    public AuthenticateUser(IRepository<UserIdentity> repository, IRepository<User> userRepository, IRepository<Account> accountRepository)
    {
      _repository = repository;
      _userRepository = userRepository;
      _accountRepository = accountRepository;
    }

    public async Task<Result> Execute(UserIdentity userIdentity)
    {
      if (string.IsNullOrWhiteSpace(userIdentity.UserName) || string.IsNullOrWhiteSpace(userIdentity.Password)) return new Result { ResultType = ResultType.BadRequest, Object = $"{nameof(userIdentity.UserName)} and {nameof(userIdentity.Password)} are mandatory" };
      var userCredentials = await _repository.GetAll().FirstOrDefaultAsync(_ => _.UserName.Equals(userIdentity.UserName));

      if (userCredentials == null)
        return new Result { ResultType = ResultType.NotFound, Object = null };

      if (!userCredentials.Password.Equals(userIdentity.Password))
        return new Result { ResultType = ResultType.NotFound, Object = null };

      var userAccount = await _accountRepository.GetAll().Include(_ => _.User).Include(_ => _.User.UserType).FirstOrDefaultAsync(_ => _.User.UserIdentityId == userCredentials.Id);

      return new Result { ResultType = ResultType.Success, Object = userAccount };
    }
  }
}
