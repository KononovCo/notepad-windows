using System.Windows;

namespace Notepad
{
    public class NotepadFormat : Access
    {
        public void Settings()
        {
            SettingsWindow settings = (SettingsWindow)GetMethod().ShowDialog(new SettingsWindow());

            if (settings.Result == settings.GetApply()) GetMethod().UpdateSettings();
        }

        public void ReplaceTab()
        {
            object message = MessageBox.Show(
                "Вы уверены что хотите заменить табуляцию на 4 пробела?", "Notepad",
                MessageBoxButton.YesNo, MessageBoxImage.Question
            );

            if (message.Equals(MessageBoxResult.Yes)) GetMain().TextBox.Text = GetMain().TextBox.Text.Replace("\t", "    ");
        }
    }
}