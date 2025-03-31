using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DashBoard.Controls
{
    public partial class WatermarkTextBox : UserControl
    {
        public WatermarkTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(WatermarkTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(WatermarkTextBox), new PropertyMetadata(string.Empty));

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt.Text))
                (Resources["WatermarkUp"] as Storyboard)?.Begin(WatermarkText);

            (Resources["txtGotFocus"] as Storyboard)?.Begin(Border);
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt.Text))
                (Resources["WatermarkDown"] as Storyboard)?.Begin(WatermarkText);

            (Resources["txtLostFocus"] as Storyboard)?.Begin(Border);
        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txt.IsFocused)
                return;

            if (string.IsNullOrEmpty(txt.Text))
                (Resources["WatermarkDown"] as Storyboard)?.Begin(WatermarkText);
            else
                (Resources["WatermarkUp"] as Storyboard)?.Begin(WatermarkText);
        }
    }
}
