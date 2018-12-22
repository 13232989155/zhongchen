using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 食谱关键字关联
    /// </summary>
    [SugarTable("t_recipe_key")]
    public partial class RecipeKeyEntity
    {
        public RecipeKeyEntity()
        {


        }
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>        
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int recipeKeyId { get; set; }

        /// <summary>
        /// Desc:食谱ID
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public int recipeId { get; set; }

        /// <summary>
        /// Desc:关键字ID
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public int keywordId { get; set; }


        /// <summary>
        /// Desc:内容
        /// Default:
        /// Nullable:False
        /// </summary>      
        [SugarColumn(IsIgnore =true)]
        public string contents { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:False
        /// </summary>        
        [SugarColumn(IsIgnore = true)]
        public string remark { get; set; }
    }
}
