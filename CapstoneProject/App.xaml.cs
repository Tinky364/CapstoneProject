using System;
using System.Windows;
using CapstoneProject.Models;
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
        private Lamp _lamp;
        
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _lamp = new Lamp(
                123, "MyLamp", true, new TimeSpan(18, 0, 0), new TimeSpan(4, 0, 0), 95, true
            );
            
            _navigationStore.CurrentViewModel = CreateLampAnalysisViewModel();
            MainWindow = new MainWindow {DataContext = new MainViewModel(_navigationStore)};
            MainWindow.Show();
            
            base.OnStartup(e);
        }

        private LampAnalysisViewModel CreateLampAnalysisViewModel()
        {
            return new LampAnalysisViewModel(
                _lamp, new NavigationService(_navigationStore, CreateLampSettingsViewModel)
            );
        }

        private LampSettingsViewModel CreateLampSettingsViewModel()
        {
            return new LampSettingsViewModel(
                _lamp, new NavigationService(_navigationStore, CreateLampAnalysisViewModel)
            );
        }

#region Examples
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
#endregion
    }
}
