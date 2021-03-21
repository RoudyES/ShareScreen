using CommunicationLibrary.Communication;
using CommunicationLibrary.Models;
using HelpersLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Timer = System.Timers.Timer;

namespace ShareScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _hostId;
        public string HostId
        {
            get => _hostId;
            set
            {
                _hostId = value;
                NotifyPropertyChanged();
            }
        }

        private string _hostPassword;
        public string HostPassword
        {
            get => _hostPassword;
            set
            {
                _hostPassword = value;
                NotifyPropertyChanged();
            }
        }

        private string _clientId;
        public string ClientId
        {
            get => _clientId;
            set
            {
                _clientId = value;
                NotifyPropertyChanged();
            }
        }

        private string _clientPassword;
        public string ClientPassword
        {
            get => _clientPassword;
            set
            {
                _clientPassword = value;
                NotifyPropertyChanged();
            }
        }

        private string _connectionId;
        public string ConnectionId
        {
            get => _connectionId;
            set
            {
                _connectionId = value;
                NotifyPropertyChanged();
            }
        }

        private string _connectionStatus = "Connection Status: ";
        public string ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                _connectionStatus = value;
                NotifyPropertyChanged();
            }
        }

        private string _connectedHost;
        private string _connectedClient;
        private Timer _timer;
        private Timer _reconnectTimer;
        private bool _connected;
        private bool _reconnecting;
        Timer _mouseTimer = new Timer(500);
        static Random random = new Random();

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string res = "";

            for (int i = 0; i < length; i++)
                res += chars[random.Next(chars.Length)];

            return res;
        }
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            DataContext = this;
            _timer = new System.Timers.Timer();
            _timer.Interval = 400;
            _timer.AutoReset = true;
            _timer.Elapsed += Timer_Elapsed;
            _reconnectTimer = new Timer();
            _reconnectTimer.Interval = 10000;
            _reconnectTimer.AutoReset = true;
            _reconnectTimer.Elapsed += ReconnectTimer_Elapsed;
            _mouseTimer.AutoReset = false;
            Closing += MainWindow_Closing;
        }

        private void ReconnectTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_connected)
                _reconnectTimer.Stop();
            Task.Run(() =>
            {
                Communicator.Instance.RegisterClient(HostId);
            });
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            HostId = $"{RandomString(6)}-{RandomString(3)}-{RandomString(9)}";
            HostPassword = RandomString(8);
            Communicator.Instance.ConnectionLost += Instance_ConnectionLost;
            Communicator.Instance.Reconnecting += Instance_Reconnecting; ;
            Task.Run(() =>
            {
                Thread.Sleep(2500);
                Communicator.Instance.ReadyToConnect(ClientRegistered, TryConnect, AuthenticateSuccess, Produced, StopScreenShare, RequestToResub);
                Communicator.Instance.RegisterClient(HostId);
                Communicator.Instance.ReadyToReceiveInput(MouseMoved, ScreenshotReceived);
            });
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (_window != null)
                _window.Close();
            Thread.Sleep(800);
            if (_connected)
                Communicator.Instance.Disconnect(HostId);
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var bytes = ImageHelper.TakeScreenshot(out System.Drawing.Size reso, 1920, 1080, 65);
            //string imageString = Convert.ToBase64String(bytes);
            Communicator.Instance.ProduceScreenshot(bytes, reso.Width, reso.Height, _connectedClient);
        }

        private void Instance_Reconnecting(bool connected)
        {
            _reconnecting = connected;
            ConnectionStatus = "Connection Status: Reconnecting to server...";
        }

        private void Instance_ConnectionLost(bool connected)
        {
            _connected = connected;
            ConnectionStatus = "Connection Status: Disconnected from server";
            _reconnectTimer.Start();
        }

        public void ClientRegistered(bool connected)
        {
            _connected = connected;
            if (connected)
                ConnectionStatus = "Connection Status: Connected to server";
            else
                ConnectionStatus = "Connection Status: Disconnected from server";
        }

        public void RequestToResub()
        {
            Communicator.Instance.RegisterClient(HostId);
        }

        public void MouseMoved(int x, int y)
        {
            WindowsInputHelper.MouseMove(x, y);
        }

        public void MouseDowned(InputDataComm ss)
        {
            if (ss.DataType == MessageTypeComm.MouseDown && ss.MouseData is MouseClickComm button)
            {
                if (!_mouseTimer.Enabled)
                    _mouseTimer.Start();
                WindowsInputHelper.MouseDown(button.Map());
            }
        }

        private void MouseUpped(InputDataComm ss)
        {
            if (ss.DataType == MessageTypeComm.MouseUp && ss.MouseData is MouseClickComm button)
            {
                if (_mouseTimer.Enabled)
                    WindowsInputHelper.MouseClick(button.Map());

                WindowsInputHelper.MouseUp(button.Map());
            }
        }

        private void MouseDoubleClicked(InputDataComm ss)
        {
            if (ss.DataType == MessageTypeComm.MouseDoubleClick && ss.MouseData is MouseClickComm button)
            {
                WindowsInputHelper.MouseDoubleClick(button.Map());
            }
        }

        private void KeyboardDown(InputDataComm ss)
        {
            if (ss.DataType == MessageTypeComm.KeyboardDown && ss.KeyboardData is KeyboardKeyComm key)
            {
                WindowsInputHelper.KeyDown(key.Map());
            }
        }

        private void KeyboardUp(InputDataComm ss)
        {
            if (ss.DataType == MessageTypeComm.KeyboardUp && ss.KeyboardData is KeyboardKeyComm key)
            {
                WindowsInputHelper.KeyUp(key.Map());
            }
        }

        private void MouseClicked(InputDataComm ss)
        {
            if (ss.DataType == MessageTypeComm.MouseClick && ss.MouseData is MouseClickComm button)
            {
                WindowsInputHelper.MouseClick(button.Map());
            }
        }

        private void TryConnect(string id, string password, string clientId)
        {
            if (id.Equals(HostId) && BCrypt.Net.BCrypt.Verify(HostPassword, password))
            {
                Communicator.Instance.AuthenticateSuccess(clientId, HostId);
                ConnectionId = $"Connected to: {clientId}";
                _connectedClient = clientId;
                _timer.Start();
            }
        }

        ScreenSharingWindow _window;
        private void AuthenticateSuccess(string id)
        {
            ConnectionId = $"Connected to: {id}";
            _connectedHost = id;
            Dispatcher.Invoke(() =>
            {
                ScreenSharingWindow window = new ScreenSharingWindow(_connectedHost);
                _window = window;
                _window.Closing += Window_Closing;
                _window.Show();
            });
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Communicator.Instance.StopScreenShare(_connectedHost);
        }

        private void StopScreenShare()
        {
            _timer.Stop();
        }

        private void Produced(InputDataComm ss)
        {
            switch (ss.DataType)
            {
                case MessageTypeComm.KeyboardDown:
                    KeyboardDown(ss);
                    break;
                case MessageTypeComm.KeyboardUp:
                    KeyboardUp(ss);
                    break;
                case MessageTypeComm.MouseDown:
                    MouseDowned(ss);
                    break;
                case MessageTypeComm.MouseUp:
                    MouseUpped(ss);
                    break;
                case MessageTypeComm.MouseDoubleClick:
                    MouseDoubleClicked(ss);
                    break;
                case MessageTypeComm.MouseClick:
                    MouseClicked(ss);
                    break;
                default:
                    break;
            }
        }

        private void ScreenshotReceived(byte[] data, string width, string height)
        {
            if (!(data != null))
                return;

            //_window.ImageData = imgString;
            Dispatcher.Invoke(() =>
            {
                _window.ImageData = data;
                _window.OriginalWidth = float.Parse(width);
                _window.OriginalHeight = float.Parse(height);
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Communicator.Instance.TryConnect(ClientId, BCrypt.Net.BCrypt.HashPassword(ClientPassword), HostId);
        }
    }
}
