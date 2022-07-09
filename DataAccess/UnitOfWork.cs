using DataAccess.Contracts;
using DataAccess.Repositories;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork


    {
        private UserRepository? _userRepository;

        private PostRepository? _postRepository;

        private CommentRepository? _commentRepository;

        private readonly Context.Context _context;

        private readonly ISieveProcessor _sieveProcessor;

        public UnitOfWork(Context.Context context , ISieveProcessor sieveProcessor)
        {
            _context = context;
            _sieveProcessor = sieveProcessor;
        }
        public UserRepository? UserRepository =>
            _userRepository ??= new UserRepository(_context, _sieveProcessor);

        public PostRepository PostRepository => 
            _postRepository = new PostRepository(_context, _sieveProcessor);
        public CommentRepository CommentRepository => 
            _commentRepository ??= new CommentRepository(_context, _sieveProcessor);

        public async Task<int> CommitAsync(CancellationToken cancellationToken) {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        void IUnitOfWork.commit()
        {
            _context.SaveChanges();
        }
    }
}
