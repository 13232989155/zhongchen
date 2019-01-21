using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 产品分类
    /// </summary>
    [SugarTable("t_product_type")]
    public partial class ProductTypeEntity
    {
        public ProductTypeEntity()
        {


        }
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>         
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int productTypeId { get; set; }

        /// <summary>
        /// Desc:类别名称
        /// Default:
        /// Nullable:False
        /// </summary>        
        [Required(ErrorMessage = "不能为空")]
        public string name { get; set; }

        /// <summary>
        /// Desc:主标题
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string title { get; set; }

        /// <summary>
        /// Desc:副标题
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string subheading { get; set; }

        /// <summary>
        /// Desc:推荐标题
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string recommendTitle { get; set; }

        /// <summary>
        /// Desc:图标
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string coverImage { get; set; }

        /// <summary>
        /// Desc:上级
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public bool recommend { get; set; }

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
