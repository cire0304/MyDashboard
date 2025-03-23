using System.Windows;
using System.Windows.Controls;

namespace DashBoard.Controls
{
    /// <summary>
    /// TextBoxStyles.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TextBoxControl : UserControl
    {

        public static readonly DependencyProperty WaterMarkTextProperty = 
            DependencyProperty.Register("WaterMarkText", typeof(string), typeof(TextBoxControl), new PropertyMetadata(null));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public TextBoxControl()
        {
            InitializeComponent();
        }

        public string WaterMarkText
        {
            get { return (string)GetValue(WaterMarkTextProperty); }
            set { SetValue(WaterMarkTextProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

    }
}
