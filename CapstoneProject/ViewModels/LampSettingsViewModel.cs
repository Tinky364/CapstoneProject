using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LampSettingsViewModel : ViewModelBase
    {
        private readonly Lamp _lamp;
        
        public ICommand GoToLampAnalysisView { get; }

        public LampSettingsViewModel(Lamp lamp, NavigationService lampAnalysisViewNavigationService)
        {
            _lamp = lamp;
            GoToLampAnalysisView = new NavigateCommand(lampAnalysisViewNavigationService);
        }
    }
}
