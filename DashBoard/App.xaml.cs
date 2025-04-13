using System.Configuration;
using System.IO;
using System.Windows;
using DashBoard.Controls;
using DashBoard.Options;
using DashBoard.Services;
using DashBoard.Services.IntroLauncher;
using DashBoard.Stores;
using DashBoard.ViewModels;
using DashBoard.Views;
using Microsoft.Extensions.Configuration;
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

            services.AddSingleton<IConfiguration>(s => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json")
            .Build());

            // Store
            services.AddSingleton<MainNavigationStore>();
            services.AddSingleton<MainStore>();

            services.AddSingleton<HospitalInfomationService>();

            // Services
            services.AddSingleton<ProcessLauncherService>();
            services.AddSingleton<HospitalInfomationService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ProgramLancherViewModel>();
            services.AddSingleton<HospitalInfoViewModel>();

            // Views
            services.AddSingleton(s => new MainView()
            {
                DataContext = s.GetRequiredService<MainViewModel>()               
            });

            return services.BuildServiceProvider();
        }
        public IServiceProvider Services { get; }

    }

}
