using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected readonly MyContext Context;
        private DbSet<T> DataSet;

        public BaseRepository(MyContext context)
        {
            Context = context;
            DataSet = Context.Set<T>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await DataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if(result == null)
                    return false;

                DataSet.Remove(result);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if(item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }

                item.CreateAt = DateTime.UtcNow;
                DataSet.Add(item);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return item;
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await DataSet.AnyAsync(p => p.Id.Equals(id));
        }
        
        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await DataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await DataSet.ToListAsync();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await DataSet.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if(result == null)
                    return null;
                
                item.UpdateAt = DateTime.UtcNow;

                item.CreateAt = result.CreateAt;
                Context.Entry(result).CurrentValues.SetValues(item);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {                
                throw ex;
            }

            return item;
        }
    }
}