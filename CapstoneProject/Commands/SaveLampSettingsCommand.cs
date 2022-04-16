using System;
using System.ComponentModel;
using System.Windows;
using CapstoneProject.Models;
using CapstoneProject.ViewModels;

namespace CapstoneProject.Commands
{
    public class SaveLampSettingsCommand : CommandBase
    {
        private readonly Lamp _lamp;
        private readonly LampSettingsViewModel _lampSettingsViewModel;

        public SaveLampSettingsCommand(Lamp lamp, LampSettingsViewModel lampSettingsViewModel)
        {
            _lamp = lamp;
            _lampSettingsViewModel = lampSettingsViewModel;

            _lampSettingsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        
        public override void Execute(object parameter)
        {
            try
            {
                _lamp.Name = _lampSettingsViewModel.Name.Trim().Replace(" ", "_");
                _lamp.OnTime = TimeSpan.Parse(_lampSettingsViewModel.OnTime);
                _lamp.OffTime = TimeSpan.Parse(_lampSettingsViewModel.OffTime);
                _lamp.Automated = _lampSettingsViewModel.Automated;
                
                _lampSettingsViewModel.Name = _lamp.Name;
                _lampSettingsViewModel.OnTimeHour = _lamp.OnTime.ToString(@"hh");
                _lampSettingsViewModel.OnTimeMin = _lamp.OnTime.ToString(@"mm");
                _lampSettingsViewModel.OffTimeHour = _lamp.OffTime.ToString(@"hh");
                _lampSettingsViewModel.OffTimeMin = _lamp.OffTime.ToString(@"mm");
                
                MessageBox.Show(
                    "Successfully saved.", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Invalid user inputs.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        public override bool CanExecute(object parameter)
        {
            bool result = true;

            try
            {
                if (string.IsNullOrEmpty(_lampSettingsViewModel.Name) ||
                    string.IsNullOrEmpty(_lampSettingsViewModel.OnTimeHour) ||
                    string.IsNullOrEmpty(_lampSettingsViewModel.OnTimeMin) ||
                    string.IsNullOrEmpty(_lampSettingsViewModel.OffTimeHour) ||
                    string.IsNullOrEmpty(_lampSettingsViewModel.OffTimeMin))
                {
                    result = false;
                }

                if (_lamp.Name.Equals(_lampSettingsViewModel.Name) &&
                    _lamp.OnTime == TimeSpan.Parse(_lampSettingsViewModel.OnTime) &&
                    _lamp.OffTime == TimeSpan.Parse(_lampSettingsViewModel.OffTime) &&
                    _lamp.Automated == _lampSettingsViewModel.Automated)
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            
            return result && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LampSettingsViewModel.Name) ||
                e.PropertyName == nameof(LampSettingsViewModel.OnTimeHour) ||
                e.PropertyName == nameof(LampSettingsViewModel.OnTimeMin) ||
                e.PropertyName == nameof(LampSettingsViewModel.OffTimeHour) ||
                e.PropertyName == nameof(LampSettingsViewModel.OffTimeMin) ||
                e.PropertyName == nameof(LampSettingsViewModel.Automated))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
