using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLIB.Base
{
    /// <summary>
    ///BastTypeEx 的摘要说明
    /// </summary>
    public static class BastTypeEx
    {
        public static int ToInt(this bool a)
        {
            return a ? 1 : 0;
        }

        public static int ToInt(this string a)
        {
            return Convert.ToInt32(a);
        }

        public static string Format(this string a, params object[] args)
        {
            return String.Format(a, args);
        }
    }
}