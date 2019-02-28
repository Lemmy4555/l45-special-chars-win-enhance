using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace L45SpecialCharWinEnhance
{
    class WindowWin32Helper
    {
        /*- Retrieves Id of the thread that created the specified window -*/
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, out uint lpdwProcessId);

        /*- Retrieves Handle to the ForeGroundWindow -*/
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();


        [DllImport("user32.dll")]
        static extern IntPtr AttachThreadInput(IntPtr idAttach,
                         IntPtr idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private Window window;

        public WindowWin32Helper(Window window)
        {
            this.window = window;
        }

        public void forceSetForegroundWindow()
        {
            Process currentProcess = Process.GetCurrentProcess();
            int pidc = currentProcess.Id;

            var wih = new WindowInteropHelper(this.window);
            IntPtr hWnd = wih.Handle;

            uint pid;
            uint foregroundThreadID = GetWindowThreadProcessId((int)GetForegroundWindow(), out pid);
            if (foregroundThreadID != pidc)
            {
                AttachThreadInput((IntPtr)pidc, (IntPtr)foregroundThreadID, true);
                SetForegroundWindow(hWnd);
                AttachThreadInput((IntPtr)pidc, (IntPtr)foregroundThreadID, false);
            }
            else
            {
                SetForegroundWindow(hWnd);
            }
        }

    }
}
