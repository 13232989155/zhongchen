using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class ProductImageBLL : Base.BaseBLL<ProductImageEntity>
    {
        /// <summary>
        /// 获取商品图片
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductImageEntity> ListProductImage(int productId)
        {
            return ActionDal.ActionDBAccess.Queryable<ProductImageEntity>().Where(it => it.productId == productId).ToList();
        }

        public ProductImageEntity GrtById(int productImageId)
        {
            return ActionDal.ActionDBAccess.Queryable<ProductImageEntity>().Where(it => it.productImageId == productImageId).First();
        }


        /// <summary>
        /// 删除与商品相关的图片记录
        /// </summary>
        /// <param name="productId"></param>
        public int DelByProductId(int productId)
        {
            var list = ActionDal.ActionDBAccess.Queryable<ProductImageEntity>().Where(it => it.productId == productId).ToList();
            return ActionDal.ActionDBAccess.Deleteable(list).ExecuteCommand();
        }
    }
}
