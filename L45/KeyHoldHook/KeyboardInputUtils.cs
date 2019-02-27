using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L45.KeyHoldHook
{
    class KeyboardInputUtils
    {
        [DllImport("user32.dll")]
        public static extern int ToUnicode(uint virtualKeyCode, uint scanCode,
        byte[] keyboardState,
        [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
        StringBuilder receivingBuffer,
        int bufferSize, uint flags);

        public static string GetCharsFromKeys(Keys keys, uint scanCode, bool shift, bool ctrl, bool alt, bool isExtended)
        {
            var buf = new StringBuilder(256);
            var keyboardState = new byte[256];
            if (shift)
                keyboardState[(int)Keys.ShiftKey] = 0xff;
            if (ctrl)
            {
                keyboardState[(int)Keys.ControlKey] = 0xff;
            }
            if (alt)
            {
                keyboardState[(int)Keys.Menu] = 0xff;
            }
            ToUnicode((uint)keys, scanCode, keyboardState, buf, 256, (uint)(isExtended ? 1 : 0));
            return buf.ToString();
        }

        public static Boolean IsAlphaNumeric(string strToCheck)
        {
            if (String.IsNullOrEmpty(strToCheck))
            {
                return false;
            }

            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
        }

        internal static bool IsWordChar(string strToCheck)
        {
            if (String.IsNullOrEmpty(strToCheck))
            {
                return false;
            }

            Regex rg = new Regex(@"^[\w,\.\-]*$");
            return rg.IsMatch(strToCheck);
        }
    }
}
