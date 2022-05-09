using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels;

public class LandingPageViewModel : ViewModelBase
{
    private readonly DatabaseService _databaseService;
    
    private ViewModelBase _overviewContent;
    public ViewModelBase OverviewContent
    {
        get => _overviewContent;
        set
        {
            _overviewContent = value;
            OnPropertyChanged(nameof(OverviewContent));
        }
    }
        
    private ViewModelBase _analysisContent;
    public ViewModelBase AnalysisContent
    {
        get => _analysisContent;
        set
        {
            _analysisContent = value;
            OnPropertyChanged(nameof(AnalysisContent));
        }
    }

    private string _overviewHeader;
    public string OverviewHeader
    {
        get => _overviewHeader;
        set
        {
            _overviewHeader = value;
            OnPropertyChanged(nameof(OverviewHeader));
        }
    }

    private readonly ObservableCollection<string> _databaseLampIds;
    public IEnumerable<string> DatabaseLampIds => _databaseLampIds;
    
    private string _selectedDatabaseLampId;
    public string SelectedDatabaseLampId 
    {
        get => _selectedDatabaseLampId;
        set
        {
            _selectedDatabaseLampId = value;
            OnPropertyChanged(nameof(SelectedDatabaseLampId));
            if (int.TryParse(SelectedDatabaseLampId, out int lampId))
                AnalysisContent = _createDailyAnalysisListViewModel(lampId);
        }
    }

    private readonly Func<ViewModelBase> _createLampConnectionViewModel;
    private readonly Func<LampOverviewViewModel> _createLampOverviewViewModel;
    private readonly Func<int, DailyAnalysisListViewModel> _createDailyAnalysisListViewModel;
    private readonly Func<DailyAnalysisTextViewModel> _createDailyAnalysisTextViewModel;
        
    public LandingPageViewModel(
        LampConnectionService lampConnectionService,
        DatabaseService databaseService,
        Func<LampConnectionViewModel> createLampConnectionViewModel,
        Func<LampOverviewViewModel> createLampOverviewViewModel,
        Func<int, DailyAnalysisListViewModel> createDailyAnalysisListViewModel,
        Func<DailyAnalysisTextViewModel> createDailyAnalysisTextViewModel
    )
    {
        lampConnectionService.AddListenerToLampConnected(OnLampConnected);
        lampConnectionService.AddListenerToLampDisconnected(OnLampDisconnected);

        _databaseService = databaseService;
        _databaseLampIds = new ObservableCollection<string>();
        UpdateDatabaseLampIdsList();
        
        _createLampConnectionViewModel = createLampConnectionViewModel;
        _createLampOverviewViewModel = createLampOverviewViewModel;
        _createDailyAnalysisListViewModel = createDailyAnalysisListViewModel;
        _createDailyAnalysisTextViewModel = createDailyAnalysisTextViewModel;

        if (lampConnectionService.IsLampConnected())
        {
            OnLampConnected(lampConnectionService.GetConnectedLamp());
        }
        else
        {
            AnalysisContent = _createDailyAnalysisTextViewModel();
            OnLampDisconnected();
        }
    }

    private void UpdateDatabaseLampIdsList()
    {
        _databaseLampIds.Clear();
        foreach (int lampId in _databaseService.GetAllDatabaseLampIds())
            _databaseLampIds.Add(lampId.ToString());
    }

    private void OnLampConnected(Lamp lamp)
    {
        OverviewContent = _createLampOverviewViewModel();
        AnalysisContent = _createDailyAnalysisListViewModel(lamp.Id);
        OverviewHeader = "Lamp Overview";
        UpdateDatabaseLampIdsList();
        SelectedDatabaseLampId = lamp.Id.ToString();
    }

    private void OnLampDisconnected()
    {
        OverviewContent = _createLampConnectionViewModel();
        OverviewHeader = "Lamp Connection";
    }
}
