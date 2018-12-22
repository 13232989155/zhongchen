using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 食谱关键字
    /// </summary>
    [SugarTable("t_keyword")]
    public partial class KeywordEntity
    {
        public KeywordEntity()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int keywordId { get; set; }

        /// <summary>
        /// Desc:内容
        /// Default:
        /// Nullable:False
        /// </summary>      
        [Required(ErrorMessage = "不能为空")]
        public string contents { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string remark { get; set; }
    }
}
