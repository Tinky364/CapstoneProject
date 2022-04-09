using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class FirstExampleViewModel : ViewModelBase
    {
        public ICommand GoToSecondExampleCommand { get; }

        public FirstExampleViewModel(NavigationService secondExampleViewNavigationService)
        {
            GoToSecondExampleCommand = new NavigateCommand(secondExampleViewNavigationService);
        }
    }
}
