using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BLL;
using Common;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using ZhongChen.Base;

namespace ZhongChen.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// 用户逻辑层实例
        /// </summary>
        public UserBLL userBLL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {

            this.userBLL = new UserBLL();
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
            var list = userBLL.PageList(pageNumber, PageSize, searchString);
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
        /// 创建用户
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([Bind("userName, name, gender, phone, email, disabled, password, account, birthday, FaceBook, Instagram, Twitter")]UserEntity userEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userEntity.createDate = DateTime.Now;
                    userEntity.modifyDate = DateTime.Now;
                    userEntity.adminId = -1;
                    userEntity.password = DataEncrypt.DataMd5("000000");
                    userEntity.openId = "";
                    userEntity.unionId = "";
                    userEntity.FaceBook = string.IsNullOrWhiteSpace(userEntity.FaceBook) ? "" : userEntity.FaceBook;
                    userEntity.Instagram = string.IsNullOrWhiteSpace(userEntity.Instagram) ? "" : userEntity.Instagram;
                    userEntity.Twitter = string.IsNullOrWhiteSpace(userEntity.Twitter) ? "" : userEntity.Twitter;

                    userBLL.ActionDal.ActionDBAccess.Insertable(userEntity).ExecuteCommand();
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
        /// <param name="userId"></param>
        /// <returns></returns>
        public IActionResult Detail(int userId)
        {
            return View(userBLL.GetById(userId));
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IActionResult Edit(int userId)
        {
            return View(userBLL.GetById(userId));
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit([Bind("userId, userName, name, gender, phone, email, disabled, password, account, birthday, FaceBook, Instagram, Twitter")]UserEntity userEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserEntity entity = userBLL.GetById( userEntity.userId);

                    entity.disabled = userEntity.disabled;
                    entity.email = userEntity.email;
                    entity.gender = userEntity.gender;
                    entity.modifyDate = DateTime.Now;
                    entity.name = userEntity.name;
                    entity.phone = userEntity.phone;
                    entity.userName = userEntity.userName;
                    entity.account = userEntity.account;
                    entity.birthday = userEntity.birthday;
                    entity.FaceBook = string.IsNullOrWhiteSpace(userEntity.FaceBook) ? "" : userEntity.FaceBook;
                    entity.Instagram = string.IsNullOrWhiteSpace(userEntity.Instagram) ? "" : userEntity.Instagram;
                    entity.Twitter = string.IsNullOrWhiteSpace(userEntity.Twitter) ? "" : userEntity.Twitter;

                    userBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.ex = ex.Message.ToString();
                    return View("Error");
                }
            }
            return View(userEntity);
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int userId)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity = userBLL.GetById(userId);
                userBLL.ActionDal.ActionDBAccess.Deleteable(userEntity).ExecuteCommand();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }

        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <returns></returns>
        public IActionResult Export()
        {
            List<UserEntity> userEntities = userBLL.ExportList();
            string[] tableTitle = new string[] 
            {
                "ID",
                "国家",
                "名称",
                "性别",
                "生日",
                "手机号码",
                "邮箱",
                "FaceBook",
                "Instagram",
                "Twitter",
                "创建时间"
            };

            userEntities = userEntities.Select(it => new UserEntity
                            {
                                userId = it.userId,
                                country = it.country,
                                name = it.name,
                                gender = it.gender,
                                birthday = it.birthday,
                                phone = it.phone,
                                email = it.email,
                                FaceBook = it.FaceBook,
                                Instagram = it.Instagram,
                                Twitter = it.Twitter,
                                createDate = it.createDate
                            }).ToList();

            var file = Excel.NpoiHelper.UserOutput(ConvertHelper.ListToDataTable(userEntities), tableTitle);

            return File(file, "application/octet-stream", fileDownloadName:"用户数据.xlsx");
        }

        

    }
}