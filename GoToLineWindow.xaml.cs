using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Notepad
{
    public partial class GoToLineWindow : Window
    {
        private int line;
        private readonly TextBox textBox;

        public string Result { get; private set; }

        public GoToLineWindow(TextBox textBox)
        {
            InitializeComponent();
            Button.Focus();

            this.textBox = textBox;

            MaxLine.Content = textBox.LineCount;
            TextBox.Text = (textBox.GetLineIndexFromCharacterIndex(textBox.CaretIndex) + 1).ToString();
        }

        public int GetLine()
        {
            return line;
        }

        public string GetButton()
        {
            return Button.Name;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex(@"^\-?[0-9]*$");

            if (regex.Matches(TextBox.Text).Count == 0) TextBox.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int line = Convert.ToInt32(TextBox.Text);

                if (line <= 0 || line > textBox.LineCount) throw new Exception();

                else
                {
                    this.line = line;
                    Result = Button.Name;

                    Close();
                }

                TextBox.Focus();
            }

            catch (Exception)
            {
                MessageBox.Show(
                    "Некорректный номер строки!", "Notepad",
                    MessageBoxButton.OK, MessageBoxImage.Error
                );
            }
        }
    }
}