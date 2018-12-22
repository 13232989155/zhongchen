using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zhongchen.ViewComponents
{
    public class VideoListViewComponent : ViewComponent
    {

        private readonly VideoBLL videoBLL;

        public VideoListViewComponent()
        {
            this.videoBLL = new VideoBLL();
        }

        /// <summary>
        /// 视频
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var List = await videoBLL.ActionDal.ActionDBAccess.Queryable<VideoEntity>().OrderBy( it => it.videoId, SqlSugar.OrderByType.Desc).ToListAsync();
            return View("List", List);
        }

    }
}
