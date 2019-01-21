using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zhongchen.ViewComponents
{
    public class RecipeListViewComponent : ViewComponent
    {
        private readonly RecipeBLL recipeBLL;

        public RecipeListViewComponent()
        {
            this.recipeBLL = new RecipeBLL();
        }

        /// <summary>
        /// 食谱列表
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            string recipeTitle = string.Empty;
            string videoTitle = string.Empty;
            HtmlFontElementBLL htmlFontElementBLL = new HtmlFontElementBLL();
            recipeTitle = htmlFontElementBLL.GetByKey("食谱页食谱标题").value;
            videoTitle = htmlFontElementBLL.GetByKey("食谱页视频标题").value;
            ViewBag.recipeTitle = recipeTitle;
            ViewBag.videoTitle = videoTitle;

            List<VideoEntity> videoEntities = recipeBLL.ActionDal.ActionDBAccess.Queryable<VideoEntity>()
                                                .Where( it => it.typeName == "食谱")
                                                .OrderBy(it => it.videoId, SqlSugar.OrderByType.Desc).ToList();
            ViewBag.videoEntities = videoEntities;

            var list = recipeBLL.ActionDal.ActionDBAccess.Queryable<RecipeEntity>().ToList();
            return View("List", list);
        }

    }
}
