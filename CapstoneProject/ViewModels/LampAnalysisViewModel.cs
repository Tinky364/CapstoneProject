using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LampAnalysisViewModel : ViewModelBase
    {
        private readonly Lamp _lamp;

        public LampViewModel LampViewModel { get; }
        
        public ICommand GoToLampSettingsView { get; }

        public LampAnalysisViewModel(Lamp lamp, NavigationService lampSettingsViewNavigationService)
        {
            _lamp = lamp;
            LampViewModel = new LampViewModel(_lamp);
            GoToLampSettingsView = new NavigateCommand(lampSettingsViewNavigationService);
        }
    }
}
