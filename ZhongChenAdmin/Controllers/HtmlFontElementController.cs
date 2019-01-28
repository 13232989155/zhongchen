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

namespace ZhongChenAdmin.Controllers
{
    public class HtmlFontElementController : BaseController
    {
        public HtmlFontElementController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IActionResult List()
        {
            HtmlFontElementBLL htmlFontElementBLL = new HtmlFontElementBLL();

            List<HtmlFontElementEntity> htmlFontElementEntities = htmlFontElementBLL.List();
            return View(htmlFontElementEntities);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            HtmlFontElementBLL htmlFontElementBLL = new HtmlFontElementBLL();
            HtmlFontElementEntity htmlFontElementEntity = htmlFontElementBLL.GetById(id);
            return View(htmlFontElementEntity);
        }


        /// <summary>
        /// 保存编辑
        /// </summary>
        /// <param name="recipeEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(HtmlFontElementEntity htmlFontElementEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HtmlFontElementBLL htmlFontElementBLL = new HtmlFontElementBLL();

                    HtmlFontElementEntity htmlFontElement = htmlFontElementBLL.GetById(htmlFontElementEntity.id);
                    htmlFontElement.value = htmlFontElementEntity.value;
                    htmlFontElement.modifyDate = DateTime.Now;
                    htmlFontElement.adminId = -1;
                    int rows = htmlFontElementBLL.ActionDal.ActionDBAccess.Updateable(htmlFontElement).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(htmlFontElementEntity);
        }

    }
}