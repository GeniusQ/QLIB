using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using QLIB.Base;

/// <summary>
///关于网站开发用到的基础扩展方法
/// </summary>
namespace QLIB.Web
{
    public static class WebEx
    {
        public static string GetSelectedValues(this CheckBoxList ctl)
        {
            string values = "";
            foreach (ListItem item in ctl.Items)
            {
                if (item.Selected == true)
                    values += item.Value + ",";
            }
            if (values.Length > 0)
                values = values.Substring(0, values.Length - 1);
            return values;
        }

        public static void SetSelectedValues(this CheckBoxList ctl, List<string> values)
        {
            foreach (ListItem item in ctl.Items)
            {
                if (values.Contains(item.Value))
                {
                    item.Selected = true;
                    values.Remove(item.Value);
                }
            }

        }

        public static void NewWindow(this Page p, string url)
        {
            var sm = ScriptManager.GetCurrent(p);
            if (sm != null)
                ScriptManager.RegisterStartupScript(p, p.GetType(), "NW", "window.open('{0}','NW');".Format(new object[] { url }), true);
            else
                p.ClientScript.RegisterStartupScript(p.GetType(), "NW", "window.open('{0}','NW');".Format(new object[] { url }), true);

        }

        public static void ShowMessageBox(this Page p, string scriptName, string msg)
        {
            var sm = ScriptManager.GetCurrent(p);
            if (sm != null)
                ScriptManager.RegisterStartupScript(p, p.GetType(), scriptName, "alert('{0}');".Format(new object[] { msg }), true);
            else
                p.ClientScript.RegisterStartupScript(p.GetType(), scriptName, "alert('{0}');".Format(new object[] { msg }), true);
        }

    }
}