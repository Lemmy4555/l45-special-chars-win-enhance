using L45.KeyHoldHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace L45.SpecialCharWinEnhance.KeyBind
{
    class KeyBindHandler : IDisposable
    {
        public event EventHandler<KeyBindEventArgs> KeyUpEvent;
        public event EventHandler<KeyBindEventArgs> KeyHoldEvent;
        public event EventHandler<KeyBindEventArgs> KeyDownEvent;

        private L45KeyHoldHook keyHoldHook;
        private List<string> keysAwaytingToBeSuppressed;
        private List<string> keysAwaytingToBeHolded;

        public KeyBindHandler()
        {
            this.Subscribe();
            this.keysAwaytingToBeSuppressed = new List<string>();
            this.keysAwaytingToBeHolded = new List<string>();
        }

        private void Subscribe()
        {
            if (this.keyHoldHook != null)
            {
                return;
            }

            this.keyHoldHook = new L45KeyHoldHook();
            this.keyHoldHook.KeyDownEvent += OnKeyDown;
            this.keyHoldHook.KeyUpEvent += OnKeyUp;
            this.keyHoldHook.KeyHoldEvent += OnKeyHold;
        }

        private void Unsubscribe()
        {
            if (this.keyHoldHook == null)
            {
                return;
            };

            this.keyHoldHook.KeyDownEvent -= OnKeyDown;
            this.keyHoldHook.KeyUpEvent -= OnKeyUp;
            this.keyHoldHook.KeyHoldEvent -= OnKeyHold;

            this.keyHoldHook.Dispose();
            this.keyHoldHook = null;
        }

        private bool GetBind(KeyInfo keyInfo, out KeyValuePair<string, string[]> binding)
        {
            string key = keyInfo.IsCapsLocked ? keyInfo.Key.ToUpper() : keyInfo.Key;
            string[] values;
            if (KeyBindConfig.KeyMapping.TryGetValue(key, out values))
            {
                binding = new KeyValuePair<string, string[]>(key, values);
                return true;
            }

            binding = new KeyValuePair<string, string[]>(null, null);

            return false;
        }

        private void OnKeyDown(object sender, KeyHoldEventArgs e)
        {
            KeyValuePair<string, string[]> binding;
            if (this.GetBind(e.KeyInfo, out binding))
            {
                KeyBindEventArgs eventArgs = new KeyBindEventArgs(e, binding, onHookPauseRequest, onHookResumeRequest);
                this.KeyDownEvent?.Invoke(this, eventArgs);
            }
        }

        private void OnKeyHold(object sender, KeyHoldEventArgs e)
        {
            KeyValuePair<string, string[]> binding;
            if (this.GetBind(e.KeyInfo, out binding))
            {
                KeyBindEventArgs eventArgs = new KeyBindEventArgs(e, binding, onHookPauseRequest, onHookResumeRequest);
                this.KeyHoldEvent?.Invoke(this, eventArgs);
            }
        }

        private void OnKeyUp(object sender, KeyHoldEventArgs e)
        {
            KeyValuePair<string, string[]> binding;
            if (this.GetBind(e.KeyInfo, out binding))
            {
                KeyBindEventArgs eventArgs = new KeyBindEventArgs(e, binding, onHookPauseRequest, onHookResumeRequest);
                this.KeyUpEvent?.Invoke(this, eventArgs);
            }
        }

        private void onHookPauseRequest()
        {
            this.keyHoldHook.Paused = true;
        }

        private void onHookResumeRequest()
        {
            this.keyHoldHook.Paused = false;
        }

        public void Dispose()
        {
            this.Unsubscribe();
        }
    }
}
