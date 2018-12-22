using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZhongChen.ViewComponents
{
    public class ListRecipeStepViewComponent : ViewComponent
    {
        private readonly RecipeStepBLL recipeStepBLL;

        public ListRecipeStepViewComponent()
        {
            this.recipeStepBLL = new RecipeStepBLL();
        }

        /// <summary>
        /// 下拉选择框
        /// </summary>
        /// <param name="recipeId">食谱ID</param>
        /// <param name="viewName">组件名称（默认为：Name）</param>
        /// <param name="docName">组件Name属性（默认为：productTypeId）作为返回值接收</param>
        /// <param name="isOperation">是否显示操作</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(string viewName, int recipeId, bool? isOperation = true)
        {
            viewName = string.IsNullOrWhiteSpace(viewName) ? "List" : viewName;
            ViewBag.isOperation = isOperation;
            var List = await GetItemsAsync(recipeId);
            return View(viewName, List);
        }

        private Task<List<RecipeStepEntity>> GetItemsAsync(int recipeId)
        {
            return recipeStepBLL.ActionDal.ActionDBAccess.Queryable<RecipeStepEntity>().Where(it => it.recipeId == recipeId).ToListAsync();
        }
    }
}
