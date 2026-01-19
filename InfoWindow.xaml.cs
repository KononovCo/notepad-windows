using System.IO;
using System.Windows;
using System.Reflection;

namespace Notepad
{
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();

            string path = Assembly.GetExecutingAssembly().Location;

            DateCreation.Content = Directory.GetCreationTime(path).ToString(FieldsState.DATE_FORMAT);
            LastChange.Content = Directory.GetLastWriteTime(path).ToString(FieldsState.DATE_FORMAT);
        }
    }
}