using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 企业信息设置表：banner，其他等
    /// </summary>
    [SugarTable("t_company_set")]
    public class CompanySetEntity
    {
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int companySetId { get; set; }

        /// <summary>
        /// Desc:类型
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public int setType { get; set; }

        /// <summary>
        /// Desc:键
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string keyName { get; set; }

        /// <summary>
        /// Desc:值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string keyValue { get; set; }

        /// <summary>
        /// Desc:关键字
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string keyword { get; set; }
    }
}
