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
            //一级分类
            var level1 = productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().Where( it => it.superior == 0).ToList();

            ViewBag.level1 = level1;

            //二级分类
            int[] superior = level1.Select(it => it.productTypeId).ToArray();
            var level2 = productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().In(it => it.superior, superior).ToList();
            ViewBag.level2 = level2;

            var List = productBLL.ActionDal.ActionDBAccess.Queryable<ProductEntity>().OrderBy(it => it.productId, SqlSugar.OrderByType.Desc).ToList();
            return View(viewName, List);
        }
    }
}
