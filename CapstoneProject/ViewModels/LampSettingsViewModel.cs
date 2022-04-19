using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LampSettingsViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string OnTime => $"{OnTimeHour}:{OnTimeMin}";
        private string _onTimeHour;
        public string OnTimeHour
        {
            get => _onTimeHour;
            set
            {
                _onTimeHour = value;
                OnPropertyChanged(nameof(OnTimeHour));
            }
        }
        private string _onTimeMin;
        public string OnTimeMin
        {
            get => _onTimeMin;
            set
            {
                _onTimeMin = value;
                OnPropertyChanged(nameof(OnTimeMin));
            }
        }
        
        public string OffTime => $"{OffTimeHour}:{OffTimeMin}";
        private string _offTimeHour;
        public string OffTimeHour
        {
            get => _offTimeHour;
            set
            {
                _offTimeHour = value;
                OnPropertyChanged(nameof(OffTimeHour));
            }
        }
        private string _offTimeMin;
        public string OffTimeMin
        {
            get => _offTimeMin;
            set
            {
                _offTimeMin = value;
                OnPropertyChanged(nameof(OffTimeMin));
            }
        }

        private bool _automated;
        public bool Automated
        {
            get => _automated;
            set
            {
                _automated = value;
                OnPropertyChanged(nameof(Automated));
            }
        }
        
        public LampViewModel LampViewModel { get; }
        
        public ICommand GoToLampAnalysisViewCommand { get; }
        public ICommand SaveLampSettingsCommand { get; }

        public LampSettingsViewModel(
            ConnectToLampService connectToLampService,
            NavigationService lampAnalysisViewNavigationService)
        {
            Lamp lamp = connectToLampService.GetConnectedLamp();
            
            LampViewModel = new LampViewModel(lamp);
            
            _name = LampViewModel.Name;
            _onTimeHour = lamp.OnTime.ToString(@"hh");
            _onTimeMin = lamp.OnTime.ToString(@"mm");
            _offTimeHour = lamp.OffTime.ToString(@"hh");
            _offTimeMin = lamp.OffTime.ToString(@"mm");
            _automated = LampViewModel.Automated;
            
            GoToLampAnalysisViewCommand = new NavigateCommand(lampAnalysisViewNavigationService);
            SaveLampSettingsCommand = new SaveLampSettingsCommand(lamp, this);
            
            connectToLampService.AddListenerToConnectedLampChanged(OnConnectedLampChanged);
        }

        private void OnConnectedLampChanged(Lamp lamp)
        {
            
        }
    }
}
