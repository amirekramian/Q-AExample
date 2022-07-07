using Business;
using Business.Businesses;
using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
    [System.Web.Mvc.Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserBusiness _userBusiness;
        public UserController(IBaseBusiness<User> userbusiness)
        {
            _userBusiness = userbusiness as UserBusiness;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<bool?> Login([FromForm] LoginViewModel login, CancellationToken cancellationToken)
        {
            try
            {
                return await _userBusiness!.LoginAsync(login, HttpContext , cancellationToken);
            }
            catch (Exception ex)
            {
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
                return RedirectToPage("/index");
            }
            catch
            {
                return Ok();
            }
        }
            

       
    }
}
