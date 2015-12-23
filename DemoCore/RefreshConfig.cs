using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Converter;

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

    /// <summary>
    /// 读取配置的工具
    /// </summary>
    public static class ConfigUtil
    {
        // Fields
        private static readonly NameValueCollection _appSettings;
        static ConfigUtil()
        {
            _appSettings = ConfigurationManager.AppSettings;
        }

        public static string GetString(string key, string defaultValue = "")
        {
            return GetValue<string>(key, defaultValue);
        }

        public static T GetValue<T>(string key, T defaultValue = default(T))
        {
            //if (!ConfigReader.ConfigDict.ContainsKey(key))
            //{
            //    return defaultValue;
            //}

            //var value = ConfigReader.ConfigDict[key];

            //return string.IsNullOrEmpty(value) ? defaultValue : value;
            string str = _appSettings[key];
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (typeof(T) == typeof(string))
                    return (T)((object)str);

                bool success;
                var value = ConverterFactory.Converters[typeof(T)].ConvertTo(str, out success);
                if (success) return (T)value;
            }

            return defaultValue;
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            string value = GetString(key);
            bool i = defaultValue;
            if (bool.TryParse(value, out i))
                return i;
            return defaultValue;
        }
        public static int GetIntRange(string key, int min, int max, int defaultValue = 0)
        {
            int i = GetInt(key, defaultValue);
            if (i < min)
                return defaultValue;
            if (i > max)
                return defaultValue;
            return i;
        }
        public static int GetInt(string key, int defaultValue = 0)
        {
            string value = GetString(key);
            int i = defaultValue;
            if (int.TryParse(value, out i))
                return i;
            return defaultValue;
        }
        public static long GetLong(string key, long defaultValue = 0)
        {
            string value = GetString(key);
            long i = defaultValue;
            if (long.TryParse(value, out i))
                return i;
            return defaultValue;
        }

        public static decimal GetDecimal(string key, decimal defaultValue = 0)
        {
            string value = GetString(key);
            decimal i = defaultValue;
            if (decimal.TryParse(value, out i))
                return i;
            return defaultValue;
        }

        public static double GetDouble(string key, double defaultValue = 0)
        {
            string value = GetString(key);
            double i = defaultValue;
            if (double.TryParse(value, out i))
                return i;
            return defaultValue;
        }

        public static DateTime GetDate(string key, DateTime defaultValue = default(DateTime))
        {
            string value = GetString(key);
            DateTime i = defaultValue;
            if (DateTime.TryParse(value, out i))
                return i;
            return defaultValue;
        }
    }
}
