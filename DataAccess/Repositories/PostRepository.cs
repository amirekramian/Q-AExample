using DataAccess.Base;
using DataAccess.Context;
using DataAccess.Contracts;
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
    public class PostRepository : BaseRepository<Post>
    {
        private readonly Context.Context _context;

        public PostRepository(Context.Context context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor) =>
            _context = context;

        public async Task<List<Post?>> DisplayAllPostByUserIDAsync(int UserID, CancellationToken cancellationToken = new()) =>
            await _context.Posts!.Where(x => x.UserID == UserID && x.IsDeleted==false).ToListAsync(cancellationToken);

        public async Task<Post?> DiplayPostByIDAsync(int PostID, CancellationToken cancellation = new()) =>
            await _context.Posts.FirstOrDefaultAsync(x => x.ID == PostID && x.IsDeleted==false);

        public async Task<Post?> DisplayLastUserPost(int userid, CancellationToken cancellationToken = new()) =>
            await _context.Posts!.Where(x => x.UserID == userid && x.IsDeleted==false).LastAsync(cancellationToken);
        public async Task<bool?> LikeAPost(int PostID, CancellationToken cancellation = new())
        {
            try
            {
                var selectedpost = _context.Posts.Where(x => x.ID == PostID).FirstOrDefault();
                selectedpost.LikeCount++;
                _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
            
    }
}
