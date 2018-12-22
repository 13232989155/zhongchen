using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using zhongchen.Base;
using zhongchen.Models;

namespace zhongchen.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            

            return View();
        }

        public IActionResult Contact()
        {
            

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Contact(ContactEntity contactEntity )
        {
            contactEntity.adminId = -1;
            contactEntity.createDate = DateTime.Now;
            contactEntity.tel = string.IsNullOrWhiteSpace(contactEntity.tel) ? "" : contactEntity.tel;
            contactEntity.email = string.IsNullOrWhiteSpace(contactEntity.email) ? "" : contactEntity.email;
            contactEntity.subject = string.IsNullOrWhiteSpace(contactEntity.subject) ? "" : contactEntity.subject;
            contactEntity.modifyDate = DateTime.Now;
            contactEntity.isDeal = false;
            contactEntity.ip = "";

            ContactBLL contactBLL = new ContactBLL();

            int rows = contactBLL.ActionDal.ActionDBAccess.Insertable(contactEntity).ExecuteCommand();

            if (rows > 0)
            {
                return View(model: true);
            }
            else
            {
                return View(model: false);
            }

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Support()
        {
            CompanyBaseBLL companyBaseBLL = new CompanyBaseBLL();
            var entity = companyBaseBLL.Get();

            List<FaqEntity> faqList = companyBaseBLL.ActionDal.ActionDBAccess.Queryable<FaqEntity>().ToList();
            ViewBag.faqList = faqList;


            return View( entity);
        }

    }
}
