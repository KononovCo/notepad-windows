using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Notepad
{
    public class NotepadWindow : Access
    {
        public void ClosingWindow(CancelEventArgs e)
        {
            if (!GetField().IsSave)
            {
                SelectFileAction result = GetMethod().Question();

                if (result == SelectFileAction.YES)
                {
                    if (!GetField().IsOpenFile)
                    {
                        if (!GetMethod().DialogFile(GetField().SaveFileDialog)) e.Cancel = true;
                    }

                    else File.WriteAllText(GetField().FileName, GetMain().TextBox.Text);
                }

                else if (result == SelectFileAction.CANCEL) e.Cancel = true;
            }

            if (GetField().IsCopy)
            {
                try
                {
                    Clipboard.SetText(Clipboard.GetText());
                }

                catch (COMException) { }
            }
        }
    }
}