using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhongChen.Base;
using ZhongChen.Models;

namespace ZhongChen.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {
            
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 生成实体文件
        /// </summary>
        public void DbFirst()
        {
            UserBLL userBLL = new UserBLL();

            userBLL.ActionDal.ActionDBAccess.DbFirst.Where("t_html_font_element").CreateClassFile("c:\\zhongchen");
        }
    }
}
