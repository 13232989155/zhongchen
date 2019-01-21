using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Common;
using Entity;
using Microsoft.AspNetCore.Mvc;
using zhongchen.Base;
using zhongchen.Models;
using ZhongChen.Models;

namespace zhongchen.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserBLL userBLL;

        public UserController()
        {
            this.userBLL = new UserBLL();
        }

        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public IActionResult Self()
        {
            if (this.IsLogin())
            {
                UserEntity userEntity = this.MustLogin();

                return View(userBLL.GetById(userEntity.userId));
            }
            else
            {
                return View("Login");
            }

        }

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 注册页
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Register(string email, string password, string country)
        {
            DataResult dr = new DataResult();

            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.account = "";
                userEntity.password = DataEncrypt.DataMd5(password);
                userEntity.country = country;
                userEntity.adminId = -1;
                userEntity.birthday = null;
                userEntity.createDate = DateTime.Now;
                userEntity.disabled = false;
                userEntity.email = email;
                userEntity.FaceBook = "";
                userEntity.gender = -1;
                userEntity.Instagram = "";
                userEntity.modifyDate = DateTime.Now;
                userEntity.name = "";
                userEntity.openId = "";
                userEntity.phone = "";
                userEntity.Twitter = "";
                userEntity.unionId = "";
                userEntity.userName = "";

                int rows = userBLL.ActionDal.ActionDBAccess.Insertable(userEntity).ExecuteCommand();
                dr.code = "200";
                dr.data = rows > 0 ? true : false;

            }
            catch (Exception ex)
            {

                dr.code = "999";
                dr.error = ex.Message.ToString();
            }

            return Json(dr);
        }

        /// <summary>
        /// 判断是否存在账号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult IsAccount(string account)
        {
            DataResult dr = new DataResult();
            try
            {
                UserEntity userEntity = new UserEntity();

                userEntity = userBLL.ActionDal.ActionDBAccess.Queryable<UserEntity>().Where(it => it.account == account).First();

                dr.data = userEntity == null ? false : true;
                dr.code = "200";
            }
            catch (Exception ex)
            {
                dr.error = ex.Message.ToString();
                dr.code = "999";
            }

            return Json(dr);

        }

        /// <summary>
        /// 判断是否存邮箱
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult IsEmail(string email)
        {
            DataResult dr = new DataResult();
            try
            {
                UserEntity userEntity = new UserEntity();

                userEntity = userBLL.ActionDal.ActionDBAccess.Queryable<UserEntity>().Where(it => it.email == email).First();

                dr.data = userEntity == null ? false : true;
                dr.code = "200";
            }
            catch (Exception ex)
            {
                dr.error = ex.Message.ToString();
                dr.code = "999";
            }
            return Json(dr);

        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string email, string password)
        {

            DataResult dr = new DataResult();
            try
            {
                password = DataEncrypt.DataMd5(password);
                dr.code = "200";
                UserEntity userEntity = userBLL.ActionDal.ActionDBAccess.Queryable<UserEntity>().Where(it => it.email == email && it.password == password && it.disabled == false).First();

                if (userEntity != null)
                {
                    dr.data = true;
                    HttpContext.Session.Set<UserEntity>("user", userEntity);
                }
                else
                {
                    dr.data = false;
                }
            }
            catch (Exception ex)
            {
                dr.error = ex.Message.ToString();
                dr.code = "999";
            }

            return Json(dr);
        }

        ///// <summary>
        ///// 退出登录
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult LoginOut()
        //{

        //    DataResult dr = new DataResult();

        //    HttpContext.Session.Remove("user");

        //    dr.code = "200";
        //    dr.data = HttpContext.Session.Get<UserEntity>("user");

        //    return Json(dr);

        //}

            /// <summary>
            /// 退出登录
            /// </summary>
            /// <returns></returns>
        public IActionResult LoginOut()
        {
            DataResult dr = new DataResult();

            HttpContext.Session.Remove("user");

            return RedirectToAction("Index", controllerName:"Home");
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUser()
        {
            DataResult dr = new DataResult();

            dr.code = "200";

            dr.data = HttpContext.Session.Get<UserEntity>("user") == null ? null 
                        : userBLL.ActionDal.ActionDBAccess.Queryable<UserEntity>()
                        .Where( it => it.userId == MustLogin().userId)
                        .First();

            return Json(dr);
        }

        /// <summary>
        /// 编辑个人资料
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            ViewBag.isMod = false;
            return View(model: userBLL.ActionDal.ActionDBAccess.Queryable<UserEntity>().Where(it => it.userId == MustLogin().userId).First());
        }


        /// <summary>
        /// 编辑个人资料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit( UserEntity userEntity)
        {
            UserEntity entity = userBLL.ActionDal.ActionDBAccess.Queryable<UserEntity>()
                                .Where(it => it.userId == userEntity.userId).First();

            entity.birthday = userEntity.birthday;
            entity.FaceBook = string.IsNullOrWhiteSpace(userEntity.FaceBook) ? "" : userEntity.FaceBook;
            entity.gender = userEntity.gender;
            entity.Instagram = string.IsNullOrWhiteSpace(userEntity.Instagram) ? "" : userEntity.Instagram;
            entity.modifyDate = DateTime.Now;
            entity.name = string.IsNullOrWhiteSpace(userEntity.name) ? "" : userEntity.name;
            entity.phone = string.IsNullOrWhiteSpace(userEntity.phone) ? "" : userEntity.phone;
            entity.Twitter = string.IsNullOrWhiteSpace(userEntity.Twitter) ? "" : userEntity.Twitter;

            int rows = userBLL.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

            return RedirectToAction("Self");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public IActionResult ModPwd()
        {
            ViewBag.isMod = false;
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ModPwd( string newPassword)
        {
            UserEntity userEntity = userBLL.ActionDal.ActionDBAccess.Queryable<UserEntity>().Where(it => it.userId == MustLogin().userId).First();

            userEntity.password = DataEncrypt.DataMd5(newPassword);
            userEntity.modifyDate = DateTime.Now;

            int rows = userBLL.ActionDal.ActionDBAccess.Updateable(userEntity).ExecuteCommand();

            HttpContext.Session.Remove("user");
            ViewBag.isMod = true;
            return View();
        }

        /// <summary>
        /// 用户延保列表
        /// </summary>
        /// <returns></returns>
        public IActionResult DelayList()
        {
            if (!this.IsLogin())
            {
                return View("Login");
            }
            
            UserDelayBLL userDelayBLL = new UserDelayBLL();
            var list = userDelayBLL.ActionDal.ActionDBAccess.Queryable<UserDelayEntity>().Where(it => it.userId == MustLogin().userId).ToList();

            return View(list);
        }


        /// <summary>
        /// 用户申请
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateDelay()
        {

            return View();
        }


        /// <summary>
        /// 用户申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateDelay(UserDelayEntity userDelayEntity)
        {
            UserDelayEntity entity = new UserDelayEntity();

            entity.bill = userDelayEntity.bill;
            entity.createDate = DateTime.Now;
            entity.platform = userDelayEntity.platform;
            entity.userId = this.MustLogin().userId;

            int rows = userBLL.ActionDal.ActionDBAccess.Insertable(entity).ExecuteCommand();

            return RedirectToAction("DelayList");
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <returns></returns>
        public IActionResult Message()
        {
            if (this.IsLogin())
            {
                UserEntity userEntity = this.MustLogin();

                List<ContactEntity> contactEntities = userBLL.ActionDal.ActionDBAccess.Queryable<ContactEntity>().OrderBy( it => it.createDate, SqlSugar.OrderByType.Desc).Where(it => it.userId == userEntity.userId).ToList();

                return View(contactEntities);
            }
            else
            {
                return View("Login");
            }

        }



    }
}