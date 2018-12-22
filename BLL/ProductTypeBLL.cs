using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace BLL
{
    public class ProductTypeBLL : Base.BaseBLL<ProductTypeEntity>
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
            var list = ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>()
                      .WhereIF(!string.IsNullOrWhiteSpace(searchString), it => it.name.Contains(searchString) || it.name.Contains(searchString))
                      .OrderBy(it => it.createDate, SqlSugar.OrderByType.Desc)
                      .ToList()
                      .ToPagedList(pageNumber, pageSize);
            return list;
        }



        public ProductTypeEntity GetById(int productTypeId)
        {
            return ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().Where(it => it.productTypeId == productTypeId).First();
        }


        public ProductTypeEntity GetByName(string name)
        {
            return ActionDal.ActionDBAccess.Queryable<ProductTypeEntity>().Where(it => it.name == name).First();
        }
    }
}
