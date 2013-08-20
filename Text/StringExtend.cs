using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QLIB.Text
{
    public static class StringExtend
    {
        /// <summary>
        /// 替换掉于正则表达式冲突的字符
        /// " >> '
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        /// [ ] >> 〔〕
        /// { } >> ﹛﹜
        /// ( ) >> 「」
        /// ? >> ¿
        /// - >> ⑴
        public static string ReplaceClashCharWithRegular(this String self)
        {
            string r = self.Replace("\"","'");
            //r = r.Replace("{", "﹛");
            //r = r.Replace("}", "﹜");
            //r = r.Replace("[", "〔");
            //r = r.Replace("]", "〕");
            //r = r.Replace("(", "「");
            //r = r.Replace(")", "」");
            //r = r.Replace("?", "¿");
            //r = r.Replace("-", "⑴");
            return r;
        }

        public static string GetInnerText(this string self)
        {
            return Regex.Replace(self, "</?.*?>", "");
        }

        public static int ToInt32(this string self)
        {
            return Convert.ToInt32(self);
        }
    }
}
