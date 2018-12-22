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
    public class SettingController : BaseController
    {

        public SettingController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment) : base(accessor, hostingEnvironment)
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 企业信息设置
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyProfile()
        {
            CompanyBaseEntity entity = new CompanyBaseEntity();
            CompanyBaseBLL bl = new CompanyBaseBLL();
            entity = bl.Get();

            return View(entity);
        }

        /// <summary>
        /// 保存企业信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CompanyProfile(IFormCollection collection, IFormFile logo)
        {
            try
            {
                CompanyBaseBLL bl = new CompanyBaseBLL();
                AdminEntity admin = this.MustLogin();
                CompanyBaseEntity entity = new CompanyBaseEntity();
                string companyBaseId = Convert.ToString(collection["companyBaseId"]);

                if (string.IsNullOrWhiteSpace(companyBaseId))
                {
                    entity.bakTel = collection["bakTel"];
                    entity.companyAddress = collection["companyAddress"];
                    entity.contactUser = collection["contactUser"];
                    entity.fax = collection["fax"];
                    entity.mail = collection["mail"];
                    entity.name = collection["name"];
                    entity.phone = collection["phone"];
                    entity.post = collection["post"];
                    entity.tel = collection["tel"];
                    entity.logo = logo == null ? "" : this.UpFile(logo);

                    bl.ActionDal.ActionDBAccess.Insertable(entity).ExecuteCommand();

                }
                else
                {
                    entity = bl.GetById(Convert.ToInt32(companyBaseId));

                    entity.bakTel = collection["bakTel"];
                    entity.companyAddress = collection["companyAddress"];
                    entity.contactUser = collection["contactUser"];
                    entity.fax = collection["fax"];
                    entity.mail = collection["mail"];
                    entity.name = collection["name"];
                    entity.phone = collection["phone"];
                    entity.post = collection["post"];
                    entity.tel = collection["tel"];
                    if (string.IsNullOrWhiteSpace( collection["isLogo"]) )
                    {
                        entity.logo = logo == null ? "" : this.UpFile(logo);
                    }
                    bl.ActionDal.ActionDBAccess.Updateable(entity).ExecuteCommand();

                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }
        }

        /// <summary>
        /// banner图片设置
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyBanner()
        {
            CompanySetBLL companySetBLL = new CompanySetBLL();
            var list = companySetBLL.List();

            return View(list);
        }

        /// <summary>
        /// 新增banner图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CompanyBanner(IFormCollection collection, IFormFile keyValue)
        {
            try
            {
                CompanySetBLL companySetBLL = new CompanySetBLL();
                CompanySetEntity entity = new CompanySetEntity();

                entity.keyName = "";
                entity.keyValue = keyValue == null ? "" : this.UpFile(keyValue);
                entity.keyword = "banner";
                entity.setType = -1;

                companySetBLL.ActionDal.ActionDBAccess.Insertable(entity).ExecuteCommand();


                return RedirectToAction("CompanyBanner");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message.ToString();
                return View("Error");
            }
        }

        /// <summary>
        /// 删除banner图片
        /// </summary>
        /// <param name="congfigId"></param>
        /// <returns></returns>
        public ActionResult DeleteBanner(int bannerId)
        {
            CompanySetEntity entity = new CompanySetEntity();
            CompanySetBLL companySetBLL = new CompanySetBLL();
            entity = companySetBLL.GetById(bannerId);

            companySetBLL.ActionDal.ActionDBAccess.Deleteable(entity).ExecuteCommand();

            return RedirectToAction("CompanyBanner");
        }

        /// <summary>
        /// 用户反馈列表
        /// </summary>
        /// <returns></returns>
        public IActionResult ContactList()
        {
            ContactBLL contactBLL = new ContactBLL();
            var list = contactBLL.List();

            return View(list);
        }

        /// <summary>
        /// 标记处理
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public IActionResult DealContact( int contactId)
        {
            ContactBLL contactBLL = new ContactBLL();
            ContactEntity contactEntity = contactBLL.ActionDal.ActionDBAccess.Queryable<ContactEntity>().Where(it => it.contactId == contactId).First();
            if ( !contactEntity.isDeal)
            {
                contactEntity.isDeal = true;
                contactEntity.adminId = this.MustLogin().adminId;
                contactEntity.modifyDate = DateTime.Now;

                contactBLL.ActionDal.ActionDBAccess.Updateable(contactEntity).ExecuteCommand();
            }


            return RedirectToAction("ContactList");
        }

        /// <summary>
        /// 详细页面
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public IActionResult ContactDetail( int contactId)
        {
            ContactBLL contactBLL = new ContactBLL();
            ContactEntity contactEntity = contactBLL.ActionDal.ActionDBAccess.Queryable<ContactEntity>().Where(it => it.contactId == contactId).First();
            return View(contactEntity);
        }
    }
}