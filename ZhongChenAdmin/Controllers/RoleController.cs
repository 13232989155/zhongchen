using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhongChen.Base;

namespace ZhongChen.Controllers
{
    public class RoleController : BaseController
    {
        /// <summary>
        /// 角色逻辑层实例
        /// </summary>
        public RoleBLL roleBLL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {
            this.roleBLL = new RoleBLL();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="searchString">搜索参数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public IActionResult List(string searchString, int? page)
        {

            ViewBag.searchString = searchString;
            int pageNumber = (page ?? 1);
            int PageSize = 10;
            var list = roleBLL.PageList(pageNumber, PageSize, searchString);
            return View(list);
        }

        /// <summary>
        /// 创建页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
           // roleBLL.ActionDal.ActionDBAccess.DbFirst.CreateClassFile("c:\\Demo\\1");
            return View();
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([Bind("roleName,remark")]RoleEntity roleEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RoleEntity entity = roleBLL.GetByRoleName(roleEntity.roleName);
                    if (entity != null)
                    {
                        ViewBag.ex = "已存在该角色";
                        return View("Error");
                    }
                    roleEntity.remark = roleEntity.remark ?? "";
                    roleEntity.createDate = DateTime.Now;
                    roleEntity.modifyDate = DateTime.Now;
                    roleEntity.state = 1;

                    roleBLL.ActionDal.ActionDBAccess.Insertable(roleEntity).ExecuteCommand();
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View();
           
        }


        /// <summary>
        /// 详细页面
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IActionResult Detail(int roleId)
        {
            return View(roleBLL.GetById(roleId));
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IActionResult Edit(int roleId)
        {
            return View(roleBLL.GetById(roleId));
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit([Bind("roleId,roleName,remark")]RoleEntity roleEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RoleEntity entity = roleBLL.GetByName(roleEntity.roleName);
                    if (entity != null)
                    {
                        if (entity.roleId != roleEntity.roleId)
                        {
                            ViewBag.ex = "已存在该名称";
                            return View("Error");
                        }

                    }

                    entity = null;
                    entity = roleBLL.GetById(roleEntity.roleId);
                    entity.roleName = roleEntity.roleName;
                    entity.remark = roleEntity.remark;
                    entity.modifyDate = DateTime.Now;

                    roleBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(roleEntity);
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int roleId)
        {
            try
            {
                RoleEntity roleEntity = new RoleEntity();
                roleEntity = roleBLL.GetById(roleId);
                roleBLL.ActionDal.ActionDBAccess.Deleteable(roleEntity).ExecuteCommand();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }

        }
    }
}