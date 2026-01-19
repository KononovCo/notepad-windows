using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Globalization;
using Microsoft.Win32;
using Notepad.Properties;

namespace Notepad
{
    public class HelperMethods : Access
    {
        public SelectFileAction Question()
        {
            QuestionWindow question = (QuestionWindow)ShowDialog(new QuestionWindow(
                $"Сохранить изменения в файле \"{Path.GetFileName(GetField().FileName)}\"?")
            );

            if (question.Result == question.GetSave()) return SelectFileAction.YES;
            else if (question.Result == question.GetNotSave()) return SelectFileAction.NO;

            return SelectFileAction.CANCEL;
        }

        public void InitializeFields()
        {
            GetMain().TextBox.Clear();

            GetField().MatchPosition = 0;
            GetField().IsSave = true;
            GetField().IsOpenFile = false;

            GetField().FileName = FieldsState.TITLE;
            GetMain().Title = GetField().FileName + FieldsState.APP_NAME;

            ClearUndoAndRedo();
        }

        public Window ShowDialog(Window window)
        {
            window.Topmost = true;

            window.ShowDialog();

            return window;
        }

        public bool DialogFile(FileDialog file)
        {
            bool result = (bool)file.ShowDialog();

            if (result)
            {
                GetField().FileName = file.FileName;

                if (file.GetType() == GetField().SaveFileDialog.GetType())
                {
                    File.WriteAllText(GetField().FileName, GetMain().TextBox.Text);
                }

                else if (file.GetType() == GetField().OpenFileDialog.GetType())
                {
                    GetField().MatchPosition = 0;
                    GetMain().TextBox.Text = File.ReadAllText(GetField().FileName);

                    ClearUndoAndRedo();
                }

                GetField().IsSave = true;
                GetField().IsOpenFile = true;

                GetMain().Title = Path.GetFileName(GetField().FileName) + FieldsState.APP_NAME;
            }

            return result;
        }

        public void CopyPath(string fileName)
        {
            GetField().IsCopy = true;

            Clipboard.SetDataObject(fileName);
        }

        public void ClearUndoAndRedo()
        {
            GetMain().TextBox.IsUndoEnabled = false;
            GetMain().TextBox.IsUndoEnabled = true;
        }

        public void ChangeFontSize(StateFontSize state)
        {
            int fontSize = Settings.Default.FontSize;

            if (state == StateFontSize.INCREASE || state == StateFontSize.DECREASE)
            {
                if (state == StateFontSize.INCREASE)
                {
                    if (fontSize < FieldsState.MAX_FONT_SIZE) fontSize += 2;
                }

                else
                {
                    if (fontSize > FieldsState.MIN_FONT_SIZE) fontSize -= 2;
                }
            }

            else fontSize = FieldsState.NORMAL_FONT_SIZE;

            GetMain().Lines.FontSize = fontSize;
            GetMain().TextBox.FontSize = fontSize;
            GetMain().Count.Content = fontSize * 5 + "%";

            Settings.Default.FontSize = fontSize;
            Settings.Default.Save();
        }

        public string NumberFormat(int number)
        {
            string transform = number.ToString("#,#", new CultureInfo("ru-RU"));

            return (transform.Length == 0) ? "0" : transform;
        }

        public void UpdateSettings()
        {
            byte red = Settings.Default.Red;
            byte green = Settings.Default.Green;
            byte blue = Settings.Default.Blue;

            GetMain().TextBox.CaretBrush = new SolidColorBrush(Color.FromRgb(red, green, blue));
            GetMain().TextBox.Foreground = new SolidColorBrush(Color.FromRgb(red, green, blue));
            GetMain().TextBox.SelectionBrush = new SolidColorBrush(Color.FromRgb(red, green, blue));
        }

        public void LoadSettings()
        {
            int fontSize = Settings.Default.FontSize;

            GetMain().TextBox.Focus();
            GetMain().Lines.FontSize = fontSize;
            GetMain().TextBox.FontSize = fontSize;
            GetMain().Count.Content = (fontSize * 5) + "%";

            UpdateSettings();
        }
    }
}