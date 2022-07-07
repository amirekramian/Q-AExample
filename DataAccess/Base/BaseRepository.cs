using DataAccess.Contracts;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Model;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet; 
        private readonly ISieveProcessor _processor;

        public BaseRepository(Context.Context context, ISieveProcessor processor)
        {
            
            _processor = processor;
            _dbSet = context.Set<T>();
        }
        public async Task<T> CreateAsync(T t, CancellationToken cancellationToken) =>
            (await _dbSet.AddAsync(t, cancellationToken)).Entity;
        

        public async Task<T> DeleteAsync(T t, CancellationToken cancellationToken)=>
            (await Task.FromResult(_dbSet.Remove(t))).Entity;
        

        public async Task<T> DeleteByID(int ID, CancellationToken cancellationToken)
        {
            var query = await _dbSet.SingleOrDefaultAsync(x => x.ID == ID, cancellationToken);
            if(query !=null)
                return await DeleteAsync(query, cancellationToken);
            return Activator.CreateInstance<T>();
        }

        public async Task<T> UpdateAsync(T t, CancellationToken cancellationToken)
        {
            t.UpdatedDateTime = DateTime.Now;
            return (await Task.FromResult(_dbSet.Update(t))).Entity;
        }

        

         
    }
}
