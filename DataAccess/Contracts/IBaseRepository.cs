using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sieve.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Contracts
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T t, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T t, CancellationToken cancellationToken);
        Task<T> DeleteAsync(T t, CancellationToken cancellationToken);
        Task<T> DeleteByID(int ID, CancellationToken cancellationToken);
        //Task<List<T>> GetAll(CancellationToken cancellationToken);

    }
}
