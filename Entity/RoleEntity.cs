using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    [SugarTable("t_role")]
    public partial class RoleEntity
    {
        public RoleEntity()
        {

        }

        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>       
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int roleId { get; set; }

        /// <summary>
        /// Desc:角色名称
        /// Default:
        /// Nullable:False
        /// </summary>      
        [Required(ErrorMessage = "不能为空")]
        public string roleName { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public int state { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string remark { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:getdate()
        /// Nullable:True
        /// </summary>           
        public DateTime createDate { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:getdate()
        /// Nullable:True
        /// </summary>   
        public DateTime modifyDate
        {
            get;
            set;
        }
    }
}
