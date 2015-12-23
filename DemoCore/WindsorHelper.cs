using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace DemoCore
{
    public class WindsorHelper
    {
        private static readonly WindsorContainer container;

        static WindsorHelper()
        {
            var sectionKey = "yhh.dem/castle";
            container = new WindsorContainer(new XmlInterpreter(new ConfigResource(sectionKey)));
        }

        /// <summary>
        /// 解析多个服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] ResolveAll<T>()
        {
            return container.ResolveAll<T>();
        }

        /// <summary>
        /// 解析一个服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Resulve<T>(string key = "")
        {
            if (string.IsNullOrEmpty(key))
                return container.Resolve<T>();
            return container.Resolve<T>(key);
        }
    }
}
