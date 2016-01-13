using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Demo.Core;

namespace DemoCore
{
    public interface IRefreshConfig
    {
        bool RefreshConfig();
    }

    public class RefreshConfigThread : IRefreshConfig, IDisposable
    {
        /// <summary>
        /// 定期刷新配置秒数
        /// </summary>
        private static int RefreshIntervalSeconds = 5 * 60;

        /// <summary>
        /// 登记的自动刷新项目
        /// </summary>
        private static List<IRefreshConfig> RefreshList = new List<IRefreshConfig>();

        private static Thread _thread;

        /// <summary>
        /// 登记一个项目(如果存在则忽略)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public RefreshConfigThread Append(IRefreshConfig item)
        {
            lock (RefreshList)
            {
                if (!RefreshList.Contains(item))
                    RefreshList.Add(item);
            }
            return this;
        }

        /// <summary>
        /// 登记一个项目(如果存在则忽略)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public RefreshConfigThread Appends(params IRefreshConfig[] items)
        {
            lock (RefreshList)
            {
                foreach (var item in items)
                {
                    Append(item);
                }
            }
            return this;
        }

        ManualResetEvent _evQuit = new ManualResetEvent(false);

        /// <summary>
        /// 启动自动刷新
        /// </summary>
        public void Start()
        {
            lock (this)
            {
                RefreshConfig();
                if (_thread == null)
                    _thread = new Thread(Refresh);
            }
        }

        /// <summary>
        /// 定期刷新, 直到收到退出信号
        /// </summary>
        void Refresh()
        {
            while (!_evQuit.WaitOne(RefreshIntervalSeconds * 1000))
            {
                RefreshConfig();
            }
        }

        public bool RefreshConfig()
        {
            RefreshIntervalSeconds = ConfigUtil.GetIntRange("RefreshIntervalSeconds", 5, 3600 * 24, RefreshIntervalSeconds);
            Parallel.ForEach(RefreshList.ToArray(), i =>
            {
                try
                {
                    if (i != null)
                        i.RefreshConfig();
                }
                catch (Exception)
                {

                    throw;
                }
            });
            return true;
        }

        public void Dispose()
        {
            _evQuit.Set();
        }
    }
}
