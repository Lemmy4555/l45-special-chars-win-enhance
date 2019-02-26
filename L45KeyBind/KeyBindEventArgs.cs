using L45SpecialCharWinEnhance.L45KeyHoldHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L45SpecialCharWinEnhance.L45KeyBind
{
    public class KeyBindEventArgs
    {
        public KeyHoldEventArgs KeyHoldEvent { get; }
        public string[] Values { get; }

        private bool handled;

        public KeyBindEventArgs(KeyHoldEventArgs keyHoldEvent, string[] values)
        {
            KeyHoldEvent = keyHoldEvent;
            Values = values;
            handled = false;
        }

        public bool Handled
        {
            get
            {
                return this.handled;
            }
            set
            {
                this.handled = value;
                this.KeyHoldEvent.Handled = value;
            }
        }
    }
}
