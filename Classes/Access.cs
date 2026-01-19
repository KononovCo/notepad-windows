using System.Windows;

namespace Notepad
{
    public abstract class Access
    {
        private static FieldsState fs;

        protected static FieldsState GetField()
        {
            if (fs == null) fs = new FieldsState();

            return fs;
        }

        protected HelperMethods GetMethod()
        {
            return new HelperMethods();
        }

        protected MainWindow GetMain()
        {
            return (MainWindow)Application.Current.MainWindow;
        }
    }
}