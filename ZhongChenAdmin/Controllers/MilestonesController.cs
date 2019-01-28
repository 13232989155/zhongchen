using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using ZhongChen.Base;

namespace ZhongChenAdmin.Controllers
{
    public class MilestonesController : BaseController
    {

        public MilestonesBLL milestonesBLL = null;

        public MilestonesController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {
            this.milestonesBLL = new MilestonesBLL();
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
            IPagedList<MilestonesEntity> milestonesEntities = milestonesBLL.PageList(pageNumber, PageSize, searchString);
            return View(milestonesEntities);
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
        public IActionResult Create([Bind("contents")]MilestonesEntity milestonesEntity, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    milestonesEntity.adminId =-1;
                    milestonesEntity.createDate = DateTime.Now;
                    milestonesEntity.modifyDate = DateTime.Now;
                    milestonesEntity.img = img == null ? "" : UpFile(img);
                    milestonesEntity.contents = "";
                    milestonesBLL.ActionDal.ActionDBAccess.Insertable(milestonesEntity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(milestonesEntity);

        }

        /// <summary>
        /// 详细页面
        /// </summary>
        /// <param name="MilestonesId"></param>
        /// <returns></returns>
        public IActionResult Detail(int milestonesId)
        {
            return View(milestonesBLL.GetById(milestonesId));
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="milestonesId"></param>
        /// <returns></returns>
        public IActionResult Edit(int milestonesId)
        {
            return View(milestonesBLL.GetById(milestonesId));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="milestonesEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit([Bind("milestonesId")]MilestonesEntity milestonesEntity, IFormFile img, string isImg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MilestonesEntity milestones = milestonesBLL.GetById(milestonesEntity.milestonesId);
                    milestones.modifyDate = DateTime.Now;
                    if (string.IsNullOrWhiteSpace(isImg))
                    {
                        milestones.img = img == null ? "" : UpFile(img);
                    }
                    milestonesBLL.ActionDal.ActionDBAccess.Updateable(milestones).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(milestonesEntity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="milestonesId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int milestonesId)
        {
            try
            {
                MilestonesEntity milestonesEntity = milestonesBLL.GetById(milestonesId);
                milestonesBLL.ActionDal.ActionDBAccess.Deleteable(milestonesEntity).ExecuteCommand();
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