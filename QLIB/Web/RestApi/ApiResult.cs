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
        public int ErrorCode { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        

        public ApiResult()
        {
            Success = false;
            Message = "API未执行";
        }

        public void Complete(object data,string msg)
        {
            Success = true;
            Data = data;
            Message = msg ?? "";
        }

        public void Fail(string msg, int code=-1,object data=null)
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
        }
    }
}
