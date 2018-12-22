using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace BLL
{
    public class RecipeBLL : Base.BaseBLL<RecipeEntity>
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
            var list = ActionDal.ActionDBAccess.Queryable<RecipeEntity>()
                        .WhereIF(!string.IsNullOrWhiteSpace(searchString), it => it.title.Contains(searchString))
                        .OrderBy(it => it.createDate, SqlSugar.OrderByType.Desc)
                        .ToList()
                        .ToPagedList(pageNumber, pageSize);
            return list;
        }

        public RecipeEntity GetById(int recipeId)
        {
            return ActionDal.ActionDBAccess.Queryable<RecipeEntity>().Where(it => it.recipeId == recipeId).First();
        }
    }
}
