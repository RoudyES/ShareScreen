using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace HelpersLibrary.Helpers
{
    public static class Mapper
    {
        public static VirtualKeyCode Map(this KeyboardKey key)
        {
            Enum.TryParse(key.ToString(), out VirtualKeyCode k);
            return k;
        }
    }
}
