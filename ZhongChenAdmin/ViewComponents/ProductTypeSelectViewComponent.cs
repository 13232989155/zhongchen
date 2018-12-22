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
        /// <param name="isZero">是否显示一级类目（默认为：false）</param>
        /// <param name="isLevel">显示等级，是：显示一级；否：显示二级（默认为：false）</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(string viewName, bool isLevel, string docName, int? productTypeId = 10001, bool? isZero = false)
        {
            ViewBag.productTypeId = productTypeId;
            ViewBag.isZero = isZero;
            ViewBag.docName = docName;
            viewName = string.IsNullOrWhiteSpace(viewName) ? "Name" : viewName;

            var List = await GetItemsAsync( isLevel);
            return View(viewName, List);
        }

        private Task<List<ProductTypeEntity>> GetItemsAsync( bool isLevel)
        {
            if (isLevel)
            {
                return productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().Where(it => it.superior == 0).ToListAsync();
            }
            else
            {
                //一级分类
                var level1 = productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().Where(it => it.superior == 0).ToList();

                //二级分类
                int[] superior = level1.Select(it => it.productTypeId).ToArray();

                if (superior.Count() <= 0)
                {
                    return productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().Where(it => it.productTypeId < 0).ToListAsync();
                }

                return productTypeBLL.ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().In(it => it.superior, superior).ToListAsync();
            }

        }
    }
}
