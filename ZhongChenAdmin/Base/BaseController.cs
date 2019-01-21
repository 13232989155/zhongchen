using BLL;
using Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ZhongChen.Models;

namespace ZhongChen.Base
{
    public class BaseController : Controller
    {

        private IHttpContextAccessor _accessor;

        private readonly IHostingEnvironment _hostingEnvironment;

        public BaseController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment)
        {
            _accessor = accessor;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        /// <returns></returns>
        protected bool IsLogin()
        {
            int adminId = Convert.ToInt32( HttpContext.User.Claims.ToDictionary(it => it.Type, it => it.Value)["adminId"]);

            bool isLogin = false;

            if (adminId >= 10000)
            {
                isLogin = true;
            }

            //if (HttpContext.Session.Get<AdminEntity>("admin") != null)
            //{
            //    AdminEntity entity = HttpContext.Session.Get<AdminEntity>("admin");
            //    if (entity.adminId >= 10000)
            //    {
            //        isLogin = true;
            //    }
            //}

            return isLogin;
        }

        /// <summary>
        /// 返回当前登录用户
        /// </summary>
        /// <returns></returns>
        protected AdminEntity MustLogin()
        {
            if (!IsLogin())
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.Session.Remove("admin");
                Response.Redirect("/Login/Login");
            }
            int adminId = Convert.ToInt32(HttpContext.User.Claims.ToDictionary(it => it.Type, it => it.Value)["adminId"]);

            AdminBLL adminBLL = new AdminBLL();
            AdminEntity entity = adminBLL.GetById(adminId); //HttpContext.Session.Get<AdminEntity>("admin");
            return entity;
        }


        ///// <summary>
        ///// 获取Get方式提交的参数
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <returns></returns>
        //protected T CheckGet<T>()
        //{
        //    string json = Request["GetData"];

        //    return this.JsonStringToEntity<T>(json);
        //}

        ///// <summary>
        ///// 获取post方式提交的参数
        ///// </summary>
        ///// <typeparam name="T">类型</typeparam>
        ///// <returns></returns>
        //[ValidateInput(false)]
        //protected T CheckPost<T>()
        //{
        //    string json = HttpContext.Request Request.Params[0];

        //    return this.JsonStringToEntity<T>(json);
        //}

        /// <summary>
        /// 把数据转成json返回
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult DataToJson(dynamic data)
        {
            DataResult dr = new DataResult(data);
            return Json(dr);
        }

        /// <summary>
        /// 把数据转成json返回
        /// </summary>
        /// <param name="code">错误代码编号</param>
        /// <param name="error">错误提示</param>
        /// <returns></returns>
        protected JsonResult DataToJson(string code, string error)
        {
            DataResult dr = new DataResult(error, code);
            return Json(dr);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        protected string UpFile(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString("n");
            string systemPath = _hostingEnvironment.WebRootPath;
            // 原文件名（包括路径）
            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
            // 扩展名
            var extName = filename.Substring(filename.LastIndexOf('.')).Replace("\"", "");

            string path = "/images/" + fileName + extName;

            using (var stream = new FileStream(systemPath + path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var httpcontext = _accessor.HttpContext;

            return httpcontext.Request.Scheme + "://" + httpcontext.Request.Host + path; 
        }

        /// <summary>
        /// 字符串转数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string[] StrToList(string str)
        {
            string[] arr = null;
            str.Trim();
            str.Trim('"');
            arr = str.Split(',');
            return arr;
        }

        


    }
}
