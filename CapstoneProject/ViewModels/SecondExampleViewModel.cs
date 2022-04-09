using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class SecondExampleViewModel : ViewModelBase
    {
        private string _key;
        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged(nameof(Key));
            }
        }
        
        public ICommand GoToFirstExampleCommand { get; }
        public ICommand SubmitCommand { get; }

        public SecondExampleViewModel(NavigationService firstExampleViewNavigationService)
        {
            GoToFirstExampleCommand = new NavigateCommand(firstExampleViewNavigationService);
            SubmitCommand = new SubmitExampleCommand(this, firstExampleViewNavigationService);
        }
    }
}
