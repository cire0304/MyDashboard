using System.Windows;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Windows.Interop;
using DashBoard.ViewModels;
using System.Windows.Input;
using DashBoard.Views.UI;

namespace DashBoard.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.Activated += MainView_Activated;
            this.Loaded += MainView_Loaded;
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = ProgramLauncherUI.DataContext as ProgramLancherViewModel;
            if (vm == null) return;

            this.InputBindings.Clear();

            for (int i = 0; i < vm.Programs.Count && i < 9; i++)
            {
                int index = i;
                var keyBinding = new KeyBinding
                {
                    Modifiers = ModifierKeys.Alt,
                    Key = Key.D1 - 1 + i,
                    Command = vm.LaunchProgramByIndexCommand,
                    CommandParameter = index
                };
                this.InputBindings.Add(keyBinding);
            }
        }

        private void MainView_Activated(object? sender, EventArgs e)
        {
            if (HospitalInfoUI.DataContext is HospitalInfoViewModel ui)
            {
                ui.OnWindowActivated();
            }
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this); 
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void btnMaximze_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
