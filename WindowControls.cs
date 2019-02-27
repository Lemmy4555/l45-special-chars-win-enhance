using L45.KeyHoldHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L45SpecialCharWinEnhance
{

    public class WindowControls : IDisposable
    {
        private L45KeyHoldHook keyHoldHook;
        public EventHandler WindowHideEvent;

        public WindowControls()
        {
            this.SubscribeEvents();
        }

        public void SubscribeEvents()
        {
            if (this.keyHoldHook != null)
            {
                return;
            }

            this.keyHoldHook = new L45KeyHoldHook();
            this.keyHoldHook.KeyDownEvent += OnKeyDown;
        }

        public void UnsubscribeEvents()
        {

            if (this.keyHoldHook == null)
            {
                return;
            }

            this.keyHoldHook.KeyDownEvent -= OnKeyDown;
            this.keyHoldHook = null;
        }

        public void OnKeyDown(object caller, KeyHoldEventArgs e)
        {
            if (e.KeyInfo.IsWordChar)
            {
                this.WindowHideEvent?.Invoke(this, new EventArgs());
            } else if (e.KeyEventArgs.KeyCode == Keys.Escape)
            {
                this.WindowHideEvent?.Invoke(this, new EventArgs());
            }
        }

        public void Dispose()
        {
            this.UnsubscribeEvents();
        }
    }

}
