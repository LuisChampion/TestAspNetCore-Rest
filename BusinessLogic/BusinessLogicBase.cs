using BusinessLogic.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace BusinessLogic
{
    public abstract class BusinessLogicBase<TEntidad> : IBusinessLogic<TEntidad>, IBusinessLogicAsync<TEntidad> where TEntidad : class
    {
        protected ApplicationDbContextActivities _Context;

        public BusinessLogicBase()
        {
            _Context = new ApplicationDbContextActivities(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContextActivities>());
            this.Entity = _Context.Set<TEntidad>();
        }

        protected DbSet<TEntidad> Entity { get; private set; }

        public virtual void Delete(TEntidad entidad)
        {
            throw new NotImplementedException();
        }

        public virtual int Save()
        {
            if (this._Context != null)
            {
                return this._Context.SaveChanges();
            }
            return 0;
        }

        public virtual void Update(TEntidad entidad)
        {
            this._Context?.Update(entidad);            
        }
        public List<TEntidad> Get()
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> SaveAsync()
        {
            return this._Context?.SaveChangesAsync();
        }

        public void UpdateAsync(TEntidad entidad)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(TEntidad entidad)
        {
            throw new NotImplementedException();
        }
        public virtual Task<List<TEntidad>> GetAsync()
        {
            return this.Entity?.ToListAsync();
        }

        public virtual Task<TEntidad> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual TEntidad Add(TEntidad entidad)
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<TEntidad> AddAsync(TEntidad entidad)
        {
            throw new NotImplementedException();
        }
        
    }
}
