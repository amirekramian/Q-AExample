using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        UserRepository? UserRepository { get; }
        UserRepository PostRepository { get; }
        CommentRepository CommentRepository { get; }
        void commit();
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
