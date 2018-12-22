using Common;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace BLL
{
    public class AdminBLL : Base.BaseBLL<AdminEntity>
    {

        /// <summary>
        /// 根据账户密码获取管理员信息
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public AdminEntity GetByAccountAndPassword(string account, string password)
        {
            string md5 = DataEncrypt.DataMd5(password);

            AdminEntity entity = this.ActionDal.ActionDBAccess.Queryable<AdminEntity>()
                .Where(e => e.account == account && e.password == md5)
                .First();
            return entity;
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="searchString">搜索框参数</param>
        /// <returns></returns>
        public object PageList(int pageNumber, int pageSize, string searchString)
        {
            var list = ActionDal.ActionDBAccess.Queryable<AdminEntity>()
                        .WhereIF(!string.IsNullOrWhiteSpace(searchString), it => it.account.Contains(searchString) || it.name.Contains(searchString))
                        .OrderBy(it => it.adminId, SqlSugar.OrderByType.Desc)
                        .ToList()
                        .ToPagedList(pageNumber, pageSize);
            return list;
        }

        /// <summary>
        /// 根据名称返回数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AdminEntity GetByName(string name)
        {
            return ActionDal.ActionDBAccess.Queryable<AdminEntity>().Where(it => it.name == name).First();
        }


        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public AdminEntity GetById(int adminId)
        {
            AdminEntity entity = new AdminEntity();
            entity = ActionDal.ActionDBAccess.Queryable<AdminEntity>().Where(it => it.adminId == adminId).First();
            return entity;
        }

        /// <summary>
        /// 根据账号返回数据
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public AdminEntity GetByAccount(string account)
        {
            return ActionDal.ActionDBAccess.Queryable<AdminEntity>().Where(it => it.account == account).First();
        }
    }
}
