using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CapstoneProject.Models;
using CapstoneProject.Services;
using CapstoneProject.ViewModels;

namespace CapstoneProject.Commands;

public class SaveLampSettingsCommand : AsyncCommandBase
{
    private readonly Lamp _lamp;
    private readonly LampSettingsViewModel _lampSettingsViewModel;
    private readonly DatabaseService _databaseService;

    public SaveLampSettingsCommand(
        Lamp lamp, LampSettingsViewModel lampSettingsViewModel, DatabaseService databaseService
    )
    {
        _lamp = lamp;
        _lampSettingsViewModel = lampSettingsViewModel;
        _databaseService = databaseService;

        _lampSettingsViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }
        
    public override async Task ExecuteAsync(object parameter)
    {
        try
        {
            string previousName = _lamp.Name;
            string newName = _lampSettingsViewModel.Name.Trim();
            if (previousName != newName)
            {
                _lamp.Name = newName;
                await _databaseService.WriteDatabaseLampData(_lamp);
                _databaseService.DeleteDatabaseLampOnLampNameChange(_lamp.Id, previousName);
            }
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
                "Invalid user inputs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error
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
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
