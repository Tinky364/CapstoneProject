using CapstoneProject.Services;
using CapstoneProject.Stores;
using CapstoneProject.ViewModels;

namespace CapstoneProject
{
    public class ViewModelFactory
    {
        private readonly NavigationStore _navigationStore;
        private readonly LampConnectionService _lampConnectionService;
        private readonly JsonDatabaseService _jsonDatabaseService;

        public ViewModelFactory(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _jsonDatabaseService = new JsonDatabaseService();
            _lampConnectionService = new LampConnectionService(
                new ConnectedLampStore(), _jsonDatabaseService
            );
        }
        
        public LandingPageViewModel Create_LandingPageViewModel()
        {
            return new LandingPageViewModel(
                _lampConnectionService, Create_LampConnectionViewModel, 
                Create_LampOverviewViewModel, Create_LampDailyAnalysisViewModel
            );
        }
        
        public LampConnectionViewModel Create_LampConnectionViewModel()
        {
            return new LampConnectionViewModel(_lampConnectionService);
        }
        
        public LampOverviewViewModel Create_LampOverviewViewModel()
        {
            return new LampOverviewViewModel(
                _lampConnectionService, 
                new NavigationService(_navigationStore, Create_LampSettingsViewModel)
            );
        }
        
        public LampDailyAnalysisViewModel Create_LampDailyAnalysisViewModel()
        {
            return new LampDailyAnalysisViewModel(_lampConnectionService);
        }

        public LampSettingsViewModel Create_LampSettingsViewModel()
        {
            return new LampSettingsViewModel(
                _lampConnectionService,
                new NavigationService(_navigationStore, Create_LandingPageViewModel)
            );
        }
    }
}
