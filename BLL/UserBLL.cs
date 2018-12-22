using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace BLL
{
    public class UserBLL : Base.BaseBLL<UserEntity>
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
            var list = ActionDal.ActionDBAccess.Queryable<UserEntity>()
                        .WhereIF(!string.IsNullOrWhiteSpace(searchString), it => it.userName.Contains(searchString) || it.name.Contains(searchString) || it.phone.Contains(searchString) || it.userId.ToString().Contains(searchString))
                        .OrderBy(it => it.userId, SqlSugar.OrderByType.Desc)
                        .ToList()
                        .ToPagedList(pageNumber, pageSize);
            return list;
        }


        public UserEntity GetById(int userId)
        {
            return ActionDal.ActionDBAccess.Queryable<UserEntity>().Where(it => it.userId == userId).First();
        }
    }
}
