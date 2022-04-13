using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LampOverviewViewModel : ViewModelBase
    {
        public LampViewModel LampViewModel { get; }
        
        public ICommand GoToLampSettingsView { get; }
        
        public LampOverviewViewModel(Lamp lamp, NavigationService lampSettingsViewNavigationService)
        {
            LampViewModel = new LampViewModel(lamp);
            GoToLampSettingsView = new NavigateCommand(lampSettingsViewNavigationService);
        }
    }
}
