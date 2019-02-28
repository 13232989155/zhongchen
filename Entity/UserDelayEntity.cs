using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 用户延保
    /// </summary>
    [SugarTable("t_user_delay")]
    public partial class UserDelayEntity
    {
        public UserDelayEntity()
        {

        }

        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>        
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int userDelayId { get; set; }

        /// <summary>
        /// Desc:用户ID
        /// Default:
        /// Nullable:False
        /// </summary>        
        public int userId { get; set; }

        /// <summary>
        /// Desc:单号
        /// Default:
        /// Nullable:False
        /// </summary>        
        public string bill { get; set; }


        /// <summary>
        /// Desc:购买平台
        /// Default:
        /// Nullable:False
        /// </summary>     
        public string platform { get; set; }

        /// <summary>
        /// Desc:型号
        /// Default:
        /// Nullable:False
        /// </summary>     
        public string modelNumber { get; set; }

        /// <summary>
        /// Desc:购买日期
        /// Default:
        /// Nullable:False
        /// </summary>     
        public DateTime purchasingDate { get; set; }

        // <summary>
        /// Desc:购买电话
        /// Default:
        /// Nullable:False
        /// </summary>     
        public string phoneNumber { set; get; }

        /// <summary>
        /// Desc:创建日期
        /// Default:
        /// Nullable:False
        /// </summary>     
        public DateTime createDate{ get; set; }

        /// <summary>
        /// 用户姓名（非）
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string name { get; set; }

    }
}
