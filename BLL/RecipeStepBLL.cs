using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RecipeStepBLL : Base.BaseBLL<RecipeStepEntity>
    {
        /// <summary>
        /// 获取食谱的全部步骤
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public List<RecipeStepEntity> List(int recipeId)
        {
            return ActionDal.ActionDBAccess.Queryable<RecipeStepEntity>().Where(it => it.recipeId == recipeId).ToList();
        }

        public RecipeStepEntity GetById(int recipeStepId)
        {
            return ActionDal.ActionDBAccess.Queryable<RecipeStepEntity>().Where(it => it.recipeStepId == recipeStepId).First();
        }
    }
}
