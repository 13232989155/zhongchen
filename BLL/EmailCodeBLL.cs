using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class EmailCodeBLL : Base.BaseBLL<EmailCodeEntity>
    {
        public EmailCodeEntity GetEmailAndCode(string email, string code)
        {
            return ActionDal.ActionDBAccess.Queryable<EmailCodeEntity>().Where(it => it.code == code && it.email == email).First();
        }

        public EmailCodeEntity GetEmail(string email)
        {
            return ActionDal.ActionDBAccess.Queryable<EmailCodeEntity>().Where(it => it.email == email).First();
        }
    }
}
