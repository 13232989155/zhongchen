using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace BLL
{
    public class KeywordBLL : Base.BaseBLL<KeywordEntity>
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
            var list = ActionDal.ActionDBAccess.Queryable<KeywordEntity>()
                        .WhereIF(!string.IsNullOrWhiteSpace(searchString), it => it.contents.Contains(searchString))
                        .OrderBy(it => it.keywordId, SqlSugar.OrderByType.Desc)
                        .ToList()
                        .ToPagedList(pageNumber, pageSize);
            return list;
        }

        public KeywordEntity GetById(int keywordId)
        {
            return ActionDal.ActionDBAccess.Queryable<KeywordEntity>().Where(it => it.keywordId == keywordId).First();
        }

        /// <summary>
        /// 获取全部关键字
        /// </summary>
        /// <returns></returns>
        public List<KeywordEntity> List()
        {
            return ActionDal.ActionDBAccess.Queryable<KeywordEntity>().ToList();
        }
    }
}
