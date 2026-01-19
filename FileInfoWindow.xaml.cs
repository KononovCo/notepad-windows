using System.IO;
using System.Windows;

namespace Notepad
{
    public partial class FileInfoWindow : Window
    {
        public FileInfoWindow(string fileName)
        {
            InitializeComponent();

            try
            {
                if (fileName != FieldsState.TITLE)
                {
                    FileInfo file = new FileInfo(fileName);

                    Name.Content = file.Name;
                    Path.Content = fileName;
                    Extension.Content = file.Extension;
                    DateCreation.Content = file.CreationTime.ToString(FieldsState.DATE_FORMAT);
                    LastChange.Content = file.LastWriteTime.ToString(FieldsState.DATE_FORMAT);
                    Length.Content = (file.Length / 1024) + " КБ";
                }

                else throw new FileNotFoundException();
            }

            catch (FileNotFoundException)
            {
                Table.Visibility = Visibility.Collapsed;
                FileNotFound.Visibility = Visibility.Visible;
            }
        }
    }
}