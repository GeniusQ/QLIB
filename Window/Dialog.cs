using System;
using System.Windows.Forms;

namespace QLIB.Window
{
	/// <summary>
	/// 对话框类
	/// 常用的对话框模式
	/// </summary>
	public sealed class Dialog
	{
		private Dialog()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

    /// <summary>
    /// 错误窗口
    /// </summary>
    /// <param name="msg">信息内容</param>
    public static void Error(string msg)
    {
      MessageBox.Show(msg,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
    }

    /// <summary>
    /// 提示窗口
    /// </summary>
    /// <param name="msg">信息内容</param>
    public static void Information(string msg)
    {
      MessageBox.Show(msg,"提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
    }

    /// <summary>
    /// 提问窗口
    /// </summary>
    /// <param name="msg">信息内容</param>
    public static void Question(string msg)
    {
      MessageBox.Show(msg,"问题",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
    }
  }
}
