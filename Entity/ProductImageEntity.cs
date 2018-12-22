using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 产品图片
    /// </summary>
    [SugarTable("t_product_image")]
    public partial class ProductImageEntity
    {
        public ProductImageEntity()
        {


        }
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int productImageId { get; set; }

        /// <summary>
        /// Desc:产品ID
        /// Default:-1
        /// Nullable:False
        /// </summary>         
        [Required(ErrorMessage = "不能为空")]
        public int productId { get; set; }

        /// <summary>
        /// Desc:照片路径
        /// Default:
        /// Nullable:False
        /// </summary>      
        [Required(ErrorMessage = "不能为空")]
        //[StringLength(18, MinimumLength = 1, ErrorMessage = "长度必须大于1")]
        public string imagePath { get; set; }

    }
}
