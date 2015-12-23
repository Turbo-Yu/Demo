using Demo.Datas.Interface;

namespace Demo.Datas.Manager
{
    public class ManagerFactory
    {
        public static IRespository<T> CreateCRMInstance<T>() where T : class
        {
            return new BaseManager<T>();
        }
    }
}
