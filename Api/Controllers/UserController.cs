using Business;
using Business.Businesses;
using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
    [System.Web.Mvc.Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserBusiness _userBusiness;
        private readonly Logger _logger;
        public UserController(Logger logger ,IBaseBusiness<User> userbusiness)
        {
            _userBusiness = userbusiness as UserBusiness;
            _logger = logger;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<bool?> Login([FromForm] LoginViewModel login, CancellationToken cancellationToken)
        {
            try
            {
                return await _userBusiness!.LoginAsync(login, HttpContext , cancellationToken);
                _logger.Info($"User Logedin:{login.UserName}");
            }
            catch (Exception ex)
            {
                _logger.Error(new MongoLog
                {
                    ControllerName = nameof(UserController),
                    ActionName = nameof(Login),
                    Request = login,
                    Exception = ex,
                    Username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")
                    ?.Value
                }.LogFullData());
                return false;
            }
        }
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync();
                _logger.Info($"User Loged Out");
                return RedirectToPage("/index");
            }
            catch(Exception ex)
            {
                _logger.Error(new MongoLog
                {
                    ControllerName = nameof(UserController),
                    ActionName = nameof(Logout),
                    Exception = ex,
                    Username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")
                    ?.Value
                }.LogFullData());
                return Ok();
            }
        }
            

       
    }
}
