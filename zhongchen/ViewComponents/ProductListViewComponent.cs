using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace zhongchen.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly ProductTypeBLL productTypeBLL;

        private readonly ProductBLL productBLL;

        public ProductListViewComponent()
        {
            this.productTypeBLL = new ProductTypeBLL();
            this.productBLL = new ProductBLL();
        }

        /// <summary>
        /// 产品
        /// </summary>
        /// <param name="pViewName">视图名称</param>
        /// <returns></returns>
        public IViewComponentResult Invoke(string pViewName, int productTypeId)
        {

            string viewName = string.Empty;

            if (string.IsNullOrWhiteSpace(pViewName))
            {
                viewName = "List";
            }
            else
            {
                viewName = pViewName;
            }
            ViewBag.productTypeId = productTypeId;
            List<ProductTypeEntity> productTypeEntities = productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().ToList();
            ViewBag.productTypeEntities = productTypeEntities;

            var List = productBLL.ActionDal.ActionDBAccess.Queryable<ProductEntity>().OrderBy(it => it.productId, SqlSugar.OrderByType.Desc).ToList();
            return View(viewName, List);
        }
    }
}
