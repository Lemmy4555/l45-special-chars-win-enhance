using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace L45.CaretPosition
{
    class GlobalCaretPosition
    {
        [StructLayout(LayoutKind.Sequential)]    // Required by user32.dll
        public struct GUITHREADINFO
        {
            public uint cbSize;
            public uint flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public RECT rcCaret;
        };

        /*- Retrieves information about active window or any specific GUI thread -*/
        [DllImport("user32.dll", EntryPoint = "GetGUIThreadInfo")]
        public static extern bool GetGUIThreadInfo(uint tId, out GUITHREADINFO threadInfo);

        [StructLayout(LayoutKind.Sequential)]    // Required by user32.dll
        public struct RECT
        {
            public uint Left;
            public uint Top;
            public uint Right;
            public uint Bottom;
        };

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        /*- Retrieves Handle to the ForeGroundWindow -*/
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCaretPos(out Point lpPoint);

        /*- Converts window specific point to screen specific -*/
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, out Point position);

        /*- Retrieves Id of the thread that created the specified window -*/
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, out uint lpdwProcessId);

        public static Point GetCurrentCaretPosition()
        {
            uint pid;
            GetWindowThreadProcessId((int)GetForegroundWindow(), out pid);
            Process p = Process.GetProcessById((int)pid);
            // Display current active Process on Tooltip
            string text = p.ProcessName;

            Point caretPosition;

            caretPosition = GetCaretPositionByGUIInfo();
            if (caretPosition.X == 0 && caretPosition.Y == 0)
            {
                caretPosition = GetCaretPositionByGUIInfo();
            }

            if (caretPosition.X == 0 && caretPosition.Y == 0)
            {
                caretPosition = GetCenterOfForegorundWindow();
            }

            if (caretPosition.X == 0 && caretPosition.Y == 0)
            {
                caretPosition = GetCenterOfForegorundWindow();

                Screen screen = GetActiveScreen();
                if ((caretPosition.X < screen.WorkingArea.Left) || (caretPosition.Y < screen.WorkingArea.Top) || (caretPosition.X > screen.WorkingArea.Right) || (caretPosition.Y > screen.WorkingArea.Bottom))
                {
                    caretPosition = GetCenterOfActiveScreen();
                }
            }

            return caretPosition;
        }

        public static Point GetCaretPositionByGUIInfo()
        {
            Point caretPosition = new Point();

            GUITHREADINFO guiInfo = GetThreadInfo();

            caretPosition.X = (int)guiInfo.rcCaret.Left;
            caretPosition.Y = (int)guiInfo.rcCaret.Bottom;

            ClientToScreen(guiInfo.hwndCaret, out caretPosition);

            return caretPosition;
        }

        public static Point GetCaretPositionByGetCaretPos()
        {
            Point caretPosition = new Point();

            GUITHREADINFO guiInfo = GetThreadInfo();

            RECT activeWindowPosition;
            GetWindowRect(GetForegroundWindow(), out activeWindowPosition);
            GetCaretPos(out caretPosition);

            ClientToScreen(guiInfo.hwndCaret, out caretPosition);

            return caretPosition;
        }

        public static Point GetCenterOfForegorundWindow()
        {
            Point caretPosition = new Point();
            GUITHREADINFO guiInfo = GetThreadInfo();

            RECT activeWindowPosition;
            GetWindowRect(GetForegroundWindow(), out activeWindowPosition);
            int top = (int) activeWindowPosition.Top;
            int bottom = (int) activeWindowPosition.Bottom;
            int left = (int) activeWindowPosition.Left;
            int right = (int) activeWindowPosition.Right;
            caretPosition.X = right - ((right - left) / 2);
            caretPosition.Y = bottom - ((bottom - top) / 2);

            ClientToScreen(guiInfo.hwndCaret, out caretPosition);
            return caretPosition;
        }

        public static Point GetCenterOfActiveScreen()
        {
            Point caretPosition = new Point();
            GUITHREADINFO guiInfo = GetThreadInfo();

            Screen screen = GetActiveScreen();

            int top = (int)screen.WorkingArea.Top;
            int bottom = (int)screen.WorkingArea.Bottom;
            int left = (int)screen.WorkingArea.Left;
            int right = (int)screen.WorkingArea.Right;
            caretPosition.X = right - ((right - left) / 2);
            caretPosition.Y = bottom - ((bottom - top) / 2);

            ClientToScreen(guiInfo.hwndCaret, out caretPosition);
            return caretPosition;
        }

        private static GUITHREADINFO GetThreadInfo()
        {
            GUITHREADINFO guiInfo = new GUITHREADINFO();
            guiInfo.cbSize = (uint)Marshal.SizeOf(guiInfo);

            // Get GuiThreadInfo into guiInfo
            GetGUIThreadInfo(0, out guiInfo);

            return guiInfo;
        }

        public static System.Windows.Forms.Screen GetActiveScreen()
        {
            return System.Windows.Forms.Screen.FromHandle(GetForegroundWindow());
        }
    }
}
