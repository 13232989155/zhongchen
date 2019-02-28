using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RecipeImageBLL : Base.BaseBLL<RecipeImageEntity>
    {
        /// <summary>
        /// 获取食谱详细图片
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public List<RecipeImageEntity> ListByRecipeId(int recipeId)
        {
            return ActionDal.ActionDBAccess.Queryable<RecipeImageEntity>().Where(it => it.recipeId == recipeId).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipeImageId"></param>
        /// <returns></returns>
        public RecipeImageEntity GrtById(int recipeImageId)
        {
            return ActionDal.ActionDBAccess.Queryable<RecipeImageEntity>().Where(it => it.recipeImageId == recipeImageId).First();
        }
    }
}
