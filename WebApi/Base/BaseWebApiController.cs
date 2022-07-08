using Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using Model;
using WebApi.Contract;

namespace WebApi.Base
{
    public class BaseWebApiController<T> : ControllerBase, IBaseWebApiController<T> where T : BaseEntity
    {
        private readonly IBaseController<T>
        public Task<string?> CreateAsync(T t, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> DeleteAsync(int Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> UpdateAsync(T t, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
