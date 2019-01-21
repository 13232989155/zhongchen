using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    ///<summary>
    ///页面图文元素设置
    ///</summary>
    [SugarTable("t_html_image_text_element")]
    public partial class HtmlImageTextElementEntity
    {
        public HtmlImageTextElementEntity()
        {


        }

        /// <summary>
        /// Desc:主键ID
        /// Default:
        /// Nullable:False
        /// </summary>     
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string keys { get; set; }

        /// <summary>
        /// Desc:图片
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string img { get; set; }

        /// <summary>
        /// Desc:文字
        /// Default:
        /// Nullable:False
        /// </summary>      
        public string contents { set; get; }

        /// <summary>
        /// Desc:管理员ID
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
