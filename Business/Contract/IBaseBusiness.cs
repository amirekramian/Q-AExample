using Model;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IBaseBusiness<T> where T : BaseEntity
    {
        Task<string> CreateAsync(T t, CancellationToken cancellationToken);
        Task<string> UpdateAsync(T t, CancellationToken cancellationToken);
        Task<string> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
