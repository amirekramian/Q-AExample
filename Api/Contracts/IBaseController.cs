using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Contracts
{
    public interface IBaseController<T> where T : BaseEntity
    {
        Task<string?> CreateAsync(T t , CancellationToken cancellationToken);
        //Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<string?> UpdateAsync(T t, CancellationToken cancellationToken);
        Task<string?> DeleteAsync(int Id,CancellationToken cancellationToken);
        void Option();
    }
}
