using Model;

namespace WebApi.Contract
{
    public interface IBaseWebApiController<T> where T : BaseEntity
    {
        Task<string?> CreateAsync(T t, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<string?> UpdateAsync(T t, CancellationToken cancellationToken);
        Task<string?> DeleteAsync(int Id, CancellationToken cancellationToken);
    }
}
