using System.Windows;
using DashBoard.ViewModels;
using DashBoard.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;

        public App()
        {
            Services = ConfigureServices();
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            MainView mainView = Services.GetRequiredService<MainView>();
            mainView.Show();            
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            // Store

            // Services

            // ViewModels
            services.AddSingleton<MainViewModel>();

            // Views
            services.AddSingleton<MainView>();

            return services.BuildServiceProvider();
        }
        public IServiceProvider Services { get; }

    }

}
