using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZhongChen.Models
{
    /// <summary>
    /// 错误消息类
    /// </summary>
    public class ErrorMsg
    {
        #region 账户模块错误提示 code从10001到19999

        /// <summary>
        /// 管理员账户出错代码
        /// </summary>
        public const string ADMIN_ACCOUNT_CODE = "10001";

        /// <summary>
        /// 管理员账户出错提示
        /// </summary>
        public const string ADMIN_ACCOUNT_ERROR_MESSAGE = "账户不存在或者密码错误";

        // <summary>
        /// 管理员账户无权限提示
        /// </summary>
        public const string ADMIN_ACCOUNT_NO_ACCESS = "账户没有权限";

        // <summary>
        /// 管理员账户无权限提示
        /// </summary>
        public const string ADMIN_ACCOUNT_NO_ACCESS_CODE = "9999";

        #endregion
    }
}