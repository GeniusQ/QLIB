using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QLIB.LINQ
{
    public static class LinqEx
    {
        public static void BeginLog(this System.Data.Linq.DataContext s, string filePath)
        {
            var sw = new StreamWriter(filePath,true);
            s.Log = sw;
            sw.AutoFlush = true;
        }
        public static void EndLog(this System.Data.Linq.DataContext s)
        {
            if (s.Log != null)
            {
                s.Log.Flush();
                s.Log.Dispose();
            }
        }

    }
}
