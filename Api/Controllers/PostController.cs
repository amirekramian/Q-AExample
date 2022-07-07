using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Business.Businesses;
using Microsoft.AspNetCore.Mvc;
using Model;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostBusiness? _postbusiness;
        public PostController(IBaseBusiness<Post> postBusiness)
        {
            _postbusiness = postBusiness as PostBusiness;
        }

        [HttpGet]
        [Route("ShowAPost")]
        public async Task<Post> LoadPostByIDAsync(int postid, CancellationToken cancellationToken)=>
            await _postbusiness!.DiplayPostByIDAsync(postid, cancellationToken);

        [HttpGet]
        [Route("ShowAllPost")]
        public async Task<List<Post>> LoadUserPosts(int userId , CancellationToken cancellationToken)=>
            await _postbusiness!.DisplayAllPostByUserIDAsync(userId, cancellationToken);

        [HttpPut]
        [Route("LikePost")]
        public async Task<bool?> LikePost(int postid, CancellationToken cancellationToken) =>
            await _postbusiness!.LikeAPost(postid, cancellationToken);
    }
}
