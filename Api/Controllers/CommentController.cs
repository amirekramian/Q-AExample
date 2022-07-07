using Business;
using Business.Businesses;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly CommentBusiness _commentBusiness;
        public CommentController(IBaseBusiness<Comment> commentbusiness)
        {
            _commentBusiness = commentbusiness as CommentBusiness;
        }
        public async Task<List<Comment?>> DisplayAllPostComments(int postid , CancellationToken cancellationToken)=>
            await _commentBusiness.DisplayAllPostCommentsAsync(postid, cancellationToken);

        public async Task<List<Comment?>> DisplayCommentsSortedByLike(int postid, CancellationToken cancellationToken) =>
            await _commentBusiness.DisplayCommentsSortedByLike(postid, cancellationToken);
    }
}
