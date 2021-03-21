using Microsoft.AspNet.SignalR;
using Owin;

namespace Broadcaster
{
    public class StartUpSignalR
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {

            appBuilder.MapSignalR($"/livehub", new HubConfiguration() { EnableDetailedErrors = true });
            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;
            GlobalHost.Configuration.DefaultMessageBufferSize = 40;
        }
    }
}
