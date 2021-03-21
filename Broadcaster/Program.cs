using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Broadcaster
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            string ServiceName = $"Broadcaster";

#if !RunAsService

            Broadcaster broadcasterService = new Broadcaster();
            broadcasterService.ServiceName = ServiceName;
            broadcasterService.AutoLog = true;
            broadcasterService.CanStop = true;
            broadcasterService.Start();

            Thread.Sleep(Timeout.Infinite);

#else

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new NaggiarBroadcaster()
                {
                    ServiceName = ServiceName,
                    AutoLog = true,
                    CanStop = true
                }
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
