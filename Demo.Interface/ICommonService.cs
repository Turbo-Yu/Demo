using System.ServiceModel;

namespace Demo.Interface
{
    [ServiceContract]
    public interface ICommonService
    {
        [OperationContract]
        string Test();
    }
}
