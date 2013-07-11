using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace QLIB.Net
{
    /// <summary>
    /// WebClient 增强版本，可以获取cookie和session
    /// </summary>
    public class WebClientEx:WebClient
    {
        //全局模式
        private static WebClientEx self = new WebClientEx();
        public static WebClientEx Instance
        {
            get
            {
                return self;
            }
        }

        /// <summary>
        /// 过滤掉html和正则表达式相冲突的符号
        /// </summary>
        public const string MaskRegexChars = "\\t|\\r|\\n|\"|'|\\\\|\\(|\\)";

        /// <summary>
        /// CookieContainer
        /// </summary>
        //private CookieContainer cookieContainer = new CookieContainer();

        /// <summary>
        /// Cookie String
        /// </summary>
        public string Cookie {
            get { return cookie; }
            set { cookie = value; }
        }
        public string cookie = string.Empty;

        /// <summary>
        /// 编码器
        /// </summary>
        public Encoding Coder { get; set; }

        /// <summary>
        /// 提交表单内容类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 设置引用页，防止有些网站通过引用页来识别是否为盗链
        /// </summary>
        public string Referer { get; set; }

        public WebClientEx()
        {
            Coder = Encoding.UTF8;
            Referer = "http://www.g.cn/";
            this.Headers.Add("Referer", Referer);
            ContentType = "application/x-www-form-urlencoded";
            RequestTimeout = 15000;
        }

        /// <summary>
        /// 同步对象
        /// </summary>
        private static object AsyncObject = new object();

        /// <summary>
        /// 产生时间戳
        /// </summary>
        public static string TimeStamp
        {
            get
            {
                double r = (DateTime.Now.AddHours(-8) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                r *= 100;
                return Math.Floor(r).ToString();
            }
        }

        /// <summary>
        /// 将cookie字符串变成cookie
        /// 放入CookieContainer中
        /// </summary>
        /// <param name="myCookieContainer"></param>
        /// <param name="cookstr"></param>
        /// <param name="domain"></param>
        //private void AddCookiesToCookieContainer(CookieContainer myCookieContainer, string[] cookstr, string domain)
        //{
        //    foreach (string str in cookstr)
        //    {
        //        string[] cookieNameValue = str.Split('=');
        //        Cookie ck = new Cookie();
        //        ck.Name = cookieNameValue[0].Trim().ToString();
        //        if (cookieNameValue.Length > 1)
        //            ck.Value = cookieNameValue[1].Trim().ToString();
        //        ck.Domain = domain;
        //        myCookieContainer.Add(ck);
        //    }
        //}

        /// <summary>
        /// 清空cookie
        /// </summary>
        public void ClearCookie()
        {
            Cookie = string.Empty;
            //cookieContainer = new CookieContainer();
        }

        /// <summary>
        /// 在Cookie容器中增加新的Cookie
        /// </summary>
        /// <param name="cookies">cookie数组</param>
        /// <param name="domain">Cookie所属的域，格式："www.18ra.cn"</param>
        //public void AddCookies(string[] cookies, string domain)
        //{
        //    AddCookiesToCookieContainer(cookieContainer, cookies, domain);
        //}
        public int RequestTimeout { get; set; }
        /// <summary>
        /// 在取得请求流的时候，设置cookie
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            request.Timeout = RequestTimeout; //15秒内没有返回，算失败
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.Headers["Cookie"] = Cookie;
            }
            return request;
        }

        /// <summary>
        /// 发出请求
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="postData">post的数据</param>
        /// <returns>返回的数据</returns>
        public string DownloadString(string url, string postData)
        {
            string r = null;

            Trace.WriteLine("发送请求：" + url);

            byte[] buf = null;

            //取消同步机制，交给调用者自行处理

            this.Headers["Referer"] = Referer;

            try
            {
                if (string.IsNullOrEmpty(postData) == false)
                {//POST数据
                    this.Headers.Add("Content-Type", ContentType);
                    buf = this.UploadData(url, "POST", Coder.GetBytes(postData));
                    Console.WriteLine("POST："+postData);
                }
                else
                {//GET数据
                    buf = this.DownloadData(url);
                }
                r = Coder.GetString(buf, 0, buf.Length);
                //Console.WriteLine(this.Cookie);
            }
            catch (Exception ex)
            {
                Trace.TraceError("发送请求失败：" + ex.ToString());
                self = new WebClientEx();
                self.Cookie = this.Cookie;
            }

            return r;
        }
    }
    
    public class RequestData
    {
        public string Url { get; set; }
        public string PostData { get; set; }
    }
}
