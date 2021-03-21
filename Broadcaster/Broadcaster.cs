using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Broadcaster
{
    public partial class Broadcaster : ServiceBase
    {
        public string SignalRAddress = "http://+:7717/";
        private IDisposable _serverSignalR = null;
        public Broadcaster()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _serverSignalR = WebApp.Start<StartUpSignalR>(url: SignalRAddress);
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnStop()
        {
            _serverSignalR.Dispose();
        }

        internal void Start()
        {
            OnStart(null);
        }
    }
}
