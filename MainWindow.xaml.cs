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
        private const int bMargin = 5;
        private const int bSize = 40;

        string templateBtn;
        List<System.Windows.Controls.Button> btnList = new List<System.Windows.Controls.Button>();

        BindedKeyHoldManager bindedKeyHoldManager;
        L45KeyHoldHook keyHoldHook;

        public MainWindow()
        {
            InitializeComponent();
            this.SubscribeEvents();

            templateBtn = XamlWriter.Save(this.button);
            this.ClearUI();
            this.Visibility = Visibility.Hidden;

            this.Topmost = true;
            this.Activated += OnShow;
            this.Deactivated += OnLostFocus;
        }

        public void SubscribeEvents()
        {
            if (this.bindedKeyHoldManager != null)
            {
                return;
            }

            this.bindedKeyHoldManager = new BindedKeyHoldManager();
            this.bindedKeyHoldManager.BindedKeyHoldEvent += OnBindedKeyHoldEvent;

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            if (this.keyHoldHook != null)
            {
                return;
            }

            this.keyHoldHook = new L45KeyHoldHook();
            this.keyHoldHook.KeyDownEvent += OnKeyDown;
        }

        public void UnsubscribeEvents()
        {
            if (this.bindedKeyHoldManager == null)
            {
                return;
            }

            this.bindedKeyHoldManager.BindedKeyHoldEvent -= OnBindedKeyHoldEvent;
            this.bindedKeyHoldManager.Dispose();
            this.bindedKeyHoldManager = null;

            if (this.keyHoldHook == null)
            {
                return;
            }

            this.keyHoldHook.KeyDownEvent -= OnKeyDown;
            this.keyHoldHook = null;
        }

        public void OnKeyDown(object caller, KeyHoldEventArgs e)
        {
            if (e.KeyEventArgs.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.Visibility = Visibility.Hidden;
            }
        }

        public void OnBindedKeyHoldEvent(object caller, KeyBindEventArgs e)
        {
            System.Drawing.Point caretPosition = GlobalCaretPosition.GetCurrentCaretPosition();
            Debug.WriteLine("Caret position: {0} {1}", caretPosition.X, caretPosition.Y);
            this.CreateNewButtons(e.Binding.Value);
            this.Visibility = Visibility.Visible;

            this.forceSetForegroundWindow();
            Debug.WriteLine("FOCUS: {0}", this.IsFocused);

            //this.btnList[0].Focus();
        }

        public System.Windows.Controls.Button CreateButton(string content)
        {
            StringReader stringReader = new StringReader(templateBtn);
            XmlReader xmlReaderTplBtn = XmlReader.Create(stringReader);
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)XamlReader.Load(xmlReaderTplBtn);
            btn.Height = bSize;
            btn.Width = bSize;
            btn.HorizontalAlignment = HorizontalAlignment.Left;
            btn.VerticalAlignment = VerticalAlignment.Top;
            btn.Content = content;

            btn.GotFocus += OnBtnFocus;

            return btn;
        }

        public void ClearUI()
        {
            this.btnList.Clear();
            this.grid.Children.Clear();
            this.grid.ColumnDefinitions.Clear();
            this.grid.RowDefinitions.Clear();
        }

        public void CreateNewButtons(string[] texts)
        {
            this.ClearUI();
            int i = 0;

            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(bSize + bMargin);
            this.grid.RowDefinitions.Add(row);

            foreach (string text in texts)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(bSize + bMargin);
                this.grid.ColumnDefinitions.Add(cd);

                Button btn = this.CreateButton(text);
                this.grid.Children.Add(btn);

                Grid.SetColumn(btn, i);
                Grid.SetRow(btn, 0);

                btn.Click += OnBtnClick;

                this.btnList.Add(btn);
                i++;
            }
        }

        public void OnBtnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)e.Source;
            string text = (string)btn.Content;

            this.Visibility = Visibility.Hidden;
            System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
            System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
            System.Windows.Forms.SendKeys.SendWait(text);
        }

        public void OnBtnFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)e.Source;
            //btn.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        public void OnLostFocus(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        public void OnShow(object sender, EventArgs e)
        {
            if (this.btnList.Count > 0)
            {
                //this.Focus();
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            this.UnsubscribeEvents();
        }


        /*- Retrieves Id of the thread that created the specified window -*/
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, out uint lpdwProcessId);

        /*- Retrieves Handle to the ForeGroundWindow -*/
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();


        [DllImport("user32.dll")]
        static extern IntPtr AttachThreadInput(IntPtr idAttach,
                         IntPtr idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public void forceSetForegroundWindow()
        {
            Process currentProcess = Process.GetCurrentProcess();
            int pidc = currentProcess.Id;

            var wih = new WindowInteropHelper(this);
            IntPtr hWnd = wih.Handle;

            uint pid;
            uint foregroundThreadID = GetWindowThreadProcessId((int)GetForegroundWindow(), out pid);
            if (foregroundThreadID != pidc)
            {
                AttachThreadInput((IntPtr)pidc, (IntPtr)foregroundThreadID, true);
                SetForegroundWindow(hWnd);
                AttachThreadInput((IntPtr)pidc, (IntPtr)foregroundThreadID, false);
            }
            else
                SetForegroundWindow(hWnd);
        }
    }

}
