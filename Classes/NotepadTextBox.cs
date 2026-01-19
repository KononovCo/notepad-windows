using System.IO;
using System.Text;
using System.Windows.Controls;

namespace Notepad
{
    public class NotepadTextBox : Access
    {
        public void Lines() => GetMain().Lines.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;

        public void TextChanged()
        {
            string title = Path.GetFileName(GetField().FileName) + FieldsState.APP_NAME;

            GetField().MatchPosition = 0;
            GetField().IsSave = false;

            if (GetField().IsSave) GetMain().Title = title;
            else GetMain().Title = "*" + title;
        }

        public void SelectionChanged()
        {
            int rows = GetMain().TextBox.GetLineIndexFromCharacterIndex(GetMain().TextBox.CaretIndex);
            int columns = GetMain().TextBox.CaretIndex - GetMain().TextBox.GetCharacterIndexFromLineIndex(rows);

            GetMain().CountRows.Content = GetMethod().NumberFormat(rows + 1);
            GetMain().CountColumns.Content = GetMethod().NumberFormat(columns + 1);

            GetMain().CountLines.Content = GetMethod().NumberFormat(GetMain().TextBox.LineCount);

            GetMain().CountChars.Content = $"{GetMethod().NumberFormat(GetMain().TextBox.Text.Length)} / " +
                $"{GetMethod().NumberFormat(GetMain().TextBox.MaxLength)}";
        }

        public void ScrollChanged()
        {
            StringBuilder lines = new StringBuilder();

            GetMain().Lines.ScrollToVerticalOffset(GetMain().TextBox.VerticalOffset);

            for (int i = 1; i <= GetMain().TextBox.LineCount; i++) lines.AppendLine(i.ToString());

            GetMain().Lines.Text = lines.ToString();
            GetMain().Lines.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }
    }
}