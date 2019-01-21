using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 用户反馈
    /// </summary>
    [SugarTable("t_contact")]
    public partial class ContactEntity
    {

        public ContactEntity()
        {


        }


        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int contactId { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public string name { get; set; }

        /// <summary>
        /// Desc:userId
        /// Default:-1
        /// Nullable:False
        /// </summary>  
        public int userId { set; get; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string tel { get; set; }

        /// <summary>
        /// Desc:邮箱
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string email { get; set; }

        /// <summary>
        /// Desc:id地址
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ip { get; set; }

        /// <summary>
        /// Desc:主题
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string subject { get; set; }

        /// <summary>
        /// Desc:消息
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string message { get; set; }

        /// <summary>
        /// 回复
        /// </summary>
        public string reply { set; get; }

        /// <summary>
        /// Desc:是否处理
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool isDeal { get; set; }

        /// <summary>
        /// Desc:处理者ID
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public int adminId { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:DateTime.Now
        /// Nullable:False
        /// </summary>           
        public DateTime createDate { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:DateTime.Now
        /// Nullable:False
        /// </summary>           
        public DateTime modifyDate { get; set; }
    }
}
