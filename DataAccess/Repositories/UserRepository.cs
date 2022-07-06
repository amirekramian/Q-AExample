using DataAccess.Base;
using Microsoft.EntityFrameworkCore;
using Model;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private readonly Context.Context _context;
        public UserRepository(Context.Context context, ISieveProcessor processor) : base(context, processor)
        {
            _context = context; 
        }

        public async Task<bool> IsUserNamePassworsValidasync(string username, string password , CancellationToken cancellationToken = new())=>
            await _context.Users!.AnyAsync(x =>
            x.UserName==username &&
            x.HashedPassword==password,
                cancellationToken);

        public async Task<bool> IsUserNameExistasync(string username, CancellationToken cancellationToken = new()) =>
            await _context.Users!
            .AnyAsync(x => string.Equals(x.UserName, username, StringComparison.CurrentCultureIgnoreCase),
                cancellationToken);

        public async Task<User> LoadUserByUserNameAsync(string username, CancellationToken cancellationToken = new()) =>
            await _context.Users!.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
    }

}
