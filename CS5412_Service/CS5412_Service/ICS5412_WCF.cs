using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CS5412_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICS5412_WCF" in both code and config file together.
    [ServiceContract]
    public interface ICS5412_WCF
    {
        [OperationContract]
        void GetMessage(string name);

        [OperationContract]
        void Get(string key);

        [OperationContract]
        void Set(string key, string value, string attkey);
    }
}
