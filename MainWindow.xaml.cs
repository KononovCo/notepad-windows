using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;

namespace Notepad
{
    public enum SelectFileAction
    {
        YES,
        NO,
        CANCEL
    }

    public enum StateFontSize
    {
        INCREASE,
        DECREASE,
        NORMAL
    }

    public partial class MainWindow : Window
    {
        private readonly NotepadFile file;
        private readonly NotepadEdit edit;
        private readonly NotepadFormat format;
        private readonly NotepadView view;
        private readonly NotepadReference reference;
        private readonly NotepadTextBox textBox;
        private readonly NotepadWindow window;

        public MainWindow()
        {
            InitializeComponent();

            file = new NotepadFile();
            edit = new NotepadEdit();
            format = new NotepadFormat();
            view = new NotepadView();
            reference = new NotepadReference();
            textBox = new NotepadTextBox();
            window = new NotepadWindow();

            new HelperMethods().LoadSettings();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            object obj = null;
            RoutedEventArgs routed = new RoutedEventArgs();

            if (e.Key == Key.Delete) Delete_Click(obj, routed);

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;

                if (e.Key == Key.N) CreateFile_Click(obj, routed);
                else if (e.Key == Key.O) OpenFile_Click(obj, routed);
                else if (e.Key == Key.S) SaveFile_Click(obj, routed);

                else if (e.Key == Key.C) Copy_Click(obj, routed);
                else if (e.Key == Key.V) Paste_Click(obj, routed);
                else if (e.Key == Key.X) Cut_Click(obj, routed);
                else if (e.Key == Key.A) SelectAll_Click(obj, routed);
                else if (e.Key == Key.F) Find_Click(obj, routed);
                else if (e.Key == Key.G) GoToLine_Click(obj, routed);
                else if (e.Key == Key.Up) StartScroll_Click(obj, routed);
                else if (e.Key == Key.Down) EndScroll_Click(obj, routed);
                else if (e.Key == Key.Z) Undo_Click(obj, routed);
                else if (e.Key == Key.Y) Redo_Click(obj, routed);

                else if (e.Key == Key.K) ReplaceTab_Click(obj, routed);

                else if (e.Key == Key.OemPlus) IncreaseFontSize_Click(obj, routed);
                else if (e.Key == Key.OemMinus) DecreaseFontSize_Click(obj, routed);
                else if (e.Key == Key.D0) InitializeFontSize_Click(obj, routed);
            }

            if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                e.Handled = true;

                if (e.Key == Key.S) SaveFileAs_Click(obj, routed);
                else if (e.Key == Key.F) FileInfo_Click(obj, routed);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e) => window.ClosingWindow(e);

        private void CreateFile_Click(object sender, RoutedEventArgs e) => file.CreateFile();
        private void OpenFile_Click(object sender, RoutedEventArgs e) => file.OpenFile();
        private void SaveFile_Click(object sender, RoutedEventArgs e) => file.SaveFile();
        private void SaveFileAs_Click(object sender, RoutedEventArgs e) => file.SaveFileAs();
        private void CopyPath_Click(object sender, RoutedEventArgs e) => file.CopyPath();
        private void CopyFullPath_Click(object sender, RoutedEventArgs e) => file.CopyFullPath();
        private void FileInfo_Click(object sender, RoutedEventArgs e) => file.FileInfo();

        private void Copy_Click(object sender, RoutedEventArgs e) => edit.Copy();
        private void Paste_Click(object sender, RoutedEventArgs e) => edit.Paste();
        private void Cut_Click(object sender, RoutedEventArgs e) => edit.Cut();
        private void SelectAll_Click(object sender, RoutedEventArgs e) => edit.SelectAll();
        private void Delete_Click(object sender, RoutedEventArgs e) => edit.Delete();
        private void Find_Click(object sender, RoutedEventArgs e) => edit.Find();
        private void FindCheckedChanged_Click(object sender, RoutedEventArgs e) => edit.SaveFindCheckedChanged();
        private void ButtonFind_Click(object sender, RoutedEventArgs e) => edit.ButtonFind(sender);
        private void GoToLine_Click(object sender, RoutedEventArgs e) => edit.GoToLine();
        private void StartScroll_Click(object sender, RoutedEventArgs e) => edit.StartScroll();
        private void EndScroll_Click(object sender, RoutedEventArgs e) => edit.EndScroll();
        private void CurrentDate_Click(object sender, RoutedEventArgs e) => edit.CurrentDate();
        private void Undo_Click(object sender, RoutedEventArgs e) => edit.Undo();
        private void Redo_Click(object sender, RoutedEventArgs e) => edit.Redo();

        private void Settings_Click(object sender, RoutedEventArgs e) => format.Settings();
        private void ReplaceTab_Click(object sender, RoutedEventArgs e) => format.ReplaceTab();

        private void IncreaseFontSize_Click(object sender, RoutedEventArgs e) => view.IncreaseFontSize();
        private void DecreaseFontSize_Click(object sender, RoutedEventArgs e) => view.DecreaseFontSize();
        private void InitializeFontSize_Click(object sender, RoutedEventArgs e) => view.InitializeFontSize();

        private void Program_Click(object sender, RoutedEventArgs e) => reference.Program();

        private void Lines_ScrollChanged(object sender, ScrollChangedEventArgs e) => textBox.Lines();
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => textBox.TextChanged();
        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e) => textBox.SelectionChanged();
        private void TextBox_ScrollChanged(object sender, ScrollChangedEventArgs e) => textBox.ScrollChanged();
    }
}