using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 产品
    /// </summary>
    [SugarTable("dbo.t_product")]
    public partial class ProductEntity
    {
        public ProductEntity()
        {
        }
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int productId { get; set; }

        /// <summary>
        /// Desc:分类
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public int productTypeId { get; set; }

        /// <summary>
        /// Desc:标题
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "不能为空")]
        public string title { get; set; }

        /// <summary>
        /// Desc:标题描述
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string explain { get; set; }

        /// <summary>
        /// Desc:封面图片
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string coverImage { get; set; }

        /// <summary>
        /// Desc:购买链接
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string buyLink { get; set; }

        /// <summary>
        /// Desc:产品描述
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string description { get; set; }


        /// <summary>
        /// Desc:详细
        /// Default:
        /// Nullable:False
        /// </summary>
        public string details { set; get; }

        /// <summary>
        /// Desc:项目包括
        /// Default:
        /// Nullable:False
        /// </summary>
        public string itemIncludes { set; get; }

        /// <summary>
        /// Desc:产品说明书
        /// Default:
        /// Nullable:False
        /// </summary>
        public string specification{ set; get; }

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
