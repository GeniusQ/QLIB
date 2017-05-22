using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLIB.Web.RestApi
{
    /// <summary>
    /// http api返回数据包
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 错误编号
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 错误提示或者附加说明
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 读取操作涉及的对象总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 读取页码，默认1开始
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 每页读取记录数量
        /// </summary>
        public int Size { get; set; }
        
        /// <summary>
        /// 初始化
        /// </summary>
        public ApiResult()
        {
            Success = false;
            Message = "API未执行";
        }

        /// <summary>
        /// 构建执行成功的返回数据包
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="msg">附加说明</param>
        /// <returns></returns>
        public ApiResult Complete(object data,string msg=null)
        {
            Success = true;
            Data = data;
            Message = msg ?? "";
            return this;
        }

        /// <summary>
        /// 构建执行失败的返回数据包
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <param name="code">错误编号</param>
        /// <param name="data">详细错误信息</param>
        /// <returns></returns>
        public ApiResult Fail(string msg, int code=-1,object data=null)
        {
            Success = false;
            if (string.IsNullOrWhiteSpace(msg))
                throw new ApplicationException("错误原因不能为空");
            else
            {
                Message = msg;
            }

            ErrorCode = code;
            Data = data;

            return this;
        }
    }
}
