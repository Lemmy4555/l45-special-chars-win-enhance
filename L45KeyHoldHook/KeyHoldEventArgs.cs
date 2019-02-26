using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L45SpecialCharWinEnhance.L45KeyHoldHook
{
    public class KeyHoldEventArgs : EventArgs
    {
        public KeyInfo KeyInfo { get; }
        public KeyEventArgsExt KeyEventArgs { get; }
        public long TimeHoldedKey { get; }
        public bool HasBeenHolded { get; }
        private bool handled = false;

        public KeyHoldEventArgs(KeyInfo keyInfo, KeyEventArgsExt keyEventArgs, int timeKeyHolded, bool hasKeyBeenHolded)
        {
            KeyInfo = keyInfo;
            this.KeyEventArgs = keyEventArgs;
            this.TimeHoldedKey = timeKeyHolded;
            this.HasBeenHolded = hasKeyBeenHolded;
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
                this.KeyEventArgs.Handled = value;
            }
        }

    }
}
