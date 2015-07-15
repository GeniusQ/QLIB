using System;
using System.Security.Cryptography;
using System.Text;

namespace QLIB.Security.Cryptography
{
    /// <summary>
    /// MD5加密类
    /// </summary>
    public sealed class MD5
    {
        private MD5()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">明文字符串</param>
        /// <returns>密文字符串</returns>
        public static string Encode(string source)
        {
            System.Security.Cryptography.MD5 md5 = MD5CryptoServiceProvider.Create();

            byte[] buf = Encoding.UTF8.GetBytes(source);
            byte[] maskBuf = md5.ComputeHash(buf);
            string result = BitConverter.ToString(maskBuf).ToUpper();
            result = result.Replace("-", "");
            return result;
        }
    }
}
