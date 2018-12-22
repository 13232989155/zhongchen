using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Common;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhongChen.Base;

namespace ZhongChen.Controllers
{
    public class AdminController : BaseController
    {
        /// <summary>
        /// 管理员逻辑层实例
        /// </summary>
        public AdminBLL adminBLL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AdminController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {

            this.adminBLL = new AdminBLL();
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
            var list = adminBLL.PageList(pageNumber, PageSize, searchString);
            return View(list);
        }

        /// <summary>
        /// 创建页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 创建管理员账号
        /// </summary>
        /// <param name="adminEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([Bind("name,phone,email,account,password,roleId")]AdminEntity adminEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AdminEntity entity = adminBLL.GetByName(adminEntity.name);
                    if (entity != null)
                    {
                        ViewBag.ex = "已存在该名称";
                        return View("Error");
                    }
                    entity = null;
                    entity = adminBLL.GetByAccount(adminEntity.account);
                    if (entity != null)
                    {
                        ViewBag.ex = "已存在该账号";
                        return View("Error");
                    }
                    adminEntity.email = string.IsNullOrWhiteSpace(adminEntity.email) ? "" : adminEntity.email;
                    adminEntity.phone = string.IsNullOrWhiteSpace(adminEntity.phone) ? "" : adminEntity.phone;
                    adminEntity.createDate = DateTime.Now;
                    adminEntity.modifyDate = DateTime.Now;
                    adminEntity.password = DataEncrypt.DataMd5(adminEntity.password);

                    adminBLL.ActionDal.ActionDBAccess.Insertable(adminEntity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(adminEntity);

        }

        /// <summary>
        /// 详细页面
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public IActionResult Detail(int adminId)
        {
            return View(adminBLL.GetById(adminId));
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public IActionResult Edit(int adminId)
        {
            return View(adminBLL.GetById(adminId));
        }

        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="adminEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit([Bind("adminId,name, phone,email,account,roleId,password")]AdminEntity adminEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AdminEntity entity = adminBLL.GetByName(adminEntity.name);
                    if (entity != null)
                    {
                        if (entity.adminId != adminEntity.adminId)
                        {
                            ViewBag.ex = "已存在该名称";
                            return View("Error");
                        }

                    }

                    entity = null;
                    entity = adminBLL.GetByAccount(adminEntity.account);
                    if (entity != null)
                    {
                        if (entity.adminId != adminEntity.adminId)
                        {
                            ViewBag.ex = "已存在该账号";
                            return View("Error");
                        }
                    }

                    entity = null;
                    entity = adminBLL.GetById(adminEntity.adminId);
                    entity.name = adminEntity.name;
                    entity.account = adminEntity.account;
                    entity.email = string.IsNullOrWhiteSpace(adminEntity.email) ? "" : adminEntity.email;
                    entity.phone = string.IsNullOrWhiteSpace(adminEntity.phone) ? "" : adminEntity.phone;
                    entity.modifyDate = DateTime.Now;
                    entity.roleId = adminEntity.roleId;

                    adminBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(adminEntity);
        }


        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int adminId)
        {
            try
            {
                AdminEntity adminEntity = new AdminEntity();
                adminEntity = adminBLL.GetById(adminId);
                adminBLL.ActionDal.ActionDBAccess.Deleteable(adminEntity).ExecuteCommand();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }

        }

        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public IActionResult Self()
        {
            return View(adminBLL.GetById(this.MustLogin().adminId));
        }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Self([Bind("adminId,name, phone,email, account")]AdminEntity adminEntity)
        {
            try
            {
                AdminEntity entity = adminBLL.GetByName(adminEntity.name);
                if (entity != null)
                {
                    if (entity.adminId != adminEntity.adminId)
                    {
                        ViewBag.ex = "已存在该名称";
                        return View("Error");
                    }

                }

                entity = null;
                entity = adminBLL.GetByAccount(adminEntity.account);
                if (entity != null)
                {
                    if (entity.adminId != adminEntity.adminId)
                    {
                        ViewBag.ex = "已存在该账号";
                        return View("Error");
                    }
                }

                entity = null;
                entity = adminBLL.GetById(adminEntity.adminId);
                entity.name = adminEntity.name;
                entity.email = string.IsNullOrWhiteSpace(adminEntity.email) ? "" : adminEntity.email;
                entity.phone = string.IsNullOrWhiteSpace(adminEntity.phone) ? "" : adminEntity.phone;
                entity.modifyDate = DateTime.Now;

                adminBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                return View(adminBLL.GetById(adminEntity.adminId));
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public IActionResult ModPassword()
        {
            return View(adminBLL.GetById(this.MustLogin().adminId));
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ModPassword(int adminId, string pwd1, string pwd2)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pwd1) != string.IsNullOrWhiteSpace(pwd2))
                {
                    ViewBag.ex = "密码为空";
                    return View("Error");
                }

                AdminEntity entity = new AdminEntity();
                entity = adminBLL.GetById(adminId);
                entity.password = DataEncrypt.DataMd5(pwd1);
                entity.modifyDate = DateTime.Now;

                adminBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                //重新登录
                HttpContext.Session.Remove("admin");

                return Redirect("/Login/Login");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }
        }
    }
}