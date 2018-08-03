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
  public class AddUser
  {
    private readonly IRepository<User> _repository;
    private readonly IRepository<UserIdentity> _userIdentityRepository;
    private readonly IRepository<Account> _accountRepository;

    public AddUser(IRepository<User> repository, IRepository<UserIdentity> userIdentityRepository, IRepository<Account> accountRepository)
    {
      _repository = repository;
      _userIdentityRepository = userIdentityRepository;
      _accountRepository = accountRepository;
    }

    public async Task<Result> Execute(UserDetails userDetails)
    {
      var duplicateAccount = await _accountRepository.GetAll().FirstOrDefaultAsync(_ => _.PassportNumber == userDetails.PassportNumber || _.BraceletNumber == userDetails.BraceletNumber);

      if (duplicateAccount != null)
        return new Result { ResultType = ResultType.Duplicated, Object = $"There is already another account with the same identificaiton number." };

      _userIdentityRepository.Add(new UserIdentity
      {
        Password = userDetails.Password,
        UserName = userDetails.UserName,
      });

      var userIdentity = await _userIdentityRepository.GetAll().FirstOrDefaultAsync(_ => _.UserName.Equals(userDetails.UserName));

      _repository.Add(new User
      {
        Email = userDetails.Email,
        FirstName = userDetails.FirstName,
        LastName = userDetails.LastName,
        PhoneNumber = userDetails.PhoneNumber,
        UserIdentityId = userIdentity.Id,
        UserTypeId = userDetails.UserTypeId,
      });

      var user = await _repository.GetAll().FirstOrDefaultAsync(_ => _.FirstName.Equals(userDetails.FirstName) && _.LastName.Equals(userDetails.LastName) && _.Email.Equals(userDetails.Email));

      _accountRepository.Add(new Account
      {
        PassportNumber = userDetails.PassportNumber,
        BraceletNumber = userDetails.BraceletNumber,
        Balance = userDetails.Balance,
        PinCode = userDetails.PinCode,
        IsActive = true,
        OpenedDate = DateTime.Now,
        UserId = user.Id
      });

      await _userIdentityRepository.UnitOfWork.CommitAsync();
      await _repository.UnitOfWork.CommitAsync();
      await _repository.UnitOfWork.CommitAsync();

      return new Result { ResultType = ResultType.Success, Object = user };
    }
  }
}
