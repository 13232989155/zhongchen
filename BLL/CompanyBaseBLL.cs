using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CompanyBaseBLL : Base.BaseBLL<CompanyBaseEntity>
    {
        /// <summary>
        /// 获取企业信息
        /// </summary>
        /// <returns></returns>
        public CompanyBaseEntity Get()
        {
            CompanyBaseEntity entity = new CompanyBaseEntity();
            entity = ActionDal.ActionDBAccess.Queryable<CompanyBaseEntity>().Take(1).First();
            return entity;
        }

        /// <summary>
        /// 根据ID返回
        /// </summary>
        /// <param name="companyBaseId"></param>
        /// <returns></returns>
        public CompanyBaseEntity GetById(int companyBaseId)
        {
            CompanyBaseEntity entity = new CompanyBaseEntity();
            entity = ActionDal.ActionDBAccess.Queryable<CompanyBaseEntity>().Where(it => it.companyBaseId == companyBaseId).First();
            return entity;
        }
    }
}
