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
    public class RecipeDetailViewComponent : ViewComponent
    {
        private readonly RecipeBLL recipeBLL;

        public RecipeDetailViewComponent()
        {
            this.recipeBLL = new RecipeBLL();
        }

        /// <summary>
        /// 食谱详细
        /// </summary>
        /// <param name="productId">产品ID</param>
        /// <returns></returns>
        public IViewComponentResult Invoke(int recipeId )
        {
            var recipeStepList = recipeBLL.ActionDal.ActionDBAccess.Queryable<RecipeStepEntity>().Where(it => it.recipeId == recipeId).ToList();
            ViewBag.recipeStepList = recipeStepList;

            var keyList = recipeBLL.ActionDal.ActionDBAccess.Queryable<RecipeEntity, RecipeKeyEntity, KeywordEntity>((re, rk, k) => new object[] { JoinType.Inner, re.recipeId == rk.recipeId, JoinType.Inner, rk.keywordId == k.keywordId })
                                .Where((re, rk, k) => re.recipeId == recipeId)
                                .Select((re, rk, k) => new KeywordEntity {
                                    contents = k.contents,
                                    keywordId = k.keywordId,
                                    remark = k.remark
                                })
                                .ToList();
            ViewBag.keyList = keyList;

            var entity = recipeBLL.ActionDal.ActionDBAccess.Queryable<RecipeEntity>().Where(it => it.recipeId == recipeId).First();

            return View("Detail", entity);
           

        }

    }
}
