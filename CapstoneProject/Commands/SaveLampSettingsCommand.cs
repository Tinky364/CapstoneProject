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

            if (string.IsNullOrEmpty(_lampSettingsViewModel.Name) ||
                string.IsNullOrEmpty(_lampSettingsViewModel.OnTime) ||
                string.IsNullOrEmpty(_lampSettingsViewModel.OffTime))
            {
                result = false;
            }
            
            return result && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LampSettingsViewModel.Name) ||
                e.PropertyName == nameof(LampSettingsViewModel.OnTime) ||
                e.PropertyName == nameof(LampSettingsViewModel.OffTime) ||
                e.PropertyName == nameof(LampSettingsViewModel.Automated))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
