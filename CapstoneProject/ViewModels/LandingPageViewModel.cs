using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LandingPageViewModel : ViewModelBase
    {
        public LampOverviewViewModel LampOverviewViewModel { get; }
        public LampDailyAnalysisViewModel LampDailyAnalysisViewModel { get; }

        public LandingPageViewModel(
            ConnectToLampService connectToLampService, LampOverviewViewModel lampOverviewViewModel,
            LampDailyAnalysisViewModel lampDailyAnalysisViewModel
        )
        {
            LampOverviewViewModel = lampOverviewViewModel;
            LampDailyAnalysisViewModel = lampDailyAnalysisViewModel;
            
            connectToLampService.AddListenerToConnectedLampChanged(OnConnectedLampChanged);
        }

        private void OnConnectedLampChanged(bool hasConnection, Lamp lamp)
        {
            
        }
    }
}
