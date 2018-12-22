using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zhongchen.ViewComponents
{
    public class RecipeViewComponent : ViewComponent
    {
        private readonly RecipeBLL recipeBLL;

        public RecipeViewComponent()
        {
            this.recipeBLL = new RecipeBLL();
        }

        /// <summary>
        /// 食谱
        /// </summary>
        /// <param name="pViewName">视图名称</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(string pViewName)
        {

            string viewName = string.Empty;

            if (string.IsNullOrWhiteSpace(pViewName))
            {
                viewName = "Main";
            }
            else
            {
                viewName = pViewName;
            }

            var List = await GetItemsAsync(viewName);
            return View(viewName, List);
        }

        private Task<List<RecipeEntity>> GetItemsAsync(string viewName)
        {
            if (viewName == "Main")
            {
                return recipeBLL.ActionDal.ActionDBAccess.Queryable<RecipeEntity>().Take(3).ToListAsync();
            }
            return recipeBLL.ActionDal.ActionDBAccess.Queryable<RecipeEntity>().ToListAsync();
        }

    }
}
