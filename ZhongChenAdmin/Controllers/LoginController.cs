using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL;
using Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhongChen.Base;
using ZhongChen.Models;

namespace ZhongChen.Controllers
{
    public class LoginController : BaseController
    {

        #region 实例/变量

        /// <summary>
        /// 管理员逻辑层实例
        /// </summary>
        public AdminBLL adminAll = null;

        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {
            this.adminAll = new AdminBLL();
        }


        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SignIn([FromForm] string account, string password)
        {
            AdminEntity entity = new AdminEntity();
            entity = this.adminAll.GetByAccountAndPassword(account, password);
            if (entity != null && entity.adminId > 0)
            {

                var user = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, entity.ToString()),
                    new Claim("adminId", entity.adminId.ToString()),
                    new Claim(ClaimTypes.Role, "Administrator"),
                },
                CookieAuthenticationDefaults.AuthenticationScheme));
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(7)) // 有效时间
                });


                //HttpContext.Session.Set<AdminEntity>("admin", entity);
                return this.DataToJson(entity);

            }
            else
            {
                return this.DataToJson(ErrorMsg.ADMIN_ACCOUNT_CODE
                    , ErrorMsg.ADMIN_ACCOUNT_ERROR_MESSAGE);
            }
        }


        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("admin");

            return Redirect("/Login/Login");
        }
    }
}