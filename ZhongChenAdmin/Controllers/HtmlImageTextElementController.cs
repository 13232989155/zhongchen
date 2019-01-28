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
    public class HtmlImageTextElementController : BaseController
    {

        public HtmlImageTextElementController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IActionResult List()
        {
            HtmlImageTextElementBLL htmlImageTextElementBLL = new HtmlImageTextElementBLL();

            List<HtmlImageTextElementEntity> htmlImageTextElementEntities = htmlImageTextElementBLL.List();
            return View(htmlImageTextElementEntities);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            HtmlImageTextElementBLL htmlImageTextElementBLL = new HtmlImageTextElementBLL();
            HtmlImageTextElementEntity htmlImageTextElementEntity = htmlImageTextElementBLL.GetById(id);
            return View(htmlImageTextElementEntity);
        }


        /// <summary>
        /// 保存编辑
        /// </summary>
        /// <param name="recipeEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(HtmlImageTextElementEntity htmlImageTextElementEntity, IFormFile img, string isImg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HtmlImageTextElementBLL htmlImageTextElementBLL = new HtmlImageTextElementBLL();

                    HtmlImageTextElementEntity htmlImageTextElement = htmlImageTextElementBLL.GetById(htmlImageTextElementEntity.id);
                    htmlImageTextElement.contents = htmlImageTextElementEntity.contents ?? "";
                    htmlImageTextElement.modifyDate = DateTime.Now;
                    htmlImageTextElement.adminId = -1;
                    if (string.IsNullOrWhiteSpace(isImg))
                    {
                        htmlImageTextElement.img = img == null ? "" : UpFile(img);
                    }
                    int rows = htmlImageTextElementBLL.ActionDal.ActionDBAccess.Updateable(htmlImageTextElement).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(htmlImageTextElementEntity);
        }
    }
}