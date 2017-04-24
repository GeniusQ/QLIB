using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLIB.Net.MQ
{
    /// <summary>
    /// 消息队列接口
    /// </summary>
    interface IClient
    {
        string Connection(string ip, string host, string channel, string account, string password);
        string Send(string msg);
    }
}
