using L45SpecialCharWinEnhance.L45KeyHoldHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace L45SpecialCharWinEnhance.L45KeyBind
{
    class KeysBindHandler : IDisposable
    {
        public event EventHandler<KeyHoldEventArgs> KeyUpEvent;
        public event EventHandler<KeyBindEventArgs> KeyHoldEvent;

        private KeyHoldHook keyHoldHook;
        private List<string> keysAwaytingToBeSuppressed;

        public KeysBindHandler()
        {
            this.Subscribe(KeyHoldHook.Instance);
            this.keysAwaytingToBeSuppressed = new List<string>();
        }

        private void Subscribe(KeyHoldHook hook)
        {
            this.keyHoldHook = hook;
            hook.KeyDownEvent += OnKeyDown;
            hook.KeyUpEvent += OnKeyUp;
            hook.KeyHoldEvent += OnKeyHold;
        }

        private void Unsubscribe()
        {
            if (this.keyHoldHook == null) return;

            this.keyHoldHook.KeyDownEvent -= OnKeyDown;
            this.keyHoldHook.KeyUpEvent -= OnKeyUp;
            this.keyHoldHook.KeyHoldEvent -= OnKeyHold;

            this.keyHoldHook.Dispose();
            this.keyHoldHook = null;
        }

        private bool GetBind(KeyInfo keyInfo, out string[] bind)
        {
            string key = keyInfo.IsCapsLocked ? keyInfo.Key.ToUpper() : keyInfo.Key;
            if (KeyBindConfig.KeyMapping.TryGetValue(key, out bind))
            {
                return true;
            }

            return false;
        }

        private void OnKeyDown(object sender, KeyHoldEventArgs e)
        {
            if (e.KeyInfo.IsAlphaNumeric)
            {
                e.Handled = true;
            }
        }

        private void OnKeyHold(object sender, KeyHoldEventArgs e)
        {
            string[] bind;
            if (this.GetBind(e.KeyInfo, out bind))
            {
                KeyBindEventArgs eventArgs = new KeyBindEventArgs(e, bind);
                this.KeyHoldEvent?.Invoke(this, eventArgs);
                if (eventArgs.Handled)
                {
                    this.keysAwaytingToBeSuppressed.Add(e.KeyInfo.Key);
                }
            }
        }

        private void OnKeyUp(object sender, KeyHoldEventArgs e)
        {
            int suppressKeyIndex = this.keysAwaytingToBeSuppressed.FindIndex(key => key.Equals(e.KeyInfo.Key));
            if (suppressKeyIndex != -1)
            {
                this.keysAwaytingToBeSuppressed.RemoveAt(suppressKeyIndex);
                return;
            }

            if (e.KeyInfo.IsAlphaNumeric)
            {
                this.keyHoldHook.Paused = true;
                this.KeyUpEvent?.Invoke(this, e);
                this.keyHoldHook.Paused = false;
            }
        }

        public void Dispose()
        {
            this.Unsubscribe();
        }
    }
}
