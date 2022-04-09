using System.ComponentModel;
using CapstoneProject.Services;
using CapstoneProject.ViewModels;

namespace CapstoneProject.Commands
{
    public class SubmitExampleCommand : CommandBase
    {
        private readonly SecondExampleViewModel _secondExampleViewModel;
        private readonly NavigationService _firstExampleViewNavigationService;

        public SubmitExampleCommand(
            SecondExampleViewModel secondExampleViewModel,
            NavigationService firstExampleViewNavigationService
        )
        {
            _secondExampleViewModel = secondExampleViewModel;
            _firstExampleViewNavigationService = firstExampleViewNavigationService;

            _secondExampleViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return string.Equals(_secondExampleViewModel.Key, "123") &&
                base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            _firstExampleViewNavigationService.Navigate();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SecondExampleViewModel.Key))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
