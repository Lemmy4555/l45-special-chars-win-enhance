using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L45SpecialCharWinEnhance.L45KeyBind
{
    class KeyBindConfig
    {
        public static Dictionary<string, string[]> KeyMapping = new Dictionary<string, string[]>();

        static KeyBindConfig()
        {
            KeyMapping.Add("E", new string[] { "\u00c8",  "\u00c9", "\u00ca", "\u00cb", "\u0112", "\u0114", "\u0116", "\u20ac"}); // È, É, Ê, Ë, Ē, Ĕ, Ė, €
        }
    }
}
