using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZhongChen.ViewComponents
{
    public class RoleSelectViewComponent : ViewComponent
    {
        private readonly RoleBLL roleBLL;

        public RoleSelectViewComponent()
        {
            this.roleBLL = new RoleBLL();
        }

        /// <summary>
        /// 下拉选择框
        /// </summary>
        /// <param name="roleId">选中的条目</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(int? roleId = 10001)
        {
            ViewBag.roleId = roleId;
            var List = await GetItemsAsync();
            return View("RoleName", List);
        }

        private Task<List<RoleEntity>> GetItemsAsync()
        {
            return roleBLL.ActionDal.ActionDBAccess.Queryable<RoleEntity>().ToListAsync();
        }
    }
}
