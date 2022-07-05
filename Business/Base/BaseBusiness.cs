using DataAccess.Contracts;
using Model;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Base
{
    public class BaseBusiness<T> : IBaseBusiness<T> where T : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<T> _repository;
        public BaseBusiness (IUnitOfWork unitOfWork, IBaseRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;

        }
        public async Task<string> CreateAsync(T t, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(t, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return "request success";
        }

        public async Task<string> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _repository.DeleteByID(id, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return "deleted";
        }


        public async Task<string> UpdateAsync(T t, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(t, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return "Update success";
        }
    }
}
