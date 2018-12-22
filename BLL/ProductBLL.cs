using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace BLL
{
    public class ProductBLL : Base.BaseBLL<ProductEntity>
    {
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="searchString">搜索框参数</param>
        /// <returns></returns>
        public object PageList(int pageNumber, int pageSize, string searchString)
        {
            var list = ActionDal.ActionDBAccess.Queryable<ProductEntity>()
                      .WhereIF(!string.IsNullOrWhiteSpace(searchString), it => it.title.Contains(searchString) || it.title.Contains(searchString))
                      .OrderBy(it => it.createDate, SqlSugar.OrderByType.Desc)
                      .ToList()
                      .ToPagedList(pageNumber, pageSize);
            return list;
        }

        public ProductEntity GetById(int productId)
        {
            return ActionDal.ActionDBAccess.Queryable<ProductEntity>().Where(it => it.productId == productId).First();
        }

        /// <summary>
        /// 根据标题返回数据
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public ProductEntity GetByTitle(string title)
        {
            return ActionDal.ActionDBAccess.Queryable<ProductEntity>().Where(it => it.title == title).First();
        }
    }
}
