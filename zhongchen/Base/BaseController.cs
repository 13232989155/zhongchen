using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zhongchen.Models;

namespace zhongchen.Base
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 企业信息
        /// </summary>
        public CompanyBaseEntity companyBaseEntity = null;

        public BaseController()
        {
            CompanyBaseEntity entity = new CompanyBaseEntity();
            CompanyBaseBLL bl = new CompanyBaseBLL();
            entity = bl.Get();
            this.companyBaseEntity = entity;
        }
        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        /// <returns></returns>
        protected bool IsLogin()
        {
            bool isLogin = false;
            if (HttpContext.Session.Get<UserEntity>("user") != null)
            {
                UserEntity entity = HttpContext.Session.Get<UserEntity>("user");
                if (entity.userId >= 10000)
                {
                    isLogin = true;
                }
            }

            return isLogin;
        }

        /// <summary>
        /// 返回当前登录用户
        /// </summary>
        /// <returns></returns>
        protected UserEntity MustLogin()
        {
            if (!IsLogin())
            {
                HttpContext.Session.Remove("user");
            }

            UserEntity entity = HttpContext.Session.Get<UserEntity>("user");
            return entity;
        }

        public CompanyBaseEntity GetCompanyBase()
        {

            return this.companyBaseEntity;
        }
    }
}