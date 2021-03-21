using CommunicationLibrary.Communication;
using CommunicationLibrary.Models;
using HelpersLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShareScreen
{
    /// <summary>
    /// Interaction logic for ScreenSharingWindow.xaml
    /// </summary>
    public partial class ScreenSharingWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Timer _mouseTimer = new Timer(500);
        Timer _keyboardTimer = new Timer(700);
        public BitmapImage ToImage(byte[] array)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad; // here
            image.StreamSource = new System.IO.MemoryStream(array);
            image.EndInit();
            return image;
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                NotifyPropertyChanged();
            }
        }

        private byte[] _imageData;
        public byte[] ImageData
        {
            get => _imageData;
            set
            {
                _imageData = value;
                NotifyPropertyChanged();

                Dispatcher.Invoke(() => Image = ToImage(ImageData));
            }
        }

        public float OriginalWidth { get; set; }
        public float OriginalHeight { get; set; }

        string _host;
        bool _mouseUp;
        public ScreenSharingWindow(string host)
        {
            InitializeComponent();
            DataContext = this;
            _mouseTimer.AutoReset = false;
            _keyboardTimer.AutoReset = false;
            _host = host;
        }

        private void ImageHolder_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point p = new System.Drawing.Point((int)(e.GetPosition(ss).X * (OriginalWidth / ss.ActualWidth)), (int)(e.GetPosition(ss).Y * (OriginalHeight / ss.ActualHeight)));
            Communicator.Instance.ProduceMouseMove(p.X, p.Y, _host);
        }

        private void ImageHolder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseUp = false;
            if (e.ClickCount == 2)
            {
                Communicator.Instance.Produce(
                new InputDataComm()
                {
                    DataType = MessageTypeComm.MouseDoubleClick,
                    MouseData = e.ChangedButton.Map()
                }, _host);
                return;
            }

            Task.Run(() =>
            {
                System.Threading.Thread.Sleep(130);
                if (_mouseUp)
                {
                    Communicator.Instance.Produce(
                        new InputDataComm()
                        {
                            DataType = MessageTypeComm.MouseClick,
                            MouseData = e.ChangedButton.Map()
                        }, _host);
                }
                else
                {
                    Communicator.Instance.Produce(
                        new InputDataComm()
                        {
                            DataType = MessageTypeComm.MouseDown,
                            MouseData = e.ChangedButton.Map()
                        }, _host);
                }
            });
        }

        private void ImageHolder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _mouseUp = true;

            Communicator.Instance.Produce(
                new InputDataComm()
                {
                    DataType = MessageTypeComm.MouseUp,
                    MouseData = e.ChangedButton.Map()
                }, _host);
            //WindowsInputHelper.MouseUp(e.ChangedButton.Map());
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Focus();
            Keyboard.Focus(this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_keyboardTimer.Enabled)
                _keyboardTimer.Start();

            Communicator.Instance.Produce(
                new InputDataComm()
                {
                    DataType = MessageTypeComm.KeyboardDown,
                    KeyboardData = e.Key.Map()
                }, _host);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //if (_mouseTimer.Enabled)
            //    WindowsInputHelper.KeyPress(e.Key.Map());

            Communicator.Instance.Produce(
                new InputDataComm()
                {
                    DataType = MessageTypeComm.KeyboardUp,
                    KeyboardData = e.Key.Map()
                }, _host);
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
