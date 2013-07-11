using System;
using System.Collections.Generic;
using System.Text;

namespace QLIB.Log
{
    public interface ILogger
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="catalog">日志分类</param>
        /// <param name="msg">日志详情</param>
        /// <returns>处理结果</returns>
        int Write(string catalog, string msg);

        /// <summary>
        /// 写入无分类日志
        /// </summary>
        /// <param name="msg">日志详情</param>
        /// <returns>处理结果</returns>
        int Write(string msg);
    }
}
