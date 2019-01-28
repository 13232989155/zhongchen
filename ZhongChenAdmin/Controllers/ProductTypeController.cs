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
    public class ProductTypeController : BaseController
    {

        /// <summary>
        /// 产品分类逻辑层实例
        /// </summary>
        public ProductTypeBLL productTypeBLL = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProductTypeController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {
            this.productTypeBLL = new ProductTypeBLL();
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
            var list = productTypeBLL.PageList(pageNumber, PageSize, searchString);
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
        /// 创建管理员账号
        /// </summary>
        /// <param name="adminEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([Bind("name, title, subheading, recommendTitle")]ProductTypeEntity productTypeEntity, IFormFile coverImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productTypeEntity.adminId = -1;
                    productTypeEntity.createDate = DateTime.Now;
                    productTypeEntity.modifyDate = DateTime.Now;
                    productTypeEntity.recommend = false;
                    productTypeEntity.name = productTypeEntity.name ?? "";
                    productTypeEntity.title = productTypeEntity.title ?? "";
                    productTypeEntity.subheading = productTypeEntity.subheading ?? "";
                    productTypeEntity.recommendTitle = productTypeEntity.recommendTitle ?? "";
                    productTypeEntity.coverImage = coverImage == null ? "" : UpFile(coverImage);

                    productTypeBLL.ActionDal.ActionDBAccess.Insertable(productTypeEntity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(productTypeEntity);

        }

        /// <summary>
        /// 详细页面
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public IActionResult Detail(int productTypeId)
        {
            return View(productTypeBLL.GetById(productTypeId));
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public IActionResult Edit(int productTypeId)
        {
            return View(productTypeBLL.GetById(productTypeId));
        }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit([Bind("productTypeId,name, title, subheading, recommendTitle")]ProductTypeEntity productTypeEntity, IFormFile coverImage, string isCoverImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductTypeEntity entity = productTypeBLL.GetByName(productTypeEntity.name);
                    if (entity != null)
                    {
                        if (entity.productTypeId != productTypeEntity.productTypeId)
                        {
                            ViewBag.ex = "已存在该名称";
                            return View("Error");
                        }

                    }

                    entity = null;
                    entity = productTypeBLL.GetById(productTypeEntity.productTypeId);
                    entity.name = productTypeEntity.name;
                    entity.title = productTypeEntity.title ?? "";
                    entity.subheading = productTypeEntity.subheading ?? "";
                    entity.recommendTitle = productTypeEntity.recommendTitle ?? "";
                    entity.modifyDate = DateTime.Now;
                    if (string.IsNullOrWhiteSpace(isCoverImage))
                    {
                        entity.coverImage = coverImage == null ? "" : UpFile(coverImage);
                    }
                    productTypeBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(productTypeEntity);
        }


        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int productTypeId)
        {
            try
            {
                ProductTypeEntity productTypeEntity  = new ProductTypeEntity();
                productTypeEntity = productTypeBLL.GetById(productTypeId);
                productTypeBLL.ActionDal.ActionDBAccess.Deleteable(productTypeEntity).ExecuteCommand();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }

        }

        /// <summary>
        /// 推荐
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <param name="isRecommend"></param>
        /// <returns></returns>
        public IActionResult Recommend(int productTypeId, bool isRecommend)
        {
            try
            {
                ProductTypeEntity productTypeEntity = new ProductTypeEntity();
                productTypeEntity = productTypeBLL.GetById(productTypeId);

                productTypeEntity.recommend = isRecommend;
                productTypeEntity.modifyDate = DateTime.Now;
                productTypeBLL.ActionDal.ActionDBAccess.Updateable(productTypeEntity).ExecuteCommand();
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