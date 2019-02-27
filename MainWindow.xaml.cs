using L45.CaretPosition;
using L45.SpecialCharWinEnhance.Implementation;
using L45.SpecialCharWinEnhance.KeyBind;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using System.Windows.Markup;
using System.IO;
using System.Xml;
using System.Windows.Media;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Input;
using L45.KeyHoldHook;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace L45SpecialCharWinEnhance
{
    public partial class MainWindow : MetroWindow
    {

        BindedKeyHoldManager bindedKeyHoldManager;

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

        public void SubscribeEvents()
        {
            if (this.bindedKeyHoldManager == null)
            {
                this.bindedKeyHoldManager = new BindedKeyHoldManager();
                this.bindedKeyHoldManager.BindedKeyHoldEvent += OnBindedKeyHoldEvent;
            }

            if (this.windowControls == null)
            {
                this.windowControls = new WindowControls();
                this.windowControls.WindowHideEvent += this.HideWindow;
            }
        }

        public void UnsubscribeEvents()
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
            this.windowUIHelper.ShowWindow();
            //this.windowWin32Helper.forceSetForegroundWindow();
        }

        public void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            this.UnsubscribeEvents();
        }
    }

}
