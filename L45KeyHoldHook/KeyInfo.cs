using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L45SpecialCharWinEnhance.L45KeyHoldHook
{
    public class KeyInfo
    {
        public string KeyCode { get; }
        public string Key { get; }
        public bool IsAlphaNumeric { get; }
        public bool IsCapsLocked { get; }

        public KeyInfo(string keyCode, string key, bool isAlphaNumeric, bool isCapsLocked) {
            KeyCode = keyCode;
            Key = key;
            IsAlphaNumeric = isAlphaNumeric;
            IsCapsLocked = isCapsLocked;
        }
    }
}
