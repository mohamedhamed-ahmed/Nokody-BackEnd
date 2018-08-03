using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nokody.API.Domain.Model;
using Nokody.API.Domain.Model.Models;
using Nokody.API.Domain.Repositories;

namespace Nokody.API.Domain.Handlers.Transactions
{
  public class ExecuteTransaction
  {
    private readonly IRepository<Transaction> _repository;
    private readonly IRepository<Account> _accountRepository;

    public ExecuteTransaction(IRepository<Transaction> repository, IRepository<Account> accountRepository)
    {
      _repository = repository;
      _accountRepository = accountRepository;
    }

    public async Task<Result> Execute(string fromAccount, string toAccount, float amount, int pinCode)
    {
      var accounts = await _accountRepository.GetAll().Include("User").Where(_ => _.PassportNumber == fromAccount || _.BraceletNumber == fromAccount || _.PassportNumber == toAccount || _.BraceletNumber == toAccount).ToListAsync();

      if (accounts == null || accounts.Count < 2)
        return new Result { ResultType = ResultType.NotFound, Object = $"Please provide a valid from account and to account" };

      var from = accounts.FirstOrDefault(_ => (_.PassportNumber == fromAccount || _.BraceletNumber == fromAccount) && _.PinCode == pinCode);

      if (from == null)
        return new Result { ResultType = ResultType.BadRequest, Object = $"Invalid Pin Code." };

      var to = accounts.FirstOrDefault(_ => _.PassportNumber == toAccount || _.BraceletNumber == toAccount);

      if (from.Balance < amount)
        return new Result { ResultType = ResultType.BadRequest, Object = $"Account with id {fromAccount} does not have enough credit." };

      from.Balance -= amount;
      to.Balance += amount;

      _accountRepository.Update(from, new[] { "Balance" });
      _accountRepository.Update(to, new[] { "Balance" });

      _repository.Add(new Transaction
      {
        Amount = amount,
        Date = DateTime.Now,
        FromAccountId = from.Id,
        ToAccountId = to.Id,
        IsSuccessful = true
      });

      await _accountRepository.UnitOfWork.CommitAsync();
      await _repository.UnitOfWork.CommitAsync();

      SendNotification(from.User.DeviceId, $" Debited for {amount}{Environment.NewLine}Current Balance: {from.Balance}");
      SendNotification(to.User.DeviceId, $" Credited for {amount}{Environment.NewLine}Current Balance: {from.Balance}");

      return new Result { ResultType = ResultType.Success, Object = new TransactionSummary { FromAccount = fromAccount, ToAccount = toAccount, Amount = amount } };
    }

    private string SendNotification(string deviceId, string message)
    {
      var result = "-1";
      var webAddr = "https://fcm.googleapis.com/fcm/send";
      var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
      httpWebRequest.ContentType = "application/json";
      httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=AAAAiRjhurs:APA91bEjB4GzQHKLzqUO0CiWgu8ZvSCYi2wcNEVhIjd7qRonE5xn-F_Op8oZ0fvqQY5IrJWUfVDbtU7Dqu9VdLA0hebfNuygPmsBOJU23TKRfixpMkgHuK8hvu-spG_3Ny6XMZDS8eqzPhzCbb0Qmf6wgmIi5TTqDA");
      httpWebRequest.Method = "POST";
      using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
      {
        string strNJson = @"{
                                    ""to"": """ + deviceId + @""",
                                    ""data"": {
                                        ""ShortDesc"": ""Some short desc"",
                                        ""IncidentNo"": ""588827966139"",
                                        ""Description"": ""detail desc""
                                      },
                                      ""notification"": {
                                                    ""title"": ""Transaction successful"",
                                        ""text"": """ + message + @""",
                                    ""sound"":""default""
                                      }
                                            }";
        streamWriter.Write(strNJson);
        streamWriter.Flush();
      }

      var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
      using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
      {
        result = streamReader.ReadToEnd();
      }
      return result;
    }



  }
}
