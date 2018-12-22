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
    public class FaqController : BaseController
    {
        /// <summary>
        /// 管理员逻辑层实例
        /// </summary>
        public FaqBLL faqBLL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FaqController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {

            this.faqBLL = new FaqBLL();
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
            var list = faqBLL.PageList(pageNumber, PageSize, searchString);
            return View(list);
        }


        /// <summary>
        /// 创建页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="faqEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([Bind("title, contents")]FaqEntity faqEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    faqEntity.adminId = this.MustLogin().adminId;
                    faqEntity.createDate = DateTime.Now;
                    faqEntity.modifyDate = DateTime.Now;

                    faqBLL.ActionDal.ActionDBAccess.Insertable(faqEntity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(faqEntity);

        }

        /// <summary>
        /// 详细页面
        /// </summary>
        /// <param name="faqId"></param>
        /// <returns></returns>
        public IActionResult Detail(int faqId)
        {
            return View(faqBLL.GetById(faqId));
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="faqId"></param>
        /// <returns></returns>
        public IActionResult Edit(int faqId)
        {
            return View(faqBLL.GetById(faqId));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="adminEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit([Bind("faqId,title, contents")]FaqEntity faqEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FaqEntity entity = faqBLL.GetById(faqEntity.faqId);
                    entity.modifyDate = DateTime.Now;
                    entity.title = faqEntity.title;
                    entity.contents = faqEntity.contents;

                    faqBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(faqEntity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="faqId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int faqId)
        {
            try
            {
                FaqEntity faqEntity = faqBLL.GetById(faqId);
                faqBLL.ActionDal.ActionDBAccess.Deleteable(faqEntity).ExecuteCommand();
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