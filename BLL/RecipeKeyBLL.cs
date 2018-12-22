using Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RecipeKeyBLL : Base.BaseBLL<RecipeKeyEntity>
    {
        /// <summary>
        /// 获取食谱全部关键字
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public List<RecipeKeyEntity> List(int recipeId)
        {
            return ActionDal.ActionDBAccess.Queryable<RecipeKeyEntity,KeywordEntity>((rk,k) => new object[] {JoinType.Inner,rk.keywordId == k.keywordId})
                    .Select((rk, k) => new RecipeKeyEntity {
                        contents = k.contents,
                        keywordId = rk.keywordId,
                        recipeId = rk.recipeId,
                        recipeKeyId = rk.recipeKeyId,
                        remark = k.remark
                    })
                    .Where(rk => rk.recipeId == recipeId).ToList();
        }
    }
}
