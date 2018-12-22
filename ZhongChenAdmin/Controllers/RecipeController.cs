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
using ZhongChen.Models;

namespace ZhongChen.Controllers
{
    public class RecipeController : BaseController
    {
        /// <summary>
        /// 食谱逻辑层实例
        /// </summary>
        public RecipeBLL recipeBLL = null;
        public RecipeStepBLL recipeStepBLL = null;
        public RecipeKeyBLL recipeKeyBLL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RecipeController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {
            this.recipeBLL = new RecipeBLL();
            this.recipeKeyBLL = new RecipeKeyBLL();
            this.recipeStepBLL = new RecipeStepBLL();
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
            var list = recipeBLL.PageList(pageNumber, PageSize, searchString);
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
        /// 创建食谱
        /// </summary>
        /// <param name="recipeEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([Bind("title, typeName, materials, explicitLink, remark")]RecipeEntity recipeEntity, IFormFile coverImage)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    recipeEntity.coverImage = coverImage == null ? "" : UpFile(coverImage);
                    recipeEntity.materials = string.IsNullOrWhiteSpace(recipeEntity.materials) ? "" : recipeEntity.materials;
                    recipeEntity.explicitLink = string.IsNullOrWhiteSpace(recipeEntity.explicitLink) ? "" : recipeEntity.explicitLink;
                    recipeEntity.remark = string.IsNullOrWhiteSpace(recipeEntity.remark) ? "" : recipeEntity.remark;
                    recipeEntity.adminId = this.MustLogin().adminId;
                    recipeEntity.createDate = DateTime.Now;
                    recipeEntity.modifyDate = DateTime.Now;

                    recipeBLL.ActionDal.ActionDBAccess.Insertable(recipeEntity).ExecuteCommand();
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
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public IActionResult Detail(int recipeId)
        {
            List<RecipeKeyEntity> listRecipeKey = recipeKeyBLL.List(recipeId);
            ViewBag.listRecipeKey = listRecipeKey;
            List<RecipeStepEntity> listRecipeStpe = recipeStepBLL.List( recipeId);
            ViewBag.listRecipeStpe = listRecipeStpe;
            return View(recipeBLL.GetById(recipeId));
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public IActionResult Edit(int recipeId)
        {
            return View(recipeBLL.GetById(recipeId));
        }

        /// <summary>
        /// 修改食谱
        /// </summary>
        /// <param name="recipeEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit([Bind("recipeId, title, coverImage, typeName, materials, explicitLink, remark")]RecipeEntity recipeEntity, IFormFile coverImage, string isCoverImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RecipeEntity entity = recipeBLL.GetById(recipeEntity.recipeId);

                    if (string.IsNullOrWhiteSpace(isCoverImage))
                    {
                        entity.coverImage = coverImage == null ? "" : UpFile(coverImage);
                    }
                    entity.materials = string.IsNullOrWhiteSpace(recipeEntity.materials) ? "" : recipeEntity.materials;
                    entity.explicitLink = string.IsNullOrWhiteSpace(recipeEntity.explicitLink) ? "" : recipeEntity.explicitLink;
                    entity.remark = string.IsNullOrWhiteSpace(recipeEntity.remark) ? "" : recipeEntity.remark;
                    entity.modifyDate = DateTime.Now;
                    entity.typeName = recipeEntity.typeName;
                    entity.title = recipeEntity.title;

                    recipeBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(recipeEntity);
        }


        /// <summary>
        /// 删除食谱
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int recipeId)
        {
            try
            {
                RecipeEntity recipeEntity = new RecipeEntity();
                recipeEntity = recipeBLL.GetById(recipeId);
                recipeBLL.ActionDal.ActionDBAccess.Deleteable(recipeEntity).ExecuteCommand();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }

        }

        /// <summary>
        /// 设置关键字页面
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public IActionResult Keyword(int recipeId)
        {

            var recipeKeyList = recipeKeyBLL.List(recipeId);
            ViewBag.recipeKeyList = recipeKeyList;

            return View(recipeBLL.GetById(recipeId));
        }

