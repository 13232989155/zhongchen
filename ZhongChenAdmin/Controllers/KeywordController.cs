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
    public class KeywordController : BaseController
    {
        /// <summary>
        /// 关键字逻辑层实例
        /// </summary>
        public KeywordBLL keywordBLL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public KeywordController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {
            this.keywordBLL = new KeywordBLL();
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
            var list = keywordBLL.PageList(pageNumber, PageSize, searchString);
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
        /// 创建关键字
        /// </summary>
        /// <param name="keywordEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([Bind("contents,remark")]KeywordEntity keywordEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    keywordEntity.remark = string.IsNullOrWhiteSpace(keywordEntity.remark) ? "" : keywordEntity.remark;
                    keywordBLL.ActionDal.ActionDBAccess.Insertable(keywordEntity).ExecuteCommand();
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View();

        }

        /// <summary>
        /// 编辑关键字
        /// </summary>
        /// <param name="keywordId"></param>
        /// <returns></returns>
        public IActionResult Edit(int keywordId)
        {
            return View(keywordBLL.GetById(keywordId));
        }

        /// <summary>
        /// 修改关键字
        /// </summary>
        /// <param name="keywordEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit([Bind("keywordId,contents,remark")]KeywordEntity keywordEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    KeywordEntity entity = keywordBLL.GetById(keywordEntity.keywordId);

                    entity.contents = keywordEntity.contents;
                    entity.remark = string.IsNullOrWhiteSpace(keywordEntity.remark) ? "" : keywordEntity.remark;

                    keywordBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(keywordEntity);
        }


        /// <summary>
        /// 删除关键字
        /// </summary>
        /// <param name="keywordId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int keywordId)
        {
            try
            {
                KeywordEntity keywordEntity = new KeywordEntity();
                keywordEntity = keywordBLL.GetById(keywordId);
                keywordBLL.ActionDal.ActionDBAccess.Deleteable(keywordEntity).ExecuteCommand();
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