using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L45.KeyHoldHook
{

    public partial class L45KeyHoldHook
    {
        private class KeyHoldHandler
        {
            public event EventHandler<KeyHoldEventArgs> KeyUpEvent;
            public event EventHandler<KeyHoldEventArgs> KeyHoldEvent;
            public bool Handled { get; set; } = false;
            public bool IsPaused { get; private set; }

            private long handlerStart;
            private readonly KeyInfo keyInfo;
            private IKeyboardMouseEvents eventsHook;
            private bool hasKeyBeenHolded = false;

            public KeyHoldHandler(KeyInfo keyInfo, IKeyboardMouseEvents eventsHook)
            {
                this.keyInfo = keyInfo;
                this.eventsHook = eventsHook;
                this.handlerStart = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();

                this.SubscribeEvents();
            }

            public string KeyCode
            {
                get
                {
                    return this.keyInfo.KeyCode;
                }
            }

            private void SubscribeEvents()
            {
                eventsHook.KeyUp += this.OnKeyUp;
                eventsHook.KeyDown += this.OnKeyDown;
            }

            private void UnsubscribeEvents()
            {
                eventsHook.KeyUp -= this.OnKeyUp;
                eventsHook.KeyDown -= this.OnKeyDown;
            }

            private void OnKeyDown(object sender, KeyEventArgs e)
            {
                if (this.IsPaused)
                {
                    return;
                }

                if (e.KeyCode.ToString().Equals(KeyCode))
                {
                    if (this.KeyUpEvent != null && !this.hasKeyBeenHolded)
                    {
                        this.hasKeyBeenHolded = true;
                        this.KeyHoldEvent?.Invoke(this, this.CreateKeyHoldEventArgs((KeyEventArgsExt)e, this.keyInfo));
                    }

                    e.Handled = this.Handled;
                }
            }

            private void OnKeyUp(object sender, KeyEventArgs e)
            {
                if (this.IsPaused)
                {
                    return;
                }

                if (e.KeyCode.ToString().Equals(KeyCode))
                {
                    if (this.KeyUpEvent != null)
                    {
                        this.KeyUpEvent.Invoke(this, this.CreateKeyHoldEventArgs((KeyEventArgsExt)e, this.keyInfo));
                        this.UnsubscribeEvents();
                    }
                }
            }

            private KeyHoldEventArgs CreateKeyHoldEventArgs(KeyEventArgsExt e, KeyInfo keyInfo)
            {
                long handlerStop = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
                int timeKeyHolded = (int)(handlerStop - this.handlerStart);
                return new KeyHoldEventArgs(keyInfo, e, timeKeyHolded, this.hasKeyBeenHolded);
            }

            public void Pause()
            {
                this.IsPaused = true;
            }

            public void Resume()
            {
                this.IsPaused = false;
            }

            public void Dispose()
            {
                this.UnsubscribeEvents();
                this.KeyHoldEvent = null;
                this.KeyUpEvent = null;
            }

        }

    }
}
