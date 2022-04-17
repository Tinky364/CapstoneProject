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
        
        private readonly LampConnectionService _lampConnectionService;
        private readonly JsonDatabaseService _jsonDatabaseService;

        private readonly NavigationStore _navigationStore;

        public App()
        {
            _lampConnectionService = new LampConnectionService();
            _jsonDatabaseService = new JsonDatabaseService();
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _lamp = _lampConnectionService.ConnectToLamp();
            
            _jsonDatabaseService.CatchAllDailyData(_lamp);
            _jsonDatabaseService.UpdateDailyDataDatabase(_lamp);
            
            _lamp.SortAllDailyData();
            
            _navigationStore.CurrentViewModel = Create_LampConnectedViewModel();
            MainWindow = new MainWindow {DataContext = new MainViewModel(_navigationStore)};
            MainWindow.Show();
            
            base.OnStartup(e);
        }

#region View Model Creators
        private LampConnectedViewModel Create_LampConnectedViewModel()
        {
            return new LampConnectedViewModel(
                Create_LampOverviewViewModel(), Create_LampDailyAnalysisViewModel() 
            );
        }

        private LampSettingsViewModel Create_LampSettingsViewModel()
        {
            return new LampSettingsViewModel(
                _lamp, new NavigationService(_navigationStore, Create_LampConnectedViewModel)
            );
        }
        
        private LampOverviewViewModel Create_LampOverviewViewModel()
        {
            return new LampOverviewViewModel(
                _lamp, new NavigationService(_navigationStore, Create_LampSettingsViewModel)
            );
        }
        
        private LampDailyAnalysisViewModel Create_LampDailyAnalysisViewModel()
        {
            return new LampDailyAnalysisViewModel(_lamp);
        }
#endregion
    }
}
