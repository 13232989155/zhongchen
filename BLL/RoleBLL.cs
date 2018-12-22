using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace BLL
{
    public class RoleBLL : Base.BaseBLL<RoleEntity>
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
            var list = ActionDal.ActionDBAccess.Queryable<RoleEntity>()
                        .WhereIF(!string.IsNullOrWhiteSpace(searchString), it => it.roleName.Contains(searchString))
                        .OrderBy(it => it.roleId, SqlSugar.OrderByType.Desc)
                        .ToList()
                        .ToPagedList(pageNumber, pageSize);
            return list;
        }

        /// <summary>
        /// 根据角色名称获取数据
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public RoleEntity GetByRoleName(string roleName)
        {
            return ActionDal.ActionDBAccess.Queryable<RoleEntity>().Where(it => it.roleName == roleName).First();
        }
        
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public List<RoleEntity> List()
        {
            return ActionDal.ActionDBAccess.Queryable<RoleEntity>().ToList();
        }

        /// <summary>
        /// 根据角色名称返回数据
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public RoleEntity GetByName(string roleName)
        {
            return ActionDal.ActionDBAccess.Queryable<RoleEntity>().Where(it => it.roleName == roleName).First();
        }

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public RoleEntity GetById(int roleId)
        {
            return ActionDal.ActionDBAccess.Queryable<RoleEntity>().Where(it => it.roleId == roleId).First();
        }
    }
}
