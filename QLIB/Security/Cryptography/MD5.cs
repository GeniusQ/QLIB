using System;
using System.Security.Cryptography;
using System.Text;

namespace QLIB.Security.Cryptography
{
    /// <summary>
    /// MD5������
    /// </summary>
    public sealed class MD5
    {
        private MD5()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// MD5����
        /// </summary>
        /// <param name="source">�����ַ���</param>
        /// <returns>�����ַ���</returns>
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
