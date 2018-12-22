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
        /// <param name="productId">产品ID</param>
        /// <returns></returns>
        public IViewComponentResult Invoke( string searchString)
        {

            ViewBag.searchString = searchString;


            if ( string.IsNullOrWhiteSpace(searchString))
            {
                var list = recipeBLL.ActionDal.ActionDBAccess.Queryable<RecipeEntity>().ToList();

                return View("List", list);
            }
            else
            {
                var list = recipeBLL.ActionDal.ActionDBAccess.Queryable<RecipeEntity, RecipeKeyEntity, KeywordEntity>((re, rk, k) => new object[] { JoinType.Inner, re.recipeId == rk.recipeId, JoinType.Inner, rk.keywordId == k.keywordId })
                        .WhereIF( !string.IsNullOrWhiteSpace(searchString), (re, rk, k) => k.contents.Contains(searchString))
                        .Select((re, rk, k) => new RecipeEntity
                        {
                            coverImage = re.coverImage,
                            title = re.title,
                            recipeId = re.recipeId,
                            adminId = re.adminId,
                            createDate = re.createDate,
                            explicitLink = re.explicitLink,
                            materials = re.materials,
                            modifyDate = re.modifyDate,
                            remark = re.remark,
                            typeName = re.typeName
                        })
                        .OrderBy(re => re.recipeId, OrderByType.Desc)
                        .ToList();

                return View("List", list);
            }
            
        }

    }
}
