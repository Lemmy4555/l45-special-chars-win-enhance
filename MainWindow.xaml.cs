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

namespace L45SpecialCharWinEnhance
{
    public partial class MainWindow : MetroWindow
    {
        private const int bMargin = 5;
        private const int bSize = 40;

        string templateBtn;
        List<Button> btnList = new List<Button>();

        BindedKeyHoldManager bindedKeyHoldManager;

        public MainWindow()
        {
            InitializeComponent();
            this.SubscribeEvents();

            templateBtn = XamlWriter.Save(this.button);
            this.ClearUI();
            this.Hide();
            this.Topmost = true;
            this.Activated += OnShow;
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
        }

        public void OnBindedKeyHoldEvent(object caller, KeyBindEventArgs e)
        {
            System.Drawing.Point caretPosition = GlobalCaretPosition.GetCurrentCaretPosition();
            Debug.WriteLine("Caret position: {0} {1}", caretPosition.X, caretPosition.Y);
            this.CreateNewButtons(e.Binding.Value);
            this.Show();
            this.btnList[0].Focus();
        }

        public Button CreateButton(string content)
        {
            StringReader stringReader = new StringReader(templateBtn);
            XmlReader xmlReaderTplBtn = XmlReader.Create(stringReader);
            Button btn = (Button)XamlReader.Load(xmlReaderTplBtn);
            btn.Height = bSize;
            btn.Width = bSize;
            btn.HorizontalAlignment = HorizontalAlignment.Left;
            btn.VerticalAlignment = VerticalAlignment.Top;
            btn.Content = content;

            btn.GotFocus += OnBtnFocus;
            btn.LostFocus += OnLostBtnFocus;

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

                this.btnList.Add(btn);
                i++;
            }
        }

        public void OnBtnFocus(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.Source;
            btn.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        public void OnLostBtnFocus(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.Source;
            btn.BorderBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#D5D5D5"));
        }

        public void OnShow(object sender, EventArgs e)
        {
            Debug.WriteLine("ON SHOW");
            if (this.btnList.Count > 0)
            {
                //Keyboard.Focus(this.btnList[0]);
                Task.Delay(500).ContinueWith(x =>
                {

                    Dispatcher.Invoke(() =>
                    {
                        this.Focus();
                        MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    });
                });
            }
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            this.UnsubscribeEvents();
        }
    }

}
