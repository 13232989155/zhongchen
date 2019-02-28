using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{

    /// <summary>
    /// 里程碑
    /// </summary>
    [SugarTable("t_milestones")]
    public partial class MilestonesEntity
    {

        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>        
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int milestonesId { get; set; }

        /// <summary>
        /// Desc:图片
        /// Default:-1
        /// Nullable:False
        /// </summary>      
        public string img { get; set; }

        /// <summary>
        /// Desc:文字
        /// Default:-1
        /// Nullable:False
        /// </summary>     
        public string contents { get; set; }

        /// <summary>
        /// Desc:创建者
        /// Default:-1
        /// Nullable:False
        /// </summary>    
        public int adminId { get; set; }
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
