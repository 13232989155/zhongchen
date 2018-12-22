﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZhongChen.Models
{
    /// <summary>
    /// 数据结果
    /// </summary>
    public class DataResult
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        public string code
        {
            get;
            set;
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string error
        {
            get;
            set;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public dynamic data
        {
            get;
            set;
        }

        /// <summary>
        /// 返回的数据类型
        /// </summary>
        public int dataType
        {
            get;
            set;
        }

        public DataResult()
        {

        }

        public DataResult(dynamic data)
        {
            this.code = "200";
            this.data = data;
        }

        public DataResult(string error, string code)
        {
            this.code = code;
            this.error = error;
        }
    }
}