using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 食谱
    /// </summary>
    [SugarTable("t_recipe")]
    public partial class RecipeEntity
    {
        public RecipeEntity()
        {


        }
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int recipeId { get; set; }

        /// <summary>
        /// Desc:标题
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "不能为空")]
        public string title { get; set; }

        /// <summary>
        /// Desc:简介
        /// Default:
        /// Nullable:False
        /// </summary>   
        public string remark { get; set; }

        /// <summary>
        /// Desc:封面图片
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string coverImage { get; set; }

        /// <summary>
        /// Desc:食谱分类
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string typeName { get; set; }

        /// <summary>
        /// Desc:食材
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string materials { get; set; }

        /// <summary>
        /// Desc:食谱外链
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string explicitLink { get; set; }

        /// <summary>
        /// Desc:创建者
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
