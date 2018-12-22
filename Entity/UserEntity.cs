using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 用户
    /// </summary>
    [SugarTable("t_user")]
    public partial class UserEntity
    {
        public UserEntity()
        {

        }
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>        
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int userId { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "不能为空")]
        public string userName { get; set; }


        /// <summary>
        /// Desc:账号
        /// Default:
        /// Nullable:False
        /// </summary>     
        [Required(ErrorMessage = "不能为空")]
        public string account { get; set; }

        /// <summary>
        /// Desc:用户密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "不能为空")]
        //[StringLength(18, MinimumLength = 6, ErrorMessage = "长度6-18")]
        public string password { get; set; }

        /// <summary>
        /// Desc:姓名
        /// Default:
        /// Nullable:False
        /// </summary>     
        public string name { get; set; }

        /// <summary>
        /// Desc:性别：0 表示 女，1 表示男
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public int gender { get; set; }

        /// <summary>
        /// Desc:出生日期
        /// Default:DateTime.Now
        /// Nullable:False
        /// </summary>           
        public DateTime? birthday { get; set; }

        /// <summary>
        /// Desc:手机号码
        /// Default:
        /// Nullable:False
        /// </summary>     
        public string phone { get; set; }

        /// <summary>
        /// Desc:邮箱
        /// Default:
        /// Nullable:False
        /// </summary>   
        public string email { get; set; }

        /// <summary>
        /// Desc:微信openid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string openId { get; set; }

        /// <summary>
        /// Desc:微信unionid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string unionId { get; set; }

        /// <summary>
        /// Desc:FaceBook账号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FaceBook { get; set; }

        /// <summary>
        /// Desc:Instagram账号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Instagram { get; set; }

        /// <summary>
        /// Desc:Twitter账号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Twitter { get; set; }


        /// <summary>
        /// Desc:是否禁用
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool disabled { get; set; }

        /// <summary>
        /// Desc:创建者ID
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
