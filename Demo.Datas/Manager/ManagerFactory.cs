using Demo.Datas.Interface;

namespace Demo.Datas.Manager
{
    public static class ManagerFactory
    {
        public static IRespository<T> CreateInstance<T>() where T : class
        {
            return new BaseManager<T>();
        }
    }
}
