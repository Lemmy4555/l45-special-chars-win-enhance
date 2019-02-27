using L45.KeyHoldHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L45.SpecialCharWinEnhance.KeyBind
{
    public class KeyBindEventArgs
    {
        public KeyHoldEventArgs KeyHoldEvent { get; }
        public KeyValuePair<string, string[]> Binding { get; }

        private bool handled;
        private Action onHookPauseRequest;
        private Action onHookResumeRequest;

        public KeyBindEventArgs(KeyHoldEventArgs keyHoldEvent, KeyValuePair<string, string[]> binding, Action onHookPauseRequest, Action onHookResumeRequest)
        {
            KeyHoldEvent = keyHoldEvent;
            Binding = binding;
            handled = false;
            this.onHookPauseRequest = onHookPauseRequest;
            this.onHookResumeRequest = onHookResumeRequest;
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

        public void PauseHook()
        {
            this.onHookPauseRequest?.Invoke();
        }

        public void ResumeHook()
        {
            this.onHookResumeRequest?.Invoke();
        }
    }
}
