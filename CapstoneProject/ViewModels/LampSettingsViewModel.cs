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

        private string _onTime;
        public string OnTime
        {
            get => _onTime;
            set
            {
                _onTime = value;
                OnPropertyChanged(nameof(OnTime));
            }
        }

        private string _offTime;
        public string OffTime
        {
            get => _offTime;
            set
            {
                _offTime = value;
                OnPropertyChanged(nameof(OffTime));
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

        public LampSettingsViewModel(Lamp lamp, NavigationService lampAnalysisViewNavigationService)
        {
            LampViewModel = new LampViewModel(lamp);
            _name = LampViewModel.Name;
            _onTime = LampViewModel.OnTime;
            _offTime = LampViewModel.OffTime;
            _automated = LampViewModel.Automated;
            
            GoToLampAnalysisViewCommand = new NavigateCommand(lampAnalysisViewNavigationService);
            SaveLampSettingsCommand = new SaveLampSettingsCommand(lamp, this);
        }
    }
}
