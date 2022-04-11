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

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateLampAnalysisViewModel();
            
            MainWindow = new MainWindow {DataContext = new MainViewModel(_navigationStore)};
            MainWindow.Show();
            base.OnStartup(e);
        }

        private LampAnalysisViewModel CreateLampAnalysisViewModel()
        {
            return new LampAnalysisViewModel();
        }

        private FirstExampleViewModel CreateFirstExampleViewModel()
        {
            return new FirstExampleViewModel(
                new NavigationService(_navigationStore, CreateSecondExampleViewModel)
            );
        }
        
        private SecondExampleViewModel CreateSecondExampleViewModel()
        {
            return new SecondExampleViewModel(
                new NavigationService(_navigationStore, CreateFirstExampleViewModel)
            );
        }
    }
}