        /// <summary>
        /// 保存设置关键字
        /// </summary>
        /// <param name="recipeEntity">食谱</param>
        /// <param name="strKeyword">选中的值</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetKeyword(RecipeEntity recipeEntity, string[] strKeyword)
        {
            try
            {
                var recipeKeyList = recipeKeyBLL.List(recipeEntity.recipeId);
                recipeKeyBLL.ActionDal.ActionDBAccess.Deleteable(recipeKeyList).ExecuteCommand();

                List<RecipeKeyEntity> listRecipeKey = new List<RecipeKeyEntity>();

                foreach (var item in strKeyword)
                {
                    listRecipeKey.Add(new RecipeKeyEntity { recipeId = recipeEntity.recipeId, keywordId = Convert.ToInt32(item) });
                }
                recipeKeyBLL.ActionDal.ActionDBAccess.Insertable(listRecipeKey).ExecuteCommand();

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }
        }

        /// <summary>
        /// 设置步骤页面
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public IActionResult Step(int recipeId)
        {
            return View(recipeBLL.GetById(recipeId));
        }

        /// <summary>
        /// 获取食谱的步骤
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ListRecipeStep(int recipeId)
        {
            DataResult dr = new DataResult();

            try
            {
                var listRecipeStep = recipeStepBLL.List(recipeId);

                dr.code = "200";
                dr.data = listRecipeStep;
            }
            catch (Exception ex)
            {
                dr.code = "9999";
                dr.error = ex.Message.ToString();

            }

            return Json(dr);
        }

        /// <summary>
        /// 新增步骤
        /// </summary>
        /// <param name="recipeStepEntity"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public IActionResult SetRecipeStep(RecipeStepEntity recipeStepEntity, IFormFile formFile)
        {

            try
            {
                recipeStepEntity.contents = string.IsNullOrWhiteSpace(recipeStepEntity.contents) ? "" : recipeStepEntity.contents;
                recipeStepEntity.imageUrl = formFile == null ? "" : this.UpFile(formFile);

                recipeStepBLL.ActionDal.ActionDBAccess.Insertable(recipeStepEntity).ExecuteCommand();

                return View("Step", recipeBLL.GetById(recipeStepEntity.recipeId));
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }

        }

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <param name="recipeStepEntity"></param>
        /// <param name="formFile"></param>
        /// <param name="isImageUrl"></param>
        /// <returns></returns>
        public IActionResult EditRecipeStep( RecipeStepEntity recipeStepEntity, IFormFile formFile, string isImageUrl)
        {
            try
            {
                RecipeStepEntity entity = recipeStepBLL.GetById(recipeStepEntity.recipeStepId);

                if (string.IsNullOrWhiteSpace(isImageUrl))
                {
                    entity.imageUrl = formFile == null ? "" : this.UpFile(formFile);
                }
                entity.contents = string.IsNullOrWhiteSpace(recipeStepEntity.contents) ? "" : recipeStepEntity.contents;
                recipeStepBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                return View("Step", recipeBLL.GetById(entity.recipeId));
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }
        }

        /// <summary>
        /// 删除步骤
        /// </summary>
        /// <param name="recipeStepId"></param>
        /// <returns></returns>
        public IActionResult DelRecipeStep(int recipeStepId)
        {
            try
            {
                RecipeStepEntity recipeStepEntity = recipeStepBLL.GetById( recipeStepId);
                recipeStepBLL.ActionDal.ActionDBAccess.Deleteable(recipeStepEntity).ExecuteCommand();

                return View("Step", recipeBLL.GetById(recipeStepEntity.recipeId));
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }
        }

        /// <summary>
        /// 获取步骤
        /// </summary>
        /// <param name="recipeStepId"></param>
        /// <returns></returns>
        public JsonResult GetRecipeStepById( int recipeStepId)
        {
            DataResult dr = new DataResult();

            try
            {
                RecipeStepEntity recipeStepEntity = recipeStepBLL.GetById(recipeStepId);

                dr.code = "200";
                dr.data = recipeStepEntity;
            }
            catch (Exception ex)
            {
                dr.code = "9999";
                dr.error = ex.Message.ToString();

            }

            return Json(dr);
        }
    }
}