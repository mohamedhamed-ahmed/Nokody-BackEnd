using System.Collections.Generic;

namespace Nokody.API.Domain
{
  public interface IEntityValidator
  {
    bool IsValid<TEntity>(TEntity item)
        where TEntity : class;

    ICollection<string> GetInvalidMessages<TEntity>(TEntity item)
        where TEntity : class;
  }
}