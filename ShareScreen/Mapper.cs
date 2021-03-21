using CommunicationLibrary.Models;
using HelpersLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShareScreen
{
    public static class Mapper
    {
        //public static MouseClick Map(this MouseButton button)
        //{
        //    Enum.TryParse(button.ToString(), out MouseClick s);
        //    return s;
        //}

        public static MouseClickComm Map(this MouseButton button)
        {
            Enum.TryParse(button.ToString(), out MouseClickComm s);
            return s;
        }

        public static KeyboardKeyComm Map(this KeyboardKey key)
        {
            Enum.TryParse(key.ToString(), out KeyboardKeyComm s);
            return s;
        }

        public static KeyboardKey Map(this KeyboardKeyComm key)
        {
            Enum.TryParse(key.ToString(), out KeyboardKey s);
            return s;
        }

        public static KeyboardKeyComm Map(this Key key)
        {
            switch (key)
            {
                case Key.None:
                    return KeyboardKeyComm.NONAME;
                case Key.Cancel:
                    return KeyboardKeyComm.CANCEL;
                case Key.Back:
                    return KeyboardKeyComm.BACK;
                case Key.Tab:
                    return KeyboardKeyComm.TAB;
                case Key.Clear:
                    return KeyboardKeyComm.CLEAR;
                case Key.Enter:
                    return KeyboardKeyComm.RETURN;
                case Key.Pause:
                    return KeyboardKeyComm.PAUSE;
                case Key.CapsLock:
                    return KeyboardKeyComm.CAPITAL;
                case Key.HangulMode:
                    return KeyboardKeyComm.HANGUL;
                case Key.JunjaMode:
                    return KeyboardKeyComm.JUNJA;
                case Key.FinalMode:
                    return KeyboardKeyComm.FINAL;
                case Key.KanjiMode:
                    return KeyboardKeyComm.KANJI;
                case Key.Escape:
                    return KeyboardKeyComm.ESCAPE;
                case Key.ImeConvert:
                    return KeyboardKeyComm.CONVERT;
                case Key.ImeNonConvert:
                    return KeyboardKeyComm.NONCONVERT;
                case Key.ImeAccept:
                    return KeyboardKeyComm.ACCEPT;
                case Key.ImeModeChange:
                    return KeyboardKeyComm.MODECHANGE;
                case Key.Space:
                    return KeyboardKeyComm.SPACE;
                case Key.PageUp:
                    return KeyboardKeyComm.PRIOR;
                case Key.PageDown:
                    return KeyboardKeyComm.NEXT;
                case Key.End:
                    return KeyboardKeyComm.END;
                case Key.Home:
                    return KeyboardKeyComm.HOME;
                case Key.Left:
                    return KeyboardKeyComm.LEFT;
                case Key.Up:
                    return KeyboardKeyComm.UP;
                case Key.Right:
                    return KeyboardKeyComm.RIGHT;
                case Key.Down:
                    return KeyboardKeyComm.DOWN;
                case Key.Select:
                    return KeyboardKeyComm.SELECT;
                case Key.Print:
                    return KeyboardKeyComm.PRINT;
                case Key.Execute:
                    return KeyboardKeyComm.EXECUTE;
                case Key.PrintScreen:
                    return KeyboardKeyComm.SNAPSHOT;
                case Key.Insert:
                    return KeyboardKeyComm.INSERT;
                case Key.Delete:
                    return KeyboardKeyComm.DELETE;
                case Key.Help:
                    return KeyboardKeyComm.HELP;
                case Key.D0:
                    return KeyboardKeyComm.VK_0;
                case Key.D1:
                    return KeyboardKeyComm.VK_1;
                case Key.D2:
                    return KeyboardKeyComm.VK_2;
                case Key.D3:
                    return KeyboardKeyComm.VK_3;
                case Key.D4:
                    return KeyboardKeyComm.VK_4;
                case Key.D5:
                    return KeyboardKeyComm.VK_5;
                case Key.D6:
                    return KeyboardKeyComm.VK_6;
                case Key.D7:
                    return KeyboardKeyComm.VK_7;
                case Key.D8:
                    return KeyboardKeyComm.VK_8;
                case Key.D9:
                    return KeyboardKeyComm.VK_9;
                case Key.A:
                    return KeyboardKeyComm.VK_A;
                case Key.B:
                    return KeyboardKeyComm.VK_B;
                case Key.C:
                    return KeyboardKeyComm.VK_C;
                case Key.D:
                    return KeyboardKeyComm.VK_D;;
                case Key.E:
                    return KeyboardKeyComm.VK_E;
                case Key.F:
                    return KeyboardKeyComm.VK_F;
                case Key.G:
                    return KeyboardKeyComm.VK_G;
                case Key.H:
                    return KeyboardKeyComm.VK_H;
                case Key.I:
                    return KeyboardKeyComm.VK_I;
                case Key.J:
                    return KeyboardKeyComm.VK_J;
                case Key.K:
                    return KeyboardKeyComm.VK_K;
                case Key.L:
                    return KeyboardKeyComm.VK_L;
                case Key.M:
                    return KeyboardKeyComm.VK_M;
                case Key.N:
                    return KeyboardKeyComm.VK_N;
                case Key.O:
                    return KeyboardKeyComm.VK_O;
                case Key.P:
                    return KeyboardKeyComm.VK_P;
                case Key.Q:
                    return KeyboardKeyComm.VK_Q;
                case Key.R:
                    return KeyboardKeyComm.VK_R;
                case Key.S:
                    return KeyboardKeyComm.VK_S;
                case Key.T:
                    return KeyboardKeyComm.VK_T;
                case Key.U:
                    return KeyboardKeyComm.VK_U;
                case Key.V:
                    return KeyboardKeyComm.VK_V;
                case Key.W:
                    return KeyboardKeyComm.VK_W;
                case Key.X:
                    return KeyboardKeyComm.VK_X;
                case Key.Y:
                    return KeyboardKeyComm.VK_Y;
                case Key.Z:
                    return KeyboardKeyComm.VK_Z;
                case Key.LWin:
                    return KeyboardKeyComm.LWIN;
                case Key.RWin:
                    return KeyboardKeyComm.RWIN;
                case Key.Apps:
                    return KeyboardKeyComm.APPS;
                case Key.Sleep:
                    return KeyboardKeyComm.SLEEP;
                case Key.NumPad0:
                    return KeyboardKeyComm.NUMPAD0;
                case Key.NumPad1:
                    return KeyboardKeyComm.NUMPAD1;
                case Key.NumPad2:
                    return KeyboardKeyComm.NUMPAD2;
                case Key.NumPad3:
                    return KeyboardKeyComm.NUMPAD3;
                case Key.NumPad4:
                    return KeyboardKeyComm.NUMPAD4;
                case Key.NumPad5:
                    return KeyboardKeyComm.NUMPAD5;
                case Key.NumPad6:
                    return KeyboardKeyComm.NUMPAD6;
                case Key.NumPad7:
                    return KeyboardKeyComm.NUMPAD7;
                case Key.NumPad8:
                    return KeyboardKeyComm.NUMPAD8;
                case Key.NumPad9:
                    return KeyboardKeyComm.NUMPAD9;
                case Key.Multiply:
                    return KeyboardKeyComm.MULTIPLY;
                case Key.Add:
                    return KeyboardKeyComm.ADD;
                case Key.Separator:
                    return KeyboardKeyComm.SEPARATOR;
                case Key.Subtract:
                    return KeyboardKeyComm.SUBTRACT;
                case Key.Decimal:
                    return KeyboardKeyComm.DECIMAL;
                case Key.Divide:
                    return KeyboardKeyComm.DIVIDE;
                case Key.F1:
                    return KeyboardKeyComm.F1;
                case Key.F2:
                    return KeyboardKeyComm.F2;
                case Key.F3:
                    return KeyboardKeyComm.F3;
                case Key.F4:
                    return KeyboardKeyComm.F4;
                case Key.F5:
                    return KeyboardKeyComm.F5;
                case Key.F6:
                    return KeyboardKeyComm.F6;
                case Key.F7:
                    return KeyboardKeyComm.F7;
                case Key.F8:
                    return KeyboardKeyComm.F8;
                case Key.F9:
                    return KeyboardKeyComm.F9;
                case Key.F10:
                    return KeyboardKeyComm.F10;
                case Key.F11:
                    return KeyboardKeyComm.F11;
                case Key.F12:
                    return KeyboardKeyComm.F12;
                case Key.F13:
                    return KeyboardKeyComm.F13;
                case Key.F14:
                    return KeyboardKeyComm.F14;
                case Key.F15:
                    return KeyboardKeyComm.F15;
                case Key.F16:
                    return KeyboardKeyComm.F16;
                case Key.F17:
                    return KeyboardKeyComm.F17;
                case Key.F18:
                    return KeyboardKeyComm.F18;
                case Key.F19:
                    return KeyboardKeyComm.F19;
                case Key.F20:
                    return KeyboardKeyComm.F20;
                case Key.F21:
                    return KeyboardKeyComm.F21;
                case Key.F22:
                    return KeyboardKeyComm.F22;
                case Key.F23:
                    return KeyboardKeyComm.F23;
                case Key.F24:
                    return KeyboardKeyComm.F24;
                case Key.NumLock:
                    return KeyboardKeyComm.NUMLOCK;
                case Key.Scroll:
                    return KeyboardKeyComm.SCROLL;
                case Key.LeftShift:
                    return KeyboardKeyComm.LSHIFT;
                case Key.RightShift:
                    return KeyboardKeyComm.RSHIFT;
                case Key.LeftCtrl:
                    return KeyboardKeyComm.LCONTROL;
                case Key.RightCtrl:
                    return KeyboardKeyComm.RCONTROL;
                case Key.LeftAlt:
                    return KeyboardKeyComm.LMENU;
                case Key.RightAlt:
                    return KeyboardKeyComm.RMENU;
                case Key.BrowserBack:
                    return KeyboardKeyComm.BROWSER_BACK;
                case Key.BrowserForward:
                    return KeyboardKeyComm.BROWSER_FORWARD;
                case Key.BrowserRefresh:
                    return KeyboardKeyComm.BROWSER_REFRESH;
                case Key.BrowserStop:
                    return KeyboardKeyComm.BROWSER_STOP;
                case Key.BrowserSearch:
                    return KeyboardKeyComm.BROWSER_SEARCH;
                case Key.BrowserFavorites:
                    return KeyboardKeyComm.BROWSER_FAVORITES;
                case Key.BrowserHome:
                    return KeyboardKeyComm.BROWSER_HOME;
                case Key.VolumeMute:
                    return KeyboardKeyComm.VOLUME_MUTE;
                case Key.VolumeDown:
                    return KeyboardKeyComm.VOLUME_DOWN;
                case Key.VolumeUp:
                    return KeyboardKeyComm.VOLUME_UP;
                case Key.MediaNextTrack:
                    return KeyboardKeyComm.MEDIA_NEXT_TRACK;
                case Key.MediaPreviousTrack:
                    return KeyboardKeyComm.MEDIA_PREV_TRACK;
                case Key.MediaStop:
                    return KeyboardKeyComm.MEDIA_STOP;
                case Key.MediaPlayPause:
                    return KeyboardKeyComm.MEDIA_PLAY_PAUSE;
                case Key.LaunchMail:
                    return KeyboardKeyComm.LAUNCH_MAIL;
                case Key.SelectMedia:
                    return KeyboardKeyComm.LAUNCH_MEDIA_SELECT;
                case Key.LaunchApplication1:
                    return KeyboardKeyComm.LAUNCH_APP1;
                case Key.LaunchApplication2:
                    return KeyboardKeyComm.LAUNCH_APP2;
                case Key.OemSemicolon:
                    return KeyboardKeyComm.OEM_1;
                case Key.OemPlus:
                    return KeyboardKeyComm.OEM_PLUS;
                case Key.OemComma:
                    return KeyboardKeyComm.OEM_COMMA;
                case Key.OemMinus:
                    return KeyboardKeyComm.OEM_MINUS;
                case Key.OemPeriod:
                    return KeyboardKeyComm.OEM_PERIOD;
                case Key.OemQuestion:
                    return KeyboardKeyComm.OEM_2;
                case Key.OemTilde:
                    return KeyboardKeyComm.OEM_3;
                case Key.AbntC1:
                    break;
                case Key.AbntC2:
                    break;
                case Key.OemOpenBrackets:
                    return KeyboardKeyComm.OEM_4;
                case Key.OemPipe:
                    return KeyboardKeyComm.OEM_5;
                case Key.OemCloseBrackets:
                    return KeyboardKeyComm.OEM_6;
                case Key.OemQuotes:
                    return KeyboardKeyComm.OEM_7;
                case Key.Oem8:
                    return KeyboardKeyComm.OEM_8;
                case Key.OemBackslash:
                    return KeyboardKeyComm.OEM_102;
                case Key.ImeProcessed:
                    return KeyboardKeyComm.PROCESSKEY;
                case Key.System:
                    return KeyboardKeyComm.LWIN;
                case Key.OemAttn:
                    break;
                case Key.OemFinish:
                    break;
                case Key.OemCopy:
                    break;
                case Key.OemAuto:
                    break;
                case Key.OemEnlw:
                    break;
                case Key.OemBackTab:
                    break;
                case Key.Attn:
                    break;
                case Key.CrSel:
                    break;
                case Key.ExSel:
                    break;
                case Key.EraseEof:
                    break;
                case Key.Play:
                    return KeyboardKeyComm.PLAY;
                case Key.Zoom:
                    return KeyboardKeyComm.ZOOM;
                case Key.NoName:
                    return KeyboardKeyComm.NONAME;
                case Key.Pa1:
                    return KeyboardKeyComm.PA1;
                case Key.OemClear:
                    return KeyboardKeyComm.OEM_CLEAR;
                default:
                    return KeyboardKeyComm.NONAME;
            }
            return KeyboardKeyComm.NONAME;
        }

        public static MouseClickComm Map(this MouseClick click)
        {
            Enum.TryParse(click.ToString(), out MouseClickComm comm);
            return comm;
        }

        public static MouseClick Map(this MouseClickComm click)
        {
            Enum.TryParse(click.ToString(), out MouseClick comm);
            return comm;
        }
    }
}
