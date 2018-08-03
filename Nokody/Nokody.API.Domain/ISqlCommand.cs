using System;
using System.Data;
using System.Threading.Tasks;

namespace Nokody.API.Domain
{
  public interface ISqlCommand : IDisposable
  {
    int ExecuteCommand(string sqlCommand, params object[] parameters);
    Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters);
  }
}