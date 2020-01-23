using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DissertationThemes.Database.Model;


namespace DissertationThemes.ServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(DissertationThemesService)))
            {
                System.ServiceModel.Description.ServiceThrottlingBehavior throttlingBehavior =
                    new System.ServiceModel.Description.ServiceThrottlingBehavior();
                throttlingBehavior.MaxConcurrentCalls = Int32.MaxValue;
                throttlingBehavior.MaxConcurrentInstances = Int32.MaxValue;
                throttlingBehavior.MaxConcurrentSessions = Int32.MaxValue;
                host.Description.Behaviors.Add(throttlingBehavior);
                host.Open();
                Console.WriteLine("Server odstartovany");
                //DissertationThemesService dts = new DissertationThemesService();
                //Console.WriteLine(dts.GenerateDocx(1216));
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
