using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;

namespace L45SpecialCharWinEnhance
{
    class WindowUIHelper
    {
        private const int bMargin = 5;
        private const int bSize = 40;

        private MainWindow window;

        private string templateBtn;
        private List<System.Windows.Controls.Button> btnList = new List<System.Windows.Controls.Button>();

        public WindowUIHelper(MainWindow window)
        {
            this.window = window;

            templateBtn = XamlWriter.Save(this.window.button);

            this.window.Activated += OnShow;
            this.window.Deactivated += OnLostFocus;
        }

        public void ClearUI()
        {
            this.btnList.Clear();
            this.window.grid.Children.Clear();
            this.window.grid.ColumnDefinitions.Clear();
            this.window.grid.RowDefinitions.Clear();
        }

        public void HideWindow()
        {
            this.window.Visibility = Visibility.Hidden;
        }

        public void ShowWindow()
        {
            this.window.Visibility = Visibility.Visible;
            this.setWindowInForeground();
        }

        public Button CreateButton(string content)
        {
            StringReader stringReader = new StringReader(templateBtn);
            XmlReader xmlReaderTplBtn = XmlReader.Create(stringReader);
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)XamlReader.Load(xmlReaderTplBtn);
            btn.Height = bSize;
            btn.Width = bSize;
            btn.HorizontalAlignment = HorizontalAlignment.Left;
            btn.VerticalAlignment = VerticalAlignment.Top;
            btn.Content = content;

            btn.Click += OnBtnClick;

            return btn;
        }

        public void CreateNewButtons(string[] texts)
        {
            this.ClearUI();
            int i = 0;

            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(bSize + bMargin);
            this.window.grid.RowDefinitions.Add(row);

            foreach (string text in texts)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(bSize + bMargin);
                this.window.grid.ColumnDefinitions.Add(cd);

                Button btn = this.CreateButton(text);
                this.window.grid.Children.Add(btn);

                Grid.SetColumn(btn, i);
                Grid.SetRow(btn, 0);

                this.btnList.Add(btn);
                i++;
            }
        }

        public void OnBtnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)e.Source;
            string text = (string)btn.Content;

            this.HideWindow();
            System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
            System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
            System.Windows.Forms.SendKeys.SendWait(text);
        }

        public void OnShow(object sender, EventArgs e)
        {
            if (this.btnList.Count > 0)
            {
                this.window.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        public void OnLostFocus(object sender, EventArgs e)
        {
            this.HideWindow();
        }

        public void setWindowInForeground()
        {
            this.window.Topmost = false;
            this.window.Topmost = true;

            this.window.Activate();
        }
    }
}
