using System;
using System.Web;
using System.Web.UI;
using QLIB.Base;

namespace QLIB.Web
{
	/// <summary>
	/// 网页上对话框相关的功能
    /// master
	/// </summary>
	public static class Dialog
	{
        [Obsolete("此方法已经放弃，请使用Page.ShowMessageBox")]
        public static void MessageBox(string msg)
		{
            HttpContext.Current.Response.Write(CreateAlertScript(msg));
		}

        public static string CreateAlertScript(string text)
        {
            return CreateAlertScript(text, false);
        }

        public static string CreateAlertScript(string text,bool addTag)
        {
            string script = "alert(\"" + text + "\")";
            if(addTag)
                script = AddScriptTag(script);
            return script;
        }

        private static string AddScriptTag(string script)
        {
            script = "<script language=\"javascript\">" + script + "</script>";
            return script;
        }

        public static string CreateConfirmScript(string text, bool addTag)
        {
            string script = "alert(\"" + text + "\")";
            if (addTag)
                script = AddScriptTag(script);
            return script;
        }
        public static string CreateConfirmScript(string text)
        {
            return CreateConfirmScript(text, false);
        }
    }
}
