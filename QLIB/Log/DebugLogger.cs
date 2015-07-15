using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Web;

namespace QLIB.Log
{
    public class DebugLogger:ILogger
    {
        public static string LogFilePath = "Log.txt";
        private static bool _instanced = false;

        static DebugLogger()
        {
            LogFilePath = HttpContext.Current.Server.MapPath(LogFilePath);
        }

        public DebugLogger()
        {
            lock (this)
            {
                if (_instanced == false)
                {
                    TextWriterTraceListener tw = new TextWriterTraceListener(LogFilePath);
                    Debug.Listeners.Add(tw);
                    _instanced = true;
                }
            }
        }

        #region ILogger 成员

        public int Write(string catalog, string msg)
        {
            using (FileStream fs = new FileStream(LogFilePath,FileMode.Append,FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                if (catalog != null && catalog != String.Empty)
                    sw.WriteLine("分类："+catalog);
                sw.WriteLine(msg);
                sw.WriteLine(DateTime.Now.ToString());
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            return 1;
        }

        public int Write(string msg)
        {
            return Write(null, msg);
        }

        #endregion
    }
}
