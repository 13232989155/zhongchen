using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CompanySetBLL : Base.BaseBLL<CompanySetEntity>
    {
        /// <summary>
        /// 获取banner列表
        /// </summary>
        /// <returns></returns>
        public List<CompanySetEntity> List()
        {
            return ActionDal.ActionDBAccess.Queryable<CompanySetEntity>().Where(it => it.keyword == "banner").ToList();
        }

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        public CompanySetEntity GetById(int bannerId)
        {
            return ActionDal.ActionDBAccess.Queryable<CompanySetEntity>().Where(it => it.companySetId == bannerId).First();
        }
    }
}
