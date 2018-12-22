using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZhongChen.ViewComponents
{
    public class KeywordCheckViewComponent : ViewComponent
    {
        private readonly KeywordBLL keywordBLL;

        public KeywordCheckViewComponent()
        {
            this.keywordBLL = new KeywordBLL();
        }

        /// <summary>
        /// 复选框
        /// </summary>
        /// <param name="viewName">组件名称（默认为：Name）</param>
        /// <param name="docName">组件Name属性（默认为：productTypeId）, 作为返回值接收</param>
        /// <param name="recipeKeyList">选中的值</param>
        /// <returns></returns>
        public IViewComponentResult Invoke(string viewName, string docName, List<RecipeKeyEntity> recipeKeyList)
        {
            ViewBag.docName = docName;
            ViewBag.recipeKeyList = recipeKeyList;
            viewName = string.IsNullOrWhiteSpace(viewName) ? "Name" : viewName;
            var List = keywordBLL.ActionDal.ActionDBAccess.Queryable<KeywordEntity>().ToList();
            return View(viewName, List);
        }

    }
}
