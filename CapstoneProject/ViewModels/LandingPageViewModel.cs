using System;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LandingPageViewModel : ViewModelBase
    {
        private ViewModelBase _contentViewModel;
        public ViewModelBase ContentViewModel
        {
            get => _contentViewModel;
            set
            {
                _contentViewModel = value;
                OnPropertyChanged(nameof(ContentViewModel));
            }
        }

        private readonly Func<ViewModelBase> _createLampConnectionViewModel;
        private readonly Func<LampOverviewViewModel> _createLampOverviewViewModel;
        private readonly Func<LampDailyAnalysisViewModel> _createDailyAnalysisViewModel;
        
        public LampDailyAnalysisViewModel LampDailyAnalysisViewModel { get; set; }

        public LandingPageViewModel(
            ConnectToLampService connectToLampService, 
            Func<ViewModelBase> createLampConnectionViewModel,
            Func<LampOverviewViewModel> createLampOverviewViewModel,
            Func<LampDailyAnalysisViewModel> createLampDailyAnalysisViewModel
        )
        {
            connectToLampService.AddListenerToLampConnected(OnLampConnected);
            connectToLampService.AddListenerToLampDisconnected(OnLampDisconnected);

            _createLampConnectionViewModel = createLampConnectionViewModel;
            _createLampOverviewViewModel = createLampOverviewViewModel;
            _createDailyAnalysisViewModel = createLampDailyAnalysisViewModel;

            ContentViewModel = _createLampConnectionViewModel();
        }

        private void OnLampConnected(Lamp lamp)
        {
            LampDailyAnalysisViewModel = _createDailyAnalysisViewModel();
            
            ContentViewModel = _createLampOverviewViewModel();
        }

        private void OnLampDisconnected()
        {
            ContentViewModel = _createLampConnectionViewModel();
        }
    }
}
