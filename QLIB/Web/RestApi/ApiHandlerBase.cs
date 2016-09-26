using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace QLIB.Web.RestApi
{
    /// <summary>
    /// HttpApi的基类
    /// </summary>
    public abstract class ApiHandlerBase : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected ApiResult ret { get; private set; }

        public void ProcessRequest(HttpContext context)
        {

            ret = new ApiResult();

            var cmd = context.Request.QueryString["cmd"] ?? "";
            ret.Page = Convert.ToInt32(context.Request.Form["page"] ?? "1");
            ret.Size = Convert.ToInt32(context.Request.Form["size"] ?? "20");
            try
            {
                Do(context.Request, cmd);
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.Message = "服务异常【"+ cmd + "】";
                ret.Data = ex.ToString();
            }


            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.ContentType = "text/plain";
            var json = GetJson(ret);
            context.Response.Write(json);
            context.Response.Flush();
        }

        public abstract void Do(HttpRequest req, string cmd);

        public abstract string GetJson(object obj);

    }

    public static class HttpRequestEx
    {
        public static string GetString(this HttpRequest obj, string key)
        {
            return obj[key] ?? string.Empty;
        }

        public static long GetLong(this HttpRequest obj, string key)
        {
            return Convert.ToInt64(obj[key] ?? "0");
        }
        public static Int32 GetInt(this HttpRequest obj, string key)
        {
            return Convert.ToInt32(obj[key] ?? "0");
        }
        public static decimal GetMoney(this HttpRequest obj, string key)
        {
            return Convert.ToDecimal(obj[key] ?? "0");
        }
    }

}

