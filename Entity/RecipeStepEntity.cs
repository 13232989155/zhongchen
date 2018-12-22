using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 食谱步骤
    /// </summary>
    [SugarTable("t_recipe_step")]
    public partial class RecipeStepEntity
    {
        public RecipeStepEntity()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int recipeStepId { get; set; }

        /// <summary>
        /// Desc:食谱ID
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public int recipeId { get; set; }

        /// <summary>
        /// Desc:内容
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string contents { get; set; }

        /// <summary>
        /// Desc:步骤图片
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string imageUrl { get; set; }
    }
}
