using CommunicationLibrary.Models;
using HelpersLibrary.Helpers;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommunicationLibrary.Communication
{
    public sealed class Communicator
    {
        private static readonly Communicator instance = new Communicator();
        public delegate void SignalRReconnecting(bool connected);
        public event SignalRReconnecting Reconnecting;
        public event SignalRReconnecting ConnectionLost;

        HubConnection _hub;
        IHubProxy _proxy;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Communicator()
        {
        }

        private Communicator()
        {
            string ip = ConfigurationManager.AppSettings["BroadcasterIP"];
            string port = ConfigurationManager.AppSettings["BroadcasterPort"];
            _hub = new HubConnection($"http://{ip}:{port}/livehub");
            _hub.Closed += ConnectionClosed;
            _hub.Reconnecting += Hub_Reconnecting;

            _proxy = _hub.CreateHubProxy("livehub");

            TryToConnect();
        }

        private void Hub_Reconnecting()
        {
            Reconnecting?.Invoke(true);
        }

        private void ConnectionClosed()
        {
            ConnectionLost?.Invoke(false);
        }

        private void TryToConnect()
        {
        RetryConnection:
            try
            {

                _hub.Start().Wait();
                if (_hub.State != ConnectionState.Connected)
                {
                    Thread.Sleep(4000);
                    goto RetryConnection;
                }
            }
            catch (ThreadAbortException exp)
            {
                Thread.ResetAbort();
                Console.WriteLine("Trying to Reconnect ... ");
                goto RetryConnection;
            }
            catch (Exception ex)
            {
                Thread.Sleep(4000);
                Console.WriteLine("Trying to Reconnect ... ");
                goto RetryConnection;
            }
        }

        public static Communicator Instance
        {
            get
            {
                return instance;
            }
        }

        public void RegisterClient(string hostId)
        {
            if (_hub.State != ConnectionState.Connected)
                TryToConnect();

            _proxy.Invoke("RegisterClient", _hub.ConnectionId, hostId);
        }

        public void TryConnect(string id, string password, string hostId)
        {
            if (_hub.State != ConnectionState.Connected)
                TryToConnect();

            _proxy.Invoke("TryConnect", id, password, hostId);
        }

        public void ReadyToConnect(Action<bool> clientConnected, Action<string, string, string> tryConnect, Action<string> authenticateSuccess, Action<InputDataComm> produced, Action stopScreenShare, Action requestToResub)
        {
            _proxy.On("ClientConnected", clientConnected);
            _proxy.On("TryConnect", tryConnect);
            _proxy.On("AuthenticateSuccess", authenticateSuccess);
            _proxy.On("Produce", produced);
            _proxy.On("StopScreenShare", stopScreenShare);
            _proxy.On("RequestToReSub", requestToResub);
        }

        public void AuthenticateSuccess(string clientId, string hostId)
        {
            _proxy.Invoke("AuthenticateSuccess", clientId, hostId);
        }

        public void Produce(InputDataComm broadcastDataComm, string connectedClient)
        {
            _proxy.Invoke("Produce", broadcastDataComm, connectedClient);
        }

        public void ProduceMouseMove(int x, int y, string connectedClient)
        {
            _proxy.Invoke("ProduceMouseMove", x, y, connectedClient);
        }

        public void StopScreenShare(string hostId)
        {
            _proxy.Invoke("StopScreenShare", hostId);
        }

        public void ReadyToReceiveInput(Action<int, int> mouseMoved, Action<byte[], string, string> screenshotReceived)
        {
            _proxy.On("ProduceMouseMove", mouseMoved);
            _proxy.On("ProduceScreenshot", screenshotReceived);
        }

        public void ProduceScreenshot(byte[] image, int width, int height, string connectedClient)
        {
            _proxy.Invoke("ProduceScreenshot", image, width, height, connectedClient);
        }

        public void Disconnect(string hostId)
        {
            if (_hub.State != ConnectionState.Connected)
                _hub.Stop();
        }
    }
}
