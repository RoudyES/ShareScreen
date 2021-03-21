using CommunicationLibrary.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace Broadcaster.SignalR
{
    [HubName("livehub")]
    public class SignalR : Hub
    {
        public static ConcurrentDictionary<string, string> ConnectedClients = new ConcurrentDictionary<string, string>();

        public SignalR()
        {

        }

        public void Produce(InputDataComm data, string clientId)
        {
            if (ConnectedClients.ContainsKey(clientId))
            {
                Clients.Client(ConnectedClients[clientId]).Produce(data);
            }
        }

        public void ProduceMouseMove(int x, int y, string clientId)
        {
            if (ConnectedClients.ContainsKey(clientId))
            {
                Clients.Client(ConnectedClients[clientId]).ProduceMouseMove(x, y);
            }
        }

        public void ProduceScreenshot(byte[] data, string width, string height, string clientId)
        {
            Task.Run(() =>
            {
                if (ConnectedClients.ContainsKey(clientId))
                {
                    Clients.Client(ConnectedClients[clientId]).ProduceScreenshot(data, width, height);
                }
            });
        }

        public void StopScreenShare(string hostId)
        {
            if (ConnectedClients.ContainsKey(hostId))
            {
                Clients.Client(ConnectedClients[hostId]).StopScreenShare();
            }
        }

        public void TryConnect(string username, string password, string hostId)
        {
            if (ConnectedClients.ContainsKey(username))
            {
                Clients.Client(ConnectedClients[username]).TryConnect(username, password, hostId);
            }
        }

        public void AuthenticateSuccess(string clientId, string hostId)
        {
            if (ConnectedClients.ContainsKey(clientId))
            {
                Clients.Client(ConnectedClients[clientId]).AuthenticateSuccess(hostId);
            }
        }

        public void RegisterClient(string hubId, string screenShareId)
        {
            ConnectedClients.TryAdd(screenShareId, hubId);
            Clients.Client(hubId).ClientConnected(true);
        }


        public override Task OnReconnected()
        {
            string clientId = Context.ConnectionId;

            Clients.Client(clientId).RequestToReSub();
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string clientId = Context.ConnectionId;

            for (int i = ConnectedClients.Count - 1; i >= 0; i--)
                if (ConnectedClients.ElementAt(i).Value.Equals(clientId))
                    ConnectedClients.TryRemove(ConnectedClients.ElementAt(i).Key, out _);

            return base.OnDisconnected(stopCalled);
        }
    }
}
