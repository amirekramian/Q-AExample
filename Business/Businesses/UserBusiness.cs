using Business.Base;
using Common;
using DataAccess.Contracts;
using Model;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Business.Businesses
{
    public class UserBusiness : BaseBusiness<User>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserBusiness(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.UserRepository!)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> IsUserPasswordValidAsunc(LoginViewModel loginViewModel, CancellationToken cancellationToken = new()) =>
            await _unitOfWork.UserRepository!.IsUserNamePassworsValidasync(loginViewModel.UserName!,
                 loginViewModel.Password!.GetHashCode().ToString(), cancellationToken);

        public async Task<User> LoadUserByUserNameAsync(string username, CancellationToken cancellationToken = new()) =>
            await _unitOfWork.UserRepository!.LoadUserByUserNameAsync(username, cancellationToken);

        public async Task<bool?> LoginAsync(LoginViewModel login, HttpContext context, CancellationToken cancellationToken = new())
        {
            try
            {
                var IsValidUser = await IsUserPasswordValidAsunc(login, cancellationToken);
                if (!IsValidUser) return false;
                var user = await LoadUserByUserNameAsync(login.UserName!, cancellationToken);
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier,user.ID.ToString()),
                    new("UserName" , user.UserName!)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties
                {
                    IsPersistent = login.RememberMe
                };
                await context.SignInAsync(principal, properties);
                return true;
            }
            catch
            {
                return false;
            }
        }



}
}
