using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class ContactBLL : Base.BaseBLL<ContactEntity>
    {
        public object List()
        {
            return ActionDal.ActionDBAccess.Queryable<ContactEntity>().OrderBy(it => it.contactId, SqlSugar.OrderByType.Desc).ToList();
        }

        public ContactEntity GetById(int contactId)
        {
            return ActionDal.ActionDBAccess.Queryable<ContactEntity>().OrderBy(it => it.contactId, SqlSugar.OrderByType.Desc).Where(it => it.contactId == contactId).First();
        }
    }
}
