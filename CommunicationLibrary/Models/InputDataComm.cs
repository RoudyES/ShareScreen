using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLibrary.Models
{
    public class InputDataComm
    {
        public MessageTypeComm DataType { get; set; }
        public MouseClickComm MouseData { get; set; }
        public KeyboardKeyComm KeyboardData { get; set; }
    }
}
