using System.ServiceModel;

namespace Demo.Interface
{
    public interface ICommonService
    {
        [OperationContract]
        string Test();
    }
}
