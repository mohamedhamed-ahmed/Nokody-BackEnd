using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nokody.API.Domain;
using Nokody.API.Domain.Repositories;

namespace Nokody.API.Data.Repositories.Implementation
{
  public class Repository<TEntity> : IRepository<TEntity>
      where TEntity : class
  {
    protected readonly IQueryableUnitOfWork _unitOfWork;

    protected DbSet<TEntity> GetSet()
    {
      return _unitOfWork.CreateSet<TEntity>();
    }

    #region Constructor

    public Repository(IQueryableUnitOfWork unitOfWork)
    {
      if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
      _unitOfWork = unitOfWork;
    }

    #endregion

    #region IRepository Members

    public IUnitOfWork UnitOfWork => _unitOfWork;

    public Task<TEntity> GetAsync(object[] keyValues)
    {
      //IDbSet don`t have a FindAsync, a work around it to cast to Dbset losing the benifits of abstraction
      return keyValues != null ? ((DbSet<TEntity>)GetSet()).FindAsync(keyValues) : null;
    }

    public virtual IQueryable<TEntity> GetAll(bool readOnly = true)
    {
      if (readOnly)
        return GetSet().AsNoTracking();
      return GetSet();
    }

    public virtual void Add(TEntity item)
    {
      GetSet().Add(item); // add new item in this set
    }

    public virtual void Delete(TEntity item)
    {
      if (item != null)
      {
        //attach item if not exist
        _unitOfWork.Attach(item);

        //set as "removed"
        GetSet().Remove(item);
      }
    }

    public virtual void TrackItem(TEntity item)
    {
      if (item != null)
        _unitOfWork.Attach(item);
    }

    public virtual void Update(TEntity item)
    {
      //this operation also attach item in object state manager
      _unitOfWork.SetModified(item);
    }

    public virtual void Update(TEntity item, string[] includedProperties)
    {
      //this operation also attach item in object state manager
      _unitOfWork.SetModified(item, includedProperties);
    }

    #endregion
  }
}