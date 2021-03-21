using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace HelpersLibrary.Helpers
{
    public class WindowsInputHelper
    {
        private static InputSimulator simulator = new InputSimulator();

        public static void SwitchToInputDesktop()
        {
            var s = PInvoke.OpenInputDesktop(0, false, PInvoke.ACCESS_MASK.MAXIMUM_ALLOWED);
            bool success = PInvoke.SetThreadDesktop(s);
            PInvoke.CloseDesktop(s);
        }

        public static void MouseMove(int x, int y)
        {
            SwitchToInputDesktop();
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
        }

        public static void MouseClick(MouseClick button)
        {
            SwitchToInputDesktop();
            switch (button)
            {
                case Helpers.MouseClick.Left:
                    simulator.Mouse.LeftButtonClick();
                    break;
                case Helpers.MouseClick.Middle:
                    simulator.Mouse.MiddleButtonClick();
                    break;
                case Helpers.MouseClick.Right:
                    simulator.Mouse.RightButtonClick();
                    break;
            }
        }

        public static void MouseDown(MouseClick button)
        {
            SwitchToInputDesktop();
            switch (button)
            {
                case Helpers.MouseClick.Left:
                    simulator.Mouse.LeftButtonDown();
                    break;
                case Helpers.MouseClick.Middle:
                    simulator.Mouse.MiddleButtonDown();
                    break;
                case Helpers.MouseClick.Right:
                    simulator.Mouse.RightButtonDown();
                    break;
            }
        }

        public static void MouseUp(MouseClick button)
        {
            SwitchToInputDesktop();
            switch (button)
            {
                case Helpers.MouseClick.Left:
                    simulator.Mouse.LeftButtonUp();
                    break;
                case Helpers.MouseClick.Middle:
                    simulator.Mouse.MiddleButtonUp();
                    break;
                case Helpers.MouseClick.Right:
                    simulator.Mouse.RightButtonUp();
                    break;
            }
        }

        public static void MouseDoubleClick(MouseClick button)
        {
            SwitchToInputDesktop();
            switch (button)
            {
                case Helpers.MouseClick.Left:
                    simulator.Mouse.LeftButtonDoubleClick();
                    break;
                case Helpers.MouseClick.Middle:
                    simulator.Mouse.MiddleButtonDoubleClick();
                    break;
                case Helpers.MouseClick.Right:
                    simulator.Mouse.RightButtonDoubleClick();
                    break;
            }
        }

        public static void KeyDown(KeyboardKey key)
        {
            simulator.Keyboard.KeyDown(key.Map());
        }

        public static void KeyPress(KeyboardKey key)
        {
            simulator.Keyboard.KeyPress(key.Map());
        }

        public static void KeyUp(KeyboardKey key)
        {
            simulator.Keyboard.KeyUp(key.Map());
        }
    }
}
