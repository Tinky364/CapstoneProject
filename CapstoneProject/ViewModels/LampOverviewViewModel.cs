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
        
        public LampOverviewViewModel(
            ConnectToLampService connectToLampService, 
            NavigationService lampSettingsViewNavigationService)
        {
            LampViewModel = new LampViewModel(connectToLampService.GetConnectedLamp());
            
            ConnectionStatus = LampViewModel.ConnectionStatus ? "Connected" : "Disconnected";
            Automated = LampViewModel.Automated ? "On" : "Off";
            
            GoToLampSettingsViewCommand = new NavigateCommand(lampSettingsViewNavigationService);
            
            connectToLampService.AddListenerToConnectedLampChanged(OnConnectedLampChanged);
        }

        private void OnConnectedLampChanged(bool hasConnection, Lamp lamp)
        {
            
        }
    }
}
