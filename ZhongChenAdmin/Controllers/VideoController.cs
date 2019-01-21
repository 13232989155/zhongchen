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
    public class VideoController : BaseController
    {
        /// <summary>
        /// 用户逻辑层实例
        /// </summary>
        public VideoBLL videoBLL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public VideoController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {

            this.videoBLL = new VideoBLL();
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
            var list = videoBLL.PageList(pageNumber, PageSize, searchString);
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
        /// 创建视频
        /// </summary>
        /// <param name="videoEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([Bind("title, typeName, URL")]VideoEntity videoEntity, IFormFile coverImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    videoEntity.adminId = this.MustLogin().adminId;
                    videoEntity.coverImage = coverImage == null ? "" : UpFile(coverImage);
                    videoEntity.createDate = DateTime.Now;
                    videoEntity.modifyDate = DateTime.Now;
                    videoEntity.bill = "" ;

                    videoBLL.ActionDal.ActionDBAccess.Insertable(videoEntity).ExecuteCommand();

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
        /// 详细页面
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public IActionResult Detail(int videoId)
        {
            return View(videoBLL.GetById(videoId));
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IActionResult Edit(int videoId)
        {
            return View(videoBLL.GetById(videoId));
        }

        /// <summary>
        /// 修改视频
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit([Bind("videoId,title, typeName, URL")]VideoEntity videoEntity, IFormFile coverImage, string isCoverImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    VideoEntity entity = videoBLL.GetById(videoEntity.videoId);

                    if (string.IsNullOrWhiteSpace(isCoverImage))
                    {
                        entity.coverImage = coverImage == null ? "" : UpFile(coverImage);
                    }
                    entity.modifyDate = DateTime.Now;
                    entity.title = videoEntity.title;
                    entity.typeName = videoEntity.typeName;
                    entity.URL = videoEntity.URL;

                    videoBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(videoEntity);
        }


        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int videoId)
        {
            try
            {
                VideoEntity videoEntity = new VideoEntity();
                videoEntity = videoBLL.GetById(videoId);
                videoBLL.ActionDal.ActionDBAccess.Deleteable(videoEntity).ExecuteCommand();
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