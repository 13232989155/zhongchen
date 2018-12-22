using Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace BLL
{
    public class UserDelayBLL : Base.BaseBLL<UserDelayEntity>
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
            var list = ActionDal.ActionDBAccess.Queryable<UserDelayEntity, UserEntity>((ud, u) => new object[] {
                        JoinType.Inner, ud.userId == u.userId })
                        .WhereIF(!string.IsNullOrWhiteSpace(searchString), (ud, u) => ud.userId.ToString().Contains(searchString) || ud.userDelayId.ToString().Contains(searchString) || ud.bill.Contains(searchString) || u.name.Contains(searchString))
                        .Select((ud, u) => new UserDelayEntity
                        {
                            bill = ud.bill,
                            createDate = ud.createDate,
                            platform = ud.platform,
                            userDelayId = ud.userDelayId,
                            userId = ud.userId,
                            name = u.name
                        })
                        .OrderBy(ud => ud.createDate, SqlSugar.OrderByType.Desc)
                        .ToList()
                        .ToPagedList(pageNumber, pageSize);
            return list;
        }


        public UserDelayEntity GetById(int userDelayId)
        {
            return ActionDal.ActionDBAccess.Queryable<UserDelayEntity>().Where(it => it.userDelayId == userDelayId).First();
        }
    }
}
