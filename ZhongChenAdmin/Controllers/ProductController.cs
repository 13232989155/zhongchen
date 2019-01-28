using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Common;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhongChen.Base;
using ZhongChen.Models;

namespace ZhongChen.Controllers
{
    public class ProductController : BaseController
    {

        /// <summary>
        /// 产品逻辑层实例
        /// </summary>
        public ProductBLL productBLL = null;
        public ProductImageBLL productImageBLL = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProductController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {
            this.productBLL = new ProductBLL();
            this.productImageBLL = new ProductImageBLL();
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
            var list = productBLL.PageList(pageNumber, PageSize, searchString);
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
        /// 创建商品
        /// </summary>
        /// <param name="adminEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([Bind("productTypeId,title,explain,buyLink,prop")]ProductEntity productEntity, IFormFile coverImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productEntity.adminId = -1;
                    productEntity.createDate = DateTime.Now;
                    productEntity.modifyDate = DateTime.Now;
                    productEntity.buyLink = string.IsNullOrWhiteSpace(productEntity.buyLink) ? "" : productEntity.buyLink;
                    productEntity.explain = string.IsNullOrWhiteSpace(productEntity.explain) ? "" : productEntity.explain;
                    productEntity.prop = string.IsNullOrWhiteSpace(productEntity.prop) ? "" : productEntity.prop;
                    productEntity.coverImage = coverImage == null ? "" : UpFile(coverImage);

                    productBLL.ActionDal.ActionDBAccess.Insertable(productEntity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(productEntity);

        }


        /// <summary>
        /// 详细页面
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult Detail(int productId)
        {
            return View(productBLL.GetById(productId));
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult Edit(int productId)
        {
            return View(productBLL.GetById(productId));
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit([Bind("productId,productTypeId,title,explain,buyLink,prop")]ProductEntity productEntity, IFormFile coverImage, string isCoverImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductEntity entity = productBLL.GetByTitle(productEntity.title);
                    if (entity != null)
                    {
                        if (entity.productId != productEntity.productId)
                        {
                            ViewBag.ex = "已存在该标题";
                            return View("Error");
                        }

                    }

                    entity = null;
                    entity = productBLL.GetById(productEntity.productId);
                    entity.buyLink = string.IsNullOrWhiteSpace(productEntity.buyLink) ? "" : productEntity.buyLink;
                    if (string.IsNullOrWhiteSpace(isCoverImage))
                    {
                        entity.coverImage = coverImage == null ? "" : UpFile(coverImage);
                    }
                    entity.explain = string.IsNullOrWhiteSpace(productEntity.explain) ? "" : productEntity.explain;
                    entity.productTypeId = productEntity.productTypeId;
                    entity.prop = string.IsNullOrWhiteSpace(productEntity.prop) ? "" : productEntity.prop;
                    entity.title = productEntity.title;
                    entity.modifyDate = DateTime.Now;

                    productBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(productEntity);
        }


        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            try
            {
                ProductEntity productEntity = new ProductEntity();
                productEntity = productBLL.GetById(productId);

                productImageBLL.DelByProductId( productId);
                productBLL.ActionDal.ActionDBAccess.Deleteable(productEntity).ExecuteCommand();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }
        }

        /// <summary>
        /// 获取产品图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ListProductImage( int productId)
        {
            DataResult dr = new DataResult();
            try
            {
                List<ProductImageEntity> list = new List<ProductImageEntity>();

                list = productImageBLL.ListProductImage(productId);

                dr.code = "200";
                dr.data = list;
                return Json(dr);
            }
            catch (Exception ex)
            {
                dr.code = "9999";
                dr.error = ex.Message.ToString();
                return Json(dr);
            }

        }

        /// <summary>
        /// 上传产品图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetProductImage( int productId, IFormFile imagePath)
        {
            DataResult dr = new DataResult();
            try
            {
                ProductImageEntity productImageEntity = new ProductImageEntity();
                productImageEntity.productId = productId;
                productImageEntity.imagePath = this.UpFile(imagePath);
                productImageBLL.ActionDal.ActionDBAccess.Insertable(productImageEntity).ExecuteCommand();

                List<ProductImageEntity> list = new List<ProductImageEntity>();

                list = productImageBLL.ListProductImage(productId);

                dr.code = "200";
                dr.data = list;
                return Json(dr);
            }
            catch (Exception ex)
            {
                dr.code = "9999";
                dr.error = ex.Message.ToString();
                return Json(dr);
            }
        }

        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteProductImage( int productImageId)
        {

            try
            {
                ProductImageEntity productImageEntity = new ProductImageEntity();
                productImageEntity = productImageBLL.GrtById(productImageId);
                productImageBLL.ActionDal.ActionDBAccess.Deleteable( productImageEntity).ExecuteCommand();

                return View("Edit", productBLL.GetById(productImageEntity.productId));
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }
        }
    }
}