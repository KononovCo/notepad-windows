using System.IO;

namespace Notepad
{
    public class NotepadFile : Access
    {
        public void CreateFile()
        {
            if (GetField().IsSave) GetMethod().InitializeFields();

            else
            {
                SelectFileAction result = GetMethod().Question();

                if (result == SelectFileAction.YES)
                {
                    if (!GetField().IsOpenFile)
                    {
                        if (GetMethod().DialogFile(GetField().SaveFileDialog)) GetMethod().InitializeFields();
                    }

                    else
                    {
                        File.WriteAllText(GetField().FileName, GetMain().TextBox.Text);
                        GetMethod().InitializeFields();
                    }
                }

                else if (result == SelectFileAction.NO) GetMethod().InitializeFields();
            }
        }

        public void OpenFile()
        {
            if (GetField().IsSave) GetMethod().DialogFile(GetField().OpenFileDialog);

            else
            {
                SelectFileAction result = GetMethod().Question();

                if (result == SelectFileAction.YES)
                {
                    if (!GetField().IsOpenFile)
                    {
                        if (GetMethod().DialogFile(GetField().SaveFileDialog)) GetMethod().DialogFile(GetField().OpenFileDialog);
                    }

                    else
                    {
                        GetField().MatchPosition = 0;
                        GetField().IsSave = true;
                        GetField().IsOpenFile = true;

                        GetMain().Title = Path.GetFileName(GetField().FileName) + FieldsState.APP_NAME;

                        File.WriteAllText(GetField().FileName, GetMain().TextBox.Text);
                        GetMethod().DialogFile(GetField().OpenFileDialog);
                    }
                }

                else if (result == SelectFileAction.NO) GetMethod().DialogFile(GetField().OpenFileDialog);
            }
        }

        public void SaveFile()
        {
            if (!GetField().IsOpenFile) GetMethod().DialogFile(GetField().SaveFileDialog);

            else
            {
                GetField().IsSave = true;
                GetField().IsOpenFile = true;

                GetMain().Title = Path.GetFileName(GetField().FileName) + FieldsState.APP_NAME;
                File.WriteAllText(GetField().FileName, GetMain().TextBox.Text);
            }
        }

        public void SaveFileAs() => GetMethod().DialogFile(GetField().SaveFileDialog);
        public void CopyPath() => GetMethod().CopyPath(Path.GetFileName(GetField().FileName));
        public void CopyFullPath() => GetMethod().CopyPath(GetField().FileName);
        public void FileInfo() => GetMethod().ShowDialog(new FileInfoWindow(GetField().FileName));
    }
}