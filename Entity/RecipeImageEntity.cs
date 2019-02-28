using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 食谱详细图
    /// </summary>
    [SugarTable("t_recipe_image")]
    public partial class RecipeImageEntity
    {
        public RecipeImageEntity()
        {


        }
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int recipeImageId { get; set; }

        /// <summary>
        /// Desc:产品ID
        /// Default:-1
        /// Nullable:False
        /// </summary>         
        public int recipeId { get; set; }

        /// <summary>
        /// Desc:照片路径
        /// Default:
        /// Nullable:False
        /// </summary>      
        public string url { get; set; }

    }
}
