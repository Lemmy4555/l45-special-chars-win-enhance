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
        public EventHandler WindowHideEvent;

        private MainWindow window;
        private L45KeyHoldHook keyHoldHook;

        public WindowControls(MainWindow window)
        {
            this.window = window;
            this.SubscribeEvents();
        }

        public WindowControls(MainWindow window, L45KeyHoldHook keyHoldHook)
        {
            this.window = window;
            this.keyHoldHook = keyHoldHook;
            this.SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            if (this.keyHoldHook == null)
            {
                this.keyHoldHook = new L45KeyHoldHook();
            }

            this.keyHoldHook.KeyDownEvent += OnKeyDown;
        }

        private void UnsubscribeEvents()
        {
            if (this.keyHoldHook != null)
            {
                this.keyHoldHook.KeyDownEvent -= OnKeyDown;
                this.keyHoldHook = null;
            }
        }

        public void OnKeyDown(object caller, KeyHoldEventArgs e)
        {
            if (this.window.IsActive)
            {
                if (e.KeyInfo.IsWordChar || e.KeyEventArgs.KeyCode == Keys.Back)
                {
                    this.WindowHideEvent?.Invoke(this, new EventArgs());
                    this.window.SendKey(e.KeyInfo.Key);
                    e.Handled = true;
                }
                else if (e.KeyEventArgs.KeyCode == Keys.Escape)
                {
                    this.WindowHideEvent?.Invoke(this, new EventArgs());
                    e.Handled = true;
                }
            }
        }

        public void Dispose()
        {
            this.UnsubscribeEvents();
        }
    }

}
