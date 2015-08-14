using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace CS5412_HOST
{
    class Program
    {
        static void Main()
        {
            Environment.SetEnvironmentVariable("ISIS_UNICAST_ONLY", "true");
            Environment.SetEnvironmentVariable("SET ISIS_OOBVIATCP", "true");
            Environment.SetEnvironmentVariable("SET ISIS_PORTNOp", "8080");
            Environment.SetEnvironmentVariable("SET ISIS_HOSTS", "52.24.176.122,172.31.17.46,52.24.105.170,172.31.47.137");
            using (System.ServiceModel.ServiceHost host = new
                System.ServiceModel.ServiceHost(typeof(CS5412_Service.CS5412_Service)))
            {
                host.Open();
                CS5412_Service.CS5412_Service.Isis_Start();
                Console.WriteLine("Host started @ " + DateTime.Now.ToString());
                Console.ReadLine();
            }
        }
    }

}
