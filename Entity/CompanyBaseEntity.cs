using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 企业信息
    /// </summary>
    [SugarTable("t_company_base")]
    public class CompanyBaseEntity
    {
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>   
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int companyBaseId { get; set; }

        /// <summary>
        /// Desc:LOGO
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string logo { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string name { get; set; }

        /// <summary>
        /// Desc:地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string companyAddress { get; set; }

        /// <summary>
        /// Desc:邮编
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string post { get; set; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string tel { get; set; }

        /// <summary>
        /// Desc:备用电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string bakTel { get; set; }

        /// <summary>
        /// Desc:联系人手机号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string phone { get; set; }

        /// <summary>
        /// Desc:联系人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string contactUser { get; set; }

        /// <summary>
        /// Desc:传值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string fax { get; set; }

        /// <summary>
        /// Desc:邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string mail { get; set; }
    }
}

