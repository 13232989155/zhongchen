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
using ZhongChen.Models;

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
            HtmlImageTextElementBLL htmlImageTextElementBLL = new HtmlImageTextElementBLL();
            string[] vs = new string[] { "关于页图文1", "关于页图文2", "关于页图文3", "关于页图文4" };
            List<HtmlImageTextElementEntity> htmlImageTextElementEntities = htmlImageTextElementBLL.ListByKeyInts(vs);
            ViewBag.htmlImageTextElementEntities = htmlImageTextElementEntities;

            HtmlFontElementBLL htmlFontElementBLL = new HtmlFontElementBLL();
            HtmlFontElementEntity htmlFontElement = htmlFontElementBLL.GetByKey("关于页里程碑标题");
            ViewBag.milestonesTitle = htmlFontElement.value;

            MilestonesBLL milestonesBLL = new MilestonesBLL();
            List<MilestonesEntity> milestonesEntities = milestonesBLL.ActionDal.ActionDBAccess.Queryable<MilestonesEntity>().OrderBy( it => it.createDate).ToList();
                    
            return View(milestonesEntities);
        }

        public IActionResult Contact()
        {

            HtmlFontElementBLL htmlFontElementBLL = new HtmlFontElementBLL();

            string[] vs = new string[] { "联系页邮箱1", "联系页邮箱2", "联系页电话1", "联系页电话2", "联系页电话3" };
            List<HtmlFontElementEntity> htmlFontElementEntities = htmlFontElementBLL.ListByKeyInts(vs);

            return View(htmlFontElementEntities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Contact(ContactEntity contactEntity )
        {  
            DataResult dr = new DataResult();
            try
            {
                int userId = -1;
                if (this.IsLogin())
                {
                    userId = this.MustLogin().userId;
                }

                contactEntity.adminId = -1;
                contactEntity.createDate = DateTime.Now;
                contactEntity.tel = "";
                contactEntity.email = string.IsNullOrWhiteSpace(contactEntity.email) ? "" : contactEntity.email;
                contactEntity.subject = string.IsNullOrWhiteSpace(contactEntity.subject) ? "" : contactEntity.subject;
                contactEntity.modifyDate = DateTime.Now;
                contactEntity.isDeal = false;
                contactEntity.ip = "";
                contactEntity.name = string.IsNullOrWhiteSpace(contactEntity.name) ? "" : contactEntity.name;
                contactEntity.message = string.IsNullOrWhiteSpace(contactEntity.message) ? "" : contactEntity.message;
                contactEntity.userId = userId;
                contactEntity.reply = "";

                ContactBLL contactBLL = new ContactBLL();

                int rows = contactBLL.ActionDal.ActionDBAccess.Insertable(contactEntity).ExecuteCommand();
                if ( rows > 0)
                {
                    dr.code = "200";
                }
                else
                {
                    dr.code = "201";
                }
            }
            catch (Exception ex)
            {

                dr.code = "999";
                dr.error = ex.Message.ToString();
            }

            return Json(dr);

        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetContactById( int contactId)
        {
            DataResult dr = new DataResult();
            try
            {
                ContactBLL contactBLL = new ContactBLL();
                ContactEntity contactEntity = new ContactEntity();

                contactEntity = contactBLL.GetById(contactId);

                dr.code = "200";
                dr.data = contactEntity;
            }
            catch (Exception ex)
            {

                dr.code = "999";
                dr.error = ex.Message.ToString();
            }

            return Json(dr);
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

        /// <summary>
        /// 帮助页
        /// </summary>
        /// <returns></returns>
        public IActionResult Support()
        {
            CompanyBaseBLL companyBaseBLL = new CompanyBaseBLL();
            var entity = companyBaseBLL.Get();

            List<FaqEntity> faqList = companyBaseBLL.ActionDal.ActionDBAccess.Queryable<FaqEntity>().ToList();
            ViewBag.faqList = faqList;

            HtmlFontElementBLL htmlFontElementBLL = new HtmlFontElementBLL();
            HtmlFontElementEntity htmlFontElement = htmlFontElementBLL.GetByKey("帮助页问答标题");
            HtmlFontElementEntity htmlFontElementEntity = htmlFontElementBLL.GetByKey("帮助页服务标题");
            ViewBag.FaqTitle = htmlFontElement.value;
            ViewBag.ServiceTitle = htmlFontElementEntity.value;


            HtmlImageTextElementBLL htmlImageTextElementBLL = new HtmlImageTextElementBLL();
            string[] vs = new string[]{ "帮助页图片", "帮组页服务左", "帮组页服务中", "帮组页服务右" };
            List<HtmlImageTextElementEntity> htmlImageTextElementEntities = htmlImageTextElementBLL.ListByKeyInts(vs);
            ViewBag.htmlImageTextElementEntities = htmlImageTextElementEntities;

            return View( entity);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Search(int typeId, string searchString)
        {
            HtmlFontElementBLL htmlFontElementBLL = new HtmlFontElementBLL();

            string[] vs = new string[] { "食谱页食谱标题", "食谱页视频标题", "搜索页产品标题" };
            List<HtmlFontElementEntity> htmlFontElementEntities = htmlFontElementBLL.ListByKeyInts(vs);
            ViewBag.htmlFontElementEntities = htmlFontElementEntities;

            SearchResult searchResult = new SearchResult();
            searchResult.typeId = typeId;
            searchResult.searchString = searchString;
            searchResult.productEntities = new List<ProductEntity>();
            searchResult.recipeEntities = new List<RecipeEntity>();
            searchResult.videoEntities = new List<VideoEntity>();
            switch (typeId)
            {
                case 2:
                    searchResult.recipeEntities = RecipeList(searchString);
                    break;
                case 3:
                    searchResult.productEntities = ProductList(searchString);
                    break;
                case 4:
                    searchResult.videoEntities = VideoList(searchString);
                    break;
                default:
                    searchResult.recipeEntities = RecipeList(searchString);
                    searchResult.productEntities = ProductList(searchString);
                    searchResult.videoEntities = VideoList(searchString);
                    break;
            }
            return View(searchResult);
        }

        /// <summary>
        /// 获取食谱列表
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        protected List<RecipeEntity> RecipeList(string searchString)
        {
            List<RecipeEntity> recipeEntities = new List<RecipeEntity>();

            if ( !string.IsNullOrWhiteSpace(searchString))
            {
                RecipeBLL recipeBLL = new RecipeBLL();
                recipeEntities = recipeBLL.ActionDal.ActionDBAccess.Queryable<RecipeEntity>()
                                .Where(it => it.title.Contains(searchString) || it.subheading.Contains(searchString) || it.explain.Contains(searchString))
                                .ToList();
            }                   
            return recipeEntities;
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        protected List<ProductEntity> ProductList(string searchString)
        {
            List<ProductEntity> productEntities = new List<ProductEntity>();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                ProductBLL productBLL = new ProductBLL();
                productEntities = productBLL.ActionDal.ActionDBAccess.Queryable<ProductEntity>()
                                .Where(it => it.explain.Contains(searchString) || it.prop.Contains(searchString) || it.title.Contains(searchString))
                                .ToList();
            }
            return productEntities;
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        protected List<VideoEntity> VideoList(string searchString)
        {
            List<VideoEntity> videoEntities = new List<VideoEntity>();
            if ( !string.IsNullOrWhiteSpace(searchString))
            {
                VideoBLL videoBLL = new VideoBLL();
                videoEntities = videoBLL.ActionDal.ActionDBAccess.Queryable<VideoEntity>()
                                .Where(it => it.title.Contains(searchString))
                                .ToList();

            }

            return videoEntities;
        }

    }
}
