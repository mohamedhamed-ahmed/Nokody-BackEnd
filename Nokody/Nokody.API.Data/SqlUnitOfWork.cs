using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nokody.API.Domain;
using Nokody.API.Domain.Model.Models;

namespace Nokody.API.Data
{
  public class SqlUnitOfWork : IQueryableUnitOfWork
  {
    #region Members

    private readonly NokodyModel _context;

    #endregion

    #region Constructor

    //TODO: Use DI
    public SqlUnitOfWork(NokodyModel context)
    {
      _context = context;
    }

    #endregion

    #region IQueryableUnitOfWork members        

    public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
    {
      return _context.Set<TEntity>();
    }

    public void Attach<TEntity>(TEntity item) where TEntity : class
    {
      //attach and set as unchanged
      //attach automatically set to uncahnged ??
      _context.Entry(item).State = EntityState.Unchanged;
    }

    public void SetModified<TEntity>(TEntity item) where TEntity : class
    {
      _context.Entry(item).State = EntityState.Detached;
      _context.Entry(item).State = EntityState.Unchanged;
      _context.Entry(item).CurrentValues.Properties
          .ToList()
          .ForEach(p => _context.Entry(item).Property(p.Name).IsModified = true);
    }

    public void SetModified<TEntity>(TEntity item, string[] includedProperties) where TEntity : class
    {
      _context.Entry(item).State = EntityState.Detached;
      _context.Entry(item).State = EntityState.Unchanged;
      _context.Entry(item).CurrentValues.Properties
          .Where(p => includedProperties.Contains(p.Name))
          .ToList()
          .ForEach(p => _context.Entry(item).Property(p.Name).IsModified = true);
    }

    public int Commit()
    {
      var result = _context.SaveChanges();
      return result;
    }

    public async Task<int> CommitAsync()
    {
      var result = await _context.SaveChangesAsync();
      return result;
    }

    public void Dispose()
    {
      _context?.Dispose();
    }

    #endregion
  }
}