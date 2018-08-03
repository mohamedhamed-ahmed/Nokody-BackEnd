using System;
using System.Threading.Tasks;

namespace Nokody.API.Domain
{
  public interface IUnitOfWork
      : IDisposable
  {
    int Commit();
    Task<int> CommitAsync();
  }
}