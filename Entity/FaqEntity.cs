using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 常见问题
    /// </summary>
    [SugarTable("t_faq")]
    public partial class FaqEntity
    {
        public FaqEntity()
        {


        }
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>        
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int faqId { get; set; }

        /// <summary>
        /// Desc:问题标题
        /// Default:-1
        /// Nullable:False
        /// </summary>      
        [Required(ErrorMessage = "不能为空")]
        public string title { get; set; }

        /// <summary>
        /// Desc:回答内容
        /// Default:-1
        /// Nullable:False
        /// </summary>     
        [Required(ErrorMessage = "不能为空")]
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
