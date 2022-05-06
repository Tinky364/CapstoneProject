using CapstoneProject.Services;
using CapstoneProject.Stores;
using CapstoneProject.ViewModels;

namespace CapstoneProject
{
    public class ViewModelFactory
    {
        private readonly NavigationStore _navigationStore;
        private readonly ConnectToLampService _connectToLampService;

        public ViewModelFactory(
            NavigationStore navigationStore, ConnectToLampService connectToLampService
        )
        {
            _navigationStore = navigationStore;
            _connectToLampService = connectToLampService;
        }
        
        public LandingPageViewModel Create_LandingPageViewModel()
        {
            return new LandingPageViewModel(
                _connectToLampService, Create_LampConnectionViewModel, 
                Create_LampOverviewViewModel, Create_LampDailyAnalysisViewModel
            );
        }
        
        public LampConnectionViewModel Create_LampConnectionViewModel()
        {
            return new LampConnectionViewModel(_connectToLampService);
        }
        
        public LampOverviewViewModel Create_LampOverviewViewModel()
        {
            return new LampOverviewViewModel(
                _connectToLampService, 
                new NavigationService(_navigationStore, Create_LampSettingsViewModel)
            );
        }
        
        public LampDailyAnalysisViewModel Create_LampDailyAnalysisViewModel()
        {
            return new LampDailyAnalysisViewModel(_connectToLampService);
        }

        public LampSettingsViewModel Create_LampSettingsViewModel()
        {
            return new LampSettingsViewModel(
                _connectToLampService,
                new NavigationService(_navigationStore, Create_LandingPageViewModel)
            );
        }
    }
}
