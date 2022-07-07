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
    public class CommentRepository : BaseRepository<Comment>
    {
        private readonly Context.Context _context;

        public CommentRepository(Context.Context context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor) =>
            _context = context;

        public async Task<List<Comment?>> DisplayAllPostCommentsAsync(int postid, CancellationToken cancellationToken = new()) =>
            await _context.Comments!.Where(x => x.PostID == postid).ToListAsync(cancellationToken);

        public async Task<List<Comment>> DisplaySortedCommentsByLike(int postid, CancellationToken cancellationToken = new()) =>
            await _context.Comments.Where(x=>x.PostID == postid).OrderByDescending(x=>x.CommentLikeCount).ToListAsync(cancellationToken);


    }

}
