using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LampOverviewViewModel : ViewModelBase
    {
        public LampViewModel LampViewModel { get; }
        
        public string ConnectionStatus { get; }
        public string Automated { get; }
        
        public ICommand GoToLampSettingsViewCommand { get; }
        public ICommand DisconnectLampCommand { get; }
        
        public LampOverviewViewModel(
            LampConnectionService lampConnectionService, 
            NavigationService lampSettingsViewNavigationService)
        {
            LampViewModel = new LampViewModel(lampConnectionService.GetConnectedLamp());
            
            ConnectionStatus = LampViewModel.ConnectionStatus ? "Connected" : "Disconnected";
            Automated = LampViewModel.Automated ? "On" : "Off";
            
            GoToLampSettingsViewCommand = new NavigateCommand(lampSettingsViewNavigationService);
            DisconnectLampCommand = new DisconnectLampCommand(lampConnectionService);
        }
    }
}
