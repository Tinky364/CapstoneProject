using System;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LandingPageViewModel : ViewModelBase
    {
        private ViewModelBase _overviewContent;
        public ViewModelBase OverviewContent
        {
            get => _overviewContent;
            set
            {
                _overviewContent = value;
                OnPropertyChanged(nameof(OverviewContent));
            }
        }
        
        private ViewModelBase _analysisContent;
        public ViewModelBase AnalysisContent
        {
            get => _analysisContent;
            set
            {
                _analysisContent = value;
                OnPropertyChanged(nameof(AnalysisContent));
            }
        }

        private string _overviewHeader;
        public string OverviewHeader
        {
            get => _overviewHeader;
            set
            {
                _overviewHeader = value;
                OnPropertyChanged(nameof(OverviewHeader));
            }
        }

        private readonly Func<ViewModelBase> _createLampConnectionViewModel;
        private readonly Func<LampOverviewViewModel> _createLampOverviewViewModel;
        private readonly Func<LampDailyAnalysisViewModel> _createDailyAnalysisViewModel;
        
        public LandingPageViewModel(
            LampConnectionService lampConnectionService, 
            Func<ViewModelBase> createLampConnectionViewModel,
            Func<LampOverviewViewModel> createLampOverviewViewModel,
            Func<LampDailyAnalysisViewModel> createLampDailyAnalysisViewModel
        )
        {
            lampConnectionService.AddListenerToLampConnected(OnLampConnected);
            lampConnectionService.AddListenerToLampDisconnected(OnLampDisconnected);

            _createLampConnectionViewModel = createLampConnectionViewModel;
            _createLampOverviewViewModel = createLampOverviewViewModel;
            _createDailyAnalysisViewModel = createLampDailyAnalysisViewModel;

            if (lampConnectionService.IsLampConnected()) OnLampConnected(null);
            else OnLampDisconnected();
        }

        private void OnLampConnected(Lamp lamp)
        {
            OverviewContent = _createLampOverviewViewModel();
            AnalysisContent = _createDailyAnalysisViewModel();
            OverviewHeader = "Lamp Overview";
        }

        private void OnLampDisconnected()
        {
            OverviewContent = _createLampConnectionViewModel();
            AnalysisContent = null;
            OverviewHeader = "Lamp Connection";
        }
    }
}
