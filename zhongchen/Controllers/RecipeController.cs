using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zhongchen.Base;

namespace zhongchen.Controllers
{
    public class RecipeController : BaseController
    {
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IActionResult List( string searchString)
        {
            return View(model : searchString);
        }

        public IActionResult Detail( int recipeId)
        {
            return View( model:recipeId);
        }


    }
}