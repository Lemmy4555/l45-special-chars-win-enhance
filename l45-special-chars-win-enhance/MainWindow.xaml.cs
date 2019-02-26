using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using L45SpecialCharWinEnhance.L45KeyHoldHook;
using System.Text.RegularExpressions;
using L45SpecialCharWinEnhance.L45KeyBind;
using L45SpecialCharWinEnhance.L45CaretPosition;
using System.Timers;
using System.Drawing;
using System.Windows;

namespace L45SpecialCharWinEnhance
{
    public partial class MainWindow : Window
    {
        KeysBindHandler bindHandler;

        private static System.Timers.Timer aTimer;

        public MainWindow()
        {
            InitializeComponent();
            this.SubscribeKeyBindEvents();
        }

        private void SubscribeKeyBindEvents()
        {
            if(bindHandler == null) {
                this.bindHandler = new KeysBindHandler();
            }

            this.bindHandler.KeyUpEvent += OnKeyUp;
            this.bindHandler.KeyHoldEvent += OnKeyHold;
        }

        private void UnsubscribeKeyBindEvents()
        {
            if (this.bindHandler == null)
            {
                return;
            }

            this.bindHandler.KeyUpEvent -= OnKeyUp;
            this.bindHandler.KeyHoldEvent -= OnKeyHold;

            this.bindHandler.Dispose();
            this.bindHandler = null;
        }

        private void OnKeyHold(object sender, KeyBindEventArgs e)
        {
            Log(String.Format("Key Hold event on {0} ({1}) after {2}ms \t values: {3}", e.KeyHoldEvent.KeyInfo.Key, e.KeyHoldEvent.KeyInfo.KeyCode, e.KeyHoldEvent.TimeHoldedKey, String.Join(", ", e.Values)));
            System.Drawing.Point caretPosition = GlobalCaretPosition.GetCurrentCaretPosition();
            Log(String.Format("Caret position: {0} {1}", caretPosition.X, caretPosition.Y));
            e.Handled = true;
        }

        private void OnKeyUp(object sender, KeyHoldEventArgs e)
        {
            Log(String.Format("Send key {0} ({1}) after {2}ms", e.KeyInfo.Key, e.KeyInfo.KeyCode, e.TimeHoldedKey));
            SendKeysInner(e.KeyInfo.Key);
        }

        private void Log(string text)
        {
            logger.AppendText(text + "\n");
            logger.ScrollToEnd();
            Debug.WriteLine(text);
        }

        public void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            this.UnsubscribeKeyBindEvents();
        }

        public void SendKeysInner(string key)
        {
            try
            {
                Debug.WriteLine("Sending key: {0}", (object)key);
                SendKeys.SendWait(key);
            }
            catch (Exception e) { }
        }
    }
}
