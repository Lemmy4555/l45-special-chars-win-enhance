using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L45SpecialCharWinEnhance.L45KeyHoldHook
{
    public partial class KeyHoldHook
    {
        private static KeyHoldHook instance;

        private IKeyboardMouseEvents eventsHook;
        private Dictionary<string, KeyHoldHandler> activeHandlers;
        private bool paused = false;

        public event EventHandler<KeyHoldEventArgs> KeyUpEvent;
        public event EventHandler<KeyHoldEventArgs> KeyDownEvent;
        public event EventHandler<KeyHoldEventArgs> KeyHoldEvent;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        private KeyHoldHook()
        {
            this.eventsHook = Hook.GlobalEvents();
            this.activeHandlers = new Dictionary<string, KeyHoldHandler>();
            this.SubscribeEvents();
        }

        public static KeyHoldHook Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                instance = new KeyHoldHook();
                return instance;
            }
        }

        public bool Paused
        {
            get
            {
                return this.paused;
            }
            set
            {
                this.paused = value;

                foreach (KeyValuePair<string, KeyHoldHandler> entry in this.activeHandlers)
                {
                    entry.Value.Paused = value;
                }
            }
        }

        private void SubscribeEvents()
        {
            eventsHook.KeyDown += OnKeyDown;
        }

        private void UnsubscribeEvents()
        {
            eventsHook.KeyDown -= OnKeyDown;

            foreach (KeyValuePair<string, KeyHoldHandler> entry in this.activeHandlers)
            {
                entry.Value.Dispose();
            }

            this.activeHandlers.Clear();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (this.Paused)
            {
                return;
            }

            string key = e.KeyCode.ToString();

            if (!this.activeHandlers.ContainsKey(key))
            {
                KeyInfo keyInfo = this.GetKeyInfo((KeyEventArgsExt)e);
                KeyHoldHandler newHandler = new KeyHoldHandler(keyInfo, this.eventsHook);

                newHandler.KeyUpEvent += OnKeyUp;
                newHandler.KeyHoldEvent += OnKeyHold;

                activeHandlers.Add(key, newHandler);

                this.KeyDownEvent?.Invoke(this, new KeyHoldEventArgs(keyInfo, (KeyEventArgsExt)e, 0, false));

                if (e.Handled)
                {
                    newHandler.Handled = true;
                }
            }
        }

        private void OnKeyUp(object sender, KeyHoldEventArgs e)
        {
            if (this.Paused)
            {
                return;
            }

            this.KeyUpEvent?.Invoke(this, e);

            activeHandlers.Remove(e.KeyEventArgs.KeyCode.ToString());
        }

        private void OnKeyHold(object sender, KeyHoldEventArgs e)
        {
            if (this.Paused)
            {
                return;
            }

            this.KeyHoldEvent?.Invoke(this, e);

            if (e.Handled)
            {
                string key = e.KeyInfo.KeyCode;
                KeyHoldHandler handler;
                if (this.activeHandlers.TryGetValue(key, out handler))
                {
                    handler.Handled = true;
                }
            }
        }

        private bool isCapsLockOn()
        {
            return (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        }

        private KeyInfo GetKeyInfo(KeyEventArgsExt e)
        {
            string key = KeyboardInputUtils.GetCharsFromKeys(e.KeyData, (uint)e.ScanCode, e.Shift, e.Control, e.Alt, e.IsExtendedKey);
            bool capslock = this.isCapsLockOn();
            bool isAlphaNumeric = KeyboardInputUtils.isAlphaNumeric(key);
            return new KeyInfo(e.KeyCode.ToString(), key, isAlphaNumeric, capslock);
        }

        public void Dispose()
        {
            this.UnsubscribeEvents();
            this.eventsHook.Dispose();
            this.eventsHook = null;
            this.KeyHoldEvent = null;
            this.KeyUpEvent = null;
            this.KeyDownEvent = null;
            instance = null;
        }

        internal void Pause()
        {
            throw new NotImplementedException();
        }

        internal void Resume()
        {
            throw new NotImplementedException();
        }
    }
}
