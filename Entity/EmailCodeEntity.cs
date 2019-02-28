using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 邮箱验证码
    /// </summary>
    [SugarTable("t_eamil_code")]
    public partial class EmailCodeEntity
    {

        public EmailCodeEntity()
        {
        }

        /// <summary>
        /// 管理员ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int emailCodeId
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string code
        {
            get;
            set;
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email
        {
            get;
            set;
        }

        /// <summary>
        /// 是否通过验证
        /// </summary>
        public bool verifying { set; get; }

        /// <summary>
        /// 新增日期
        /// </summary>
        public DateTime createDate
        {
            get;
            set;
        }

    }
}
