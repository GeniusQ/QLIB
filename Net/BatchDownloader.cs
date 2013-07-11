using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Text;

namespace QLIB.Net
{
    public class BatchDownloader
    {
        public BatchDownloader()
        {
            Interval = 1000;
            Running = false;
            Encoding = Encoding.Default;
            jobs = new Dictionary<string, BatchDownloadJob>();
            MaxRetryCount = 3;
        }

        public event Action<object,BatchDownloadJob> SingleJobFinished;
        public event EventHandler AllJobFinished;
        public event Action<object, BatchDownloadJob> JobError;

        #region 下载任务管理
        private Dictionary<string, BatchDownloadJob> jobs;
        private Dictionary<string, BatchDownloadJob> failedJobs = new Dictionary<string, BatchDownloadJob>();
        /// <summary>
        /// 失败的任务
        /// </summary>
        public Dictionary<string, BatchDownloadJob> FailedJobs
        {
            get { return failedJobs; }
        }

        public int AddJob(BatchDownloadJob job)
        {
            string key = job.Key;
            //任务已存在
            if (jobs.Count>0 && jobs.Keys.Contains(key) == true)
                return -1;
            else
            {
                lock (jobs)
                {
                    jobs.Add(key, job);
                }
                return 1;
            }
        }
        public bool RemoveJob(string key)
        {
            bool b = false;
            lock (jobs)
            {
                b = jobs.Remove(key);
            }
            return b;
        }
        #endregion

        /// <summary>
        /// 所有任务最大重试次数
        /// </summary>
        public int MaxRetryCount { get; set; }

        /// <summary>
        /// 下载任务的间隔时间，以毫秒为单位，默认为1000
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 编码器，默认为本机代码
        /// </summary>
        public Encoding Encoding { get; set; }

        private Thread mainThread = null;
        public bool Running { get; private set; }
        public void Start()
        {
            if (Running == false)
            {
                mainThread = new Thread(() =>
                {
                    while (true)
                    {
                        if (jobs.Count <= 0)
                        {//如果没有任务，停5秒再重试
                            Log("没有任务可执行", "");
                            if (AllJobFinished != null)
                                AllJobFinished(this, new EventArgs());
                            Thread.Sleep(5000);
                            continue;
                        }

                        var hs = jobs.First();
                        var job = hs.Value;
                        RemoveJob(hs.Key);  //已完成，移除
                        try
                        {
                            if (job.FailCount == 0)
                                Log("开始下载：" + hs.Key, job.Url);
                            else
                                Log("重新下载：" + hs.Key, job.Url);
                            
                            //开始阻塞
                            WebClient wc = new WebClient();
                            wc.Encoding = Encoding;
                            string html = wc.DownloadString(job.Url);
                            wc.Dispose();
                            job.Result = html;
                            Log("下载完成：" + hs.Key, job.Url);

                            if (SingleJobFinished != null)
                                SingleJobFinished(this, job);                            
                        }
                        catch (Exception ex)
                        {
                            job.FailCount++;
                            Log("下载 " + hs.Key + " 出错", ex.ToString());
                            if (job.FailCount < this.MaxRetryCount)
                                AddJob(job);
                            else
                            {
                                if (JobError != null)
                                    JobError(this, job);

                                failedJobs.Add(hs.Key,job);
                                Log("取消下载！", "下载 " + hs.Key + " 出错超过 " + MaxRetryCount + " 次！");
                            }
                        }
                        Thread.Sleep(Interval);
                    }
                });
                Running = true;
                mainThread.IsBackground = true;
                mainThread.Start();
            }
        }

        public void Stop()
        {
            if (mainThread != null)
            {
                try
                {
                    Running = false;
                    mainThread.Abort();
                }
                catch
                {
                }
            }
        }

        protected void Log(string subject, string content)
        {
            Trace.WriteLine("["+DateTime.Now.ToString() + "] " + subject);
            Trace.WriteLine(content);
        }
    }

    public class BatchDownloadJob
    {
        public object StateObject { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public string Result { get; internal set; }
        public int FailCount { get; internal set; }
    }
}
