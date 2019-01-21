using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class HtmlImageTextElementBLL:Base.BaseBLL<HtmlImageTextElementEntity>
    {


        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public List<HtmlImageTextElementEntity> List()
        {
            return ActionDal.ActionDBAccess.Queryable<HtmlImageTextElementEntity>().ToList();
        }

        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlImageTextElementEntity GetById(int id)
        {
            return ActionDal.ActionDBAccess.Queryable<HtmlImageTextElementEntity>().Where(it => it.id == id).First();
        }

        /// <summary>
        /// 根据key 获取实体
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public HtmlImageTextElementEntity GetByKey(string key)
        {
            return ActionDal.ActionDBAccess.Queryable<HtmlImageTextElementEntity>().Where(it => it.keys == key).First();
        }

        /// <summary>
        /// 根据key[]获取列表
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public List<HtmlImageTextElementEntity> ListByKeyInts(string[] keys)
        {
            return ActionDal.ActionDBAccess.Queryable<HtmlImageTextElementEntity>().In( it => it.keys, keys).ToList();
        }

    }
}
