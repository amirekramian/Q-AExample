using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Business.Businesses;
using Common;
using Microsoft.AspNetCore.Mvc;
using Model;
using NLog;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostBusiness? _postbusiness;
        private readonly Logger _logger;

        public PostController(Logger logger,IBaseBusiness<Post> postBusiness)
        {
            _postbusiness = postBusiness as PostBusiness;
            _logger = logger;
        }

        [HttpGet]
        [Route("ShowAPost")]
        public async Task<Post> LoadPostByIDAsync(int postid, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info($"postid {postid} loaded");
                return await _postbusiness!.DiplayPostByIDAsync(postid, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Error(new MongoLog
                {
                    ControllerName = nameof(PostController),
                    ActionName = nameof(LoadPostByIDAsync),
                    Request = postid,
                    Exception = ex,
                    Username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")
                    ?.Value
                }.LogFullData());
                return null;
            }
            

        }

        [HttpGet]
        [Route("ShowAllPost")]
        public async Task<List<Post>> LoadUserPosts(int userId , CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info($"All userID {userId} loaded");
                return await _postbusiness!.DisplayAllPostByUserIDAsync(userId, cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.Error(new MongoLog
                {
                    ControllerName = nameof(UserController),
                    ActionName = nameof(LoadUserPosts),
                    Request = userId,
                    Exception = ex,
                    Username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")
                    ?.Value
                }.LogFullData());
                return null;
            }
        }
            

        [HttpPut]
        [Route("LikePost")]
        public async Task<bool?> LikePost(int postid, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info($"postid {postid} Liked");
                return await _postbusiness!.LikeAPost(postid, cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.Error(new MongoLog
                {
                    ControllerName = nameof(UserController),
                    ActionName = nameof(LikePost),
                    Request = postid,
                    Exception = ex,
                    Username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")
                                    ?.Value
                }.LogFullData());
                return false;
            }
            
        }
    }
}
