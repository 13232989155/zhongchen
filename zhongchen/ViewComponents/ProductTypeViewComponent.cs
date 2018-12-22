using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zhongchen.ViewComponents
{
    public class ProductTypeViewComponent : ViewComponent
    {

        private readonly ProductTypeBLL productTypeBLL;

        public ProductTypeViewComponent()
        {
            this.productTypeBLL = new ProductTypeBLL();
        }

        /// <summary>
        /// 产品分类
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

        private Task<List<ProductTypeEntity>> GetItemsAsync(string viewName)
        {

            switch (viewName)
            {
                case "Main":
                    return productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().Where(it => it.superior == 0).Take(10).ToListAsync();
                case "Menu":
                    return productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().Where( it => it.superior == 0).ToListAsync();
                default:
                    return productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().ToListAsync();
            }

        }

    }
}
