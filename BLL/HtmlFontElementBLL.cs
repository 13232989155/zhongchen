using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class HtmlFontElementBLL : Base.BaseBLL<HtmlFontElementEntity>
    {
        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public List<HtmlFontElementEntity> List()
        {
            return ActionDal.ActionDBAccess.Queryable<HtmlFontElementEntity>().ToList();
        }

        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlFontElementEntity GetById(int id)
        {
            return ActionDal.ActionDBAccess.Queryable<HtmlFontElementEntity>().Where(it => it.id == id).First();
        }

        /// <summary>
        /// 根据key 获取实体
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public HtmlFontElementEntity GetByKey(string key)
        {
            return ActionDal.ActionDBAccess.Queryable<HtmlFontElementEntity>().Where(it => it.keys == key).First();
        }

        /// <summary>
        /// 根据key[]获取列表
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public List<HtmlFontElementEntity> ListByKeyInts(string[] keys)
        {
            return ActionDal.ActionDBAccess.Queryable<HtmlFontElementEntity>().In(it => it.keys, keys).ToList();
        }
    }
}
