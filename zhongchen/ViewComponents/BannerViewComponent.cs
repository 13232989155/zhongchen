using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zhongchen.ViewComponents
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly CompanySetBLL companySetBLL;

        public BannerViewComponent()
        {
            this.companySetBLL = new CompanySetBLL();
        }

        /// <summary>
        /// banner
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var List = await GetItemsAsync();
            return View(List);
        }

        private Task<List<CompanySetEntity>> GetItemsAsync()
        {
            return companySetBLL.ActionDal.ActionDBAccess.Queryable<CompanySetEntity>().Where( it => it.keyword == "banner" && it.setType == -1).ToListAsync();
        }
    }
}
