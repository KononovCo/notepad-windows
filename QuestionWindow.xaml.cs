using System.Windows;
using System.Windows.Controls;

namespace Notepad
{
    public partial class QuestionWindow : Window
    {
        public string Result { get; private set; }

        public QuestionWindow(string message)
        {
            InitializeComponent();
            Save.Focus();

            Question.Text = message;
        }

        public string GetSave()
        {
            return Save.Name;
        }

        public string GetNotSave()
        {
            return NotSave.Name;
        }

        public string GetCancel()
        {
            return Cancel.Name;
        }

        private void Action_Click(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name;

            if (name == Save.Name) Result = Save.Name;
            else if (name == NotSave.Name) Result = NotSave.Name;
            else if (name == Cancel.Name) Result = Cancel.Name;

            Close();
        }
    }
}