using Microsoft.Win32;

namespace Notepad
{
    public class FieldsState
    {
        public const int MAX_FONT_SIZE = 100;
        public const int MIN_FONT_SIZE = 4;
        public const int NORMAL_FONT_SIZE = 20;
        public const string TITLE = "Новый файл";
        public const string APP_NAME = " – Notepad";
        public const string DATE_FORMAT = "dd MMMM, yyyy год, HH:mm:ss";

        public bool IsSave { get; set; } = true;
        public bool IsCopy { get; set; } = false;
        public bool IsOpenFile { get; set; } = false;

        public int MatchPosition { get; set; } = 0;
        public string FileName { get; set; } = "Новый файл";

        public SaveFileDialog SaveFileDialog
        {
            get
            {
                return (SaveFileDialog)GetFileDialog(new SaveFileDialog());
            }
        }

        public OpenFileDialog OpenFileDialog
        {
            get
            {
                return (OpenFileDialog)GetFileDialog(new OpenFileDialog());
            }
        }

        private FileDialog GetFileDialog(FileDialog dialog)
        {
            if (dialog.Equals(new SaveFileDialog())) dialog.Title = "Сохранить";
            else if (dialog.Equals(new OpenFileDialog())) dialog.Title = "Открыть";

            dialog.FileName = "";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Текстовый файл (*.txt)|*.txt|Все файлы (*.*)|*.*";

            return dialog;
        }
    }
}