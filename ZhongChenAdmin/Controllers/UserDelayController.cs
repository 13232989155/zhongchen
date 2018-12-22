using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhongChen.Base;

namespace ZhongChen.Controllers
{
    public class UserDelayController : BaseController
    {

        /// <summary>
        /// 用户逻辑层实例
        /// </summary>
        public UserDelayBLL userDelayBLL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserDelayController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {

            this.userDelayBLL = new UserDelayBLL();
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="searchString">搜索参数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public IActionResult List(string searchString, int? page)
        {

            ViewBag.searchString = searchString;
            int pageNumber = (page ?? 1);
            int PageSize = 10;
            var list = userDelayBLL.PageList(pageNumber, PageSize, searchString);
            return View(list);
        }


        /// <summary>
        /// 删除延保记录
        /// </summary>
        /// <param name="userDelayId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int userDelayId)
        {
            try
            {
                UserDelayEntity userDelayEntity = new UserDelayEntity();
                userDelayEntity = userDelayBLL.GetById(userDelayId);
                userDelayBLL.ActionDal.ActionDBAccess.Deleteable(userDelayEntity).ExecuteCommand();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }

        }
    }
}