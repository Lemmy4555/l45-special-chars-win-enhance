using L45.KeyHoldHook;
using L45.SpecialCharWinEnhance.KeyBind;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L45.SpecialCharWinEnhance.Implementation
{
    class BindedKeyHoldManager : IDisposable
    {
        private KeyBindHandler bindHandler;
        private L45KeyHoldHook keyHoldHook;

        public event EventHandler<KeyBindEventArgs> BindedKeyHoldEvent;

        public BindedKeyHoldManager()
        {
            this.SubscribeKeyBindEvents();
        }

        private void SubscribeKeyBindEvents()
        {
            if (keyHoldHook != null)
            {
                return;
            }

            this.keyHoldHook = new L45KeyHoldHook();
            this.keyHoldHook.KeyHoldEvent += OnKeyHold;

            if (bindHandler != null)
            {
                return;
            }

            this.bindHandler = new KeyBindHandler();
            this.bindHandler.KeyHoldEvent += OnBindedKeyHold;
        }

        private void UnsubscribeKeyBindEvents()
        {
            if (this.bindHandler == null)
            {
                return;
            }

            this.bindHandler.KeyHoldEvent -= OnBindedKeyHold;
            this.bindHandler.Dispose();
            this.bindHandler = null;

            if (this.keyHoldHook == null)
            {
                return;
            }

            this.keyHoldHook.KeyHoldEvent -= OnKeyHold;
            this.keyHoldHook.Dispose();
            this.keyHoldHook = null;
        }

        private void OnBindedKeyHold(object sender, KeyBindEventArgs e)
        {
            Debug.WriteLine("Key Hold event on {0} ({1}) after {2}ms \t values: {3}", e.KeyHoldEvent.KeyInfo.Key, e.KeyHoldEvent.KeyInfo.KeyCode, e.KeyHoldEvent.TimeHoldedKey, String.Join(", ", e.Binding.Value));
            this.BindedKeyHoldEvent?.Invoke(this, e);
            e.Handled = true;
        }

        private void OnKeyHold(object sender, KeyHoldEventArgs e)
        {
            if (e.KeyInfo.IsAlphaNumeric)
            {
                e.Handled = true;
            }
        }

        public void Dispose()
        {
            this.UnsubscribeKeyBindEvents();
        }
    }
}
