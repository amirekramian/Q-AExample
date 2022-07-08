using Business;
using Business.Businesses;
using Common;
using Microsoft.AspNetCore.Mvc;
using Model;
using NLog;
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
        private readonly Logger _logger;

        public CommentController(Logger logger,IBaseBusiness<Comment> commentbusiness)
        {
            _commentBusiness = commentbusiness as CommentBusiness;
            _logger = logger;
        }
        public async Task<List<Comment?>> DisplayAllPostComments(int postid , CancellationToken cancellationToken)=>
            await _commentBusiness.DisplayAllPostCommentsAsync(postid, cancellationToken);

        public async Task<List<Comment?>> DisplayCommentsSortedByLike(int postid, CancellationToken cancellationToken)
        {
            try
            {
                return await _commentBusiness.DisplayCommentsSortedByLike(postid, cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.Error(new MongoLog
                {
                    ControllerName = nameof(UserController),
                    ActionName = nameof(DisplayCommentsSortedByLike),
                    Request = postid,
                    Exception = ex,
                    Username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")
                    ?.Value
                }.LogFullData());
                return null;
            }

        }
    }
}
