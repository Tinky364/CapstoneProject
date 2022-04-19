using System.Windows;
using CapstoneProject.Services;
using CapstoneProject.Stores;
using CapstoneProject.ViewModels;

namespace CapstoneProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelFactory _viewModelFactory;

        public App()
        {
            _navigationStore = new NavigationStore();
            
            _viewModelFactory = new ViewModelFactory(
                _navigationStore, new ConnectToLampService(new ConnectedLampStore())
            );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = _viewModelFactory.Create_LampConnectionViewModel();
            MainWindow = new MainWindow {DataContext = new MainViewModel(_navigationStore)};
            MainWindow.Show();
            
            base.OnStartup(e);
        }
    }
}
