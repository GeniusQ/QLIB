using System;
using System.Collections.Generic;
using System.Text;

namespace QLIB.Log
{
    public interface ILogger
    {
        /// <summary>
        /// д����־
        /// </summary>
        /// <param name="catalog">��־����</param>
        /// <param name="msg">��־����</param>
        /// <returns>������</returns>
        int Write(string catalog, string msg);

        /// <summary>
        /// д���޷�����־
        /// </summary>
        /// <param name="msg">��־����</param>
        /// <returns>������</returns>
        int Write(string msg);
    }
}
