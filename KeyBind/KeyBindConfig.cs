using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L45.SpecialCharWinEnhance.KeyBind
{
    class KeyBindConfig
    {
        public static Dictionary<string, string[]> KeyMapping = new Dictionary<string, string[]>();

        static KeyBindConfig()
        {
            KeyMapping.Add("E", new string[] { "\u00c8",  "\u00c9", "\u20ac"}); // È, É, €
            KeyMapping.Add("e", new string[] { "\u00e8", "\u00e9", "\u20ac" }); // è, é, €
            KeyMapping.Add("A", new string[] { "\u00c0", "\u00c1", "\u0251", "\u0040" }); // À, Á, ɑ, @
            KeyMapping.Add("a", new string[] { "\u00e0", "\u00e1", "\u0116", "\u20ac" }); // à, á, ɑ, @
            KeyMapping.Add("I", new string[] { "\u00cc", "\u00cd" }); // Ì, Í
            KeyMapping.Add("i", new string[] { "\u00ec", "\u00ed" }); // ì, í
            KeyMapping.Add("O", new string[] { "\u00d2", "\u00d3" }); // Ò, Ó
            KeyMapping.Add("o", new string[] { "\u00f2", "\u00f3" }); // ò, ó
            KeyMapping.Add("U", new string[] { "\u00d9", "\u00da" }); // Ú, Ú
            KeyMapping.Add("u", new string[] { "\u00f8", "\u00f9", "\u03bc" }); // ù, ù, μ
            KeyMapping.Add("b", new string[] { "\u03B2" }); // β
            KeyMapping.Add(".", new string[] { "\u007e" }); // ~
            KeyMapping.Add("'", new string[] { "\u0060", "\u00b4" }); // `, ´
            KeyMapping.Add("\"", new string[] { "\u0060", "\u00b4" }); // `, ´
            KeyMapping.Add("R", new string[] { "\u00ae" }); // ®
            KeyMapping.Add("C", new string[] { "\u00a9" }); // ©
            KeyMapping.Add("P", new string[] { "\u2117" }); // ℗
            KeyMapping.Add("T", new string[] { "\u2122" }); // ™
            KeyMapping.Add("(", new string[] { "\u007b" }); // {
            KeyMapping.Add("[", new string[] { "\u007b" }); // {
            KeyMapping.Add(")", new string[] { "\u007d" }); // }
            KeyMapping.Add("]", new string[] { "\u007d" }); // }
        }
    }
}
