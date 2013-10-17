using System;
using System.Web;
using System.Web.UI;
using QLIB.Base;

namespace QLIB.Web
{
	/// <summary>
	/// ��ҳ�϶Ի�����صĹ���
    /// master
	/// </summary>
	public static class Dialog
	{
        [Obsolete("�˷����Ѿ���������ʹ��Page.ShowMessageBox")]
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
