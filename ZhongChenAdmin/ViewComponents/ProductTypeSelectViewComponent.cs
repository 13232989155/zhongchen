using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZhongChen.ViewComponents
{
    public class ProductTypeSelectViewComponent : ViewComponent
    {
        private readonly ProductTypeBLL productTypeBLL;

        public ProductTypeSelectViewComponent()
        {
            this.productTypeBLL = new ProductTypeBLL();
        }

        /// <summary>
        /// 下拉选择框
        /// </summary>
        /// <param name="productTypeId">选中的条目(默认：10001)</param>
        /// <param name="viewName">组件名称（默认为：Name）</param>
        /// <param name="docName">组件Name属性（默认为：productTypeId）作为返回值接收</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(string viewName, string docName, int? productTypeId = 10001)
        {
            ViewBag.productTypeId = productTypeId;
            ViewBag.docName = docName;
            viewName = string.IsNullOrWhiteSpace(viewName) ? "Name" : viewName;
            var List = await GetItemsAsync();
            return View(viewName, List);
        }

        private Task<List<ProductTypeEntity>> GetItemsAsync()
        {
             return productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().ToListAsync();
        
        }
    }
}