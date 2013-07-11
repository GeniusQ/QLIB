using System;
using System.Windows.Forms;

namespace QLIB.Window
{
	/// <summary>
	/// �Ի�����
	/// ���õĶԻ���ģʽ
	/// </summary>
	public sealed class Dialog
	{
		private Dialog()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

    /// <summary>
    /// ���󴰿�
    /// </summary>
    /// <param name="msg">��Ϣ����</param>
    public static void Error(string msg)
    {
      MessageBox.Show(msg,"����",MessageBoxButtons.OK,MessageBoxIcon.Error);
    }

    /// <summary>
    /// ��ʾ����
    /// </summary>
    /// <param name="msg">��Ϣ����</param>
    public static void Information(string msg)
    {
      MessageBox.Show(msg,"��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
    }

    /// <summary>
    /// ���ʴ���
    /// </summary>
    /// <param name="msg">��Ϣ����</param>
    public static void Question(string msg)
    {
      MessageBox.Show(msg,"����",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
    }
  }
}
