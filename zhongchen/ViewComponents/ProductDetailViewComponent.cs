using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zhongchen.ViewComponents
{
    public class ProductDetailViewComponent : ViewComponent
    {
        private readonly ProductImageBLL productImageBLL;

        private readonly ProductBLL productBLL;
        public ProductDetailViewComponent()
        {
            this.productImageBLL = new ProductImageBLL();
            this.productBLL = new ProductBLL();
        }

        /// <summary>
        /// 产品详细
        /// </summary>
        /// <param name="productId">产品ID</param>
        /// <returns></returns>
        public IViewComponentResult Invoke( int productId)
        {
            

            var productImageList = productImageBLL.ActionDal.ActionDBAccess.Queryable<ProductImageEntity>().Where( it => it.productId == productId).ToList();

            ViewBag.productImageList = productImageList;

            var entity = productBLL.ActionDal.ActionDBAccess.Queryable<ProductEntity>().Where(it => it.productId == productId).First();

            return View("Detail", entity);
        }
    }
}
