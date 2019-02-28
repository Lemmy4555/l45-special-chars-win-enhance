using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L45.KeyHoldHook
{
    public class KeyInfo
    {
        public string KeyCode { get; }
        public string Key { get; }
        public bool IsAlphaNumeric { get; }
        public bool IsWordChar { get; internal set; }
        public bool IsCapsLocked { get; }

        public KeyInfo(string keyCode, string key, bool isAlphaNumeric, bool isWordChar, bool isCapsLocked) {
            KeyCode = keyCode;
            Key = key;
            IsAlphaNumeric = isAlphaNumeric;
            IsWordChar = isWordChar;
            IsCapsLocked = isCapsLocked;
        }
    }
}
