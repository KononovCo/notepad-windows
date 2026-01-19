using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Notepad.Properties;

namespace Notepad
{
    public partial class SettingsWindow : Window
    {
        private byte red;
        private byte green;
        private byte blue;

        public string Result { get; private set; }

        public SettingsWindow()
        {
            InitializeComponent();

            SliderRed.Value = Settings.Default.Red;
            SliderGreen.Value = Settings.Default.Green;
            SliderBlue.Value = Settings.Default.Blue;
        }

        public string GetApply()
        {
            return Apply.Name;
        }

        private void ChangeValueColor(object sender)
        {
            string name = (sender as Button).Name;

            if (name == IncreaseRed.Name)
            {
                if (red < 255) red++;
            }

            if (name == IncreaseGreen.Name)
            {
                if (green < 255) green++;
            }

            if (name == IncreaseBlue.Name)
            {
                if (blue < 255) blue++;
            }

            if (name == DecreaseRed.Name)
            {
                if (red > 0) red--;
            }

            if (name == DecreaseGreen.Name)
            {
                if (green > 0) green--;
            }

            if (name == DecreaseBlue.Name)
            {
                if (blue > 0) blue--;
            }

            CountRed.Content = red;
            CountGreen.Content = green;
            CountBlue.Content = blue;

            SliderRed.Value = red;
            SliderGreen.Value = green;
            SliderBlue.Value = blue;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Apply.IsEnabled = true;

            red = (byte)SliderRed.Value;
            green = (byte)SliderGreen.Value;
            blue = (byte)SliderBlue.Value;

            CountRed.Content = red;
            CountGreen.Content = green;
            CountBlue.Content = blue;

            Sample.Foreground = new SolidColorBrush(Color.FromRgb(red, green, blue));
        }

        private void IncreaseColorValue_Click(object sender, RoutedEventArgs e) => ChangeValueColor(sender);
        private void DecreaseColorValue_Click(object sender, RoutedEventArgs e) => ChangeValueColor(sender);

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Result = Apply.Name;

            Settings.Default.Red = red;
            Settings.Default.Green = green;
            Settings.Default.Blue = blue;
            Settings.Default.Save();

            Close();
        }
    }
}