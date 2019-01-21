using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace BLL
{
    public class MilestonesBLL : Base.BaseBLL<MilestonesEntity>
    {
        public IPagedList<MilestonesEntity> PageList(int pageNumber, int pageSize, string searchString)
        {
            IPagedList<MilestonesEntity> milestonesEntities = ActionDal.ActionDBAccess.Queryable<MilestonesEntity>()
                                                        .WhereIF( !string.IsNullOrWhiteSpace(searchString), it => it.contents.Contains(searchString))
                                                        .OrderBy(it => it.milestonesId, SqlSugar.OrderByType.Desc)
                                                        .ToList()
                                                        .ToPagedList(pageNumber, pageSize);

            return milestonesEntities;
        }

        public MilestonesEntity GetById(int milestonesId)
        {
            return ActionDal.ActionDBAccess.Queryable<MilestonesEntity>().Where(it => it.milestonesId == milestonesId).First();
        }
    }
}
