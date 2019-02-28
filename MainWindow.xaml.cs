using L45.CaretPosition;
using L45.SpecialCharWinEnhance.Implementation;
using L45.SpecialCharWinEnhance.KeyBind;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using L45.KeyHoldHook;

namespace L45SpecialCharWinEnhance
{
    public partial class MainWindow : Window
    {

        private BindedKeyHoldManager bindedKeyHoldManager;
        private L45KeyHoldHook keyHoldHook;

        private WindowControls windowControls;
        private WindowWin32Helper windowWin32Helper;
        private WindowUIHelper windowUIHelper;

        public MainWindow()
        {
            this.Init();

            this.windowUIHelper.HideWindow();
            this.windowUIHelper.ClearUI();
        }

        private void Init()
        {
            this.InitializeComponent();

            this.windowWin32Helper = new WindowWin32Helper(this);
            this.windowUIHelper = new WindowUIHelper(this);

            this.SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            if (this.keyHoldHook == null)
            {
                this.keyHoldHook = new L45KeyHoldHook();
            }

            if (this.bindedKeyHoldManager == null)
            {
                this.bindedKeyHoldManager = new BindedKeyHoldManager(this.keyHoldHook);
            }
            this.bindedKeyHoldManager.BindedKeyHoldEvent += OnBindedKeyHoldEvent;

            if (this.windowControls == null)
            {
                this.windowControls = new WindowControls(this, this.keyHoldHook);
            }
            this.windowControls.WindowHideEvent += this.HideWindow;
        }

        private void UnsubscribeEvents()
        {
            if (this.bindedKeyHoldManager != null)
            {
                this.bindedKeyHoldManager.BindedKeyHoldEvent -= OnBindedKeyHoldEvent;
                this.bindedKeyHoldManager.Dispose();
                this.bindedKeyHoldManager = null;
            }

            if (this.windowControls != null)
            {
                this.windowControls.WindowHideEvent -= this.HideWindow;
                this.windowControls.Dispose();
                this.windowControls = null;
            }
        }

        public void HideWindow(object caller, EventArgs e)
        {
            this.windowUIHelper.HideWindow();
        }

        public void OnBindedKeyHoldEvent(object caller, KeyBindEventArgs e)
        {
            System.Drawing.Point caretPosition = GlobalCaretPosition.GetCurrentCaretPosition();
            Debug.WriteLine("Caret position: {0} {1}", caretPosition.X, caretPosition.Y);

            this.windowUIHelper.CreateNewButtons(e.Binding.Value);
            this.windowUIHelper.ShowWindow(caretPosition);
            if (!this.IsActive)
            {
                this.windowWin32Helper.forceSetForegroundWindow();
            }
        }

        internal void SendKey(string text)
        {
            this.keyHoldHook.Pause();
            UnbreakableSendKey.Send(text);
            this.keyHoldHook.Resume();
        }

        public void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            this.UnsubscribeEvents();
        }
    }

}
