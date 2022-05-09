using System.Collections.Generic;
using System.Collections.ObjectModel;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels;

public class DailyAnalysisListViewModel : ViewModelBase
{
    private readonly Lamp _lamp;

    private readonly ObservableCollection<LampDailyDataViewModel> _lampDailyDataList;
    public IEnumerable<LampDailyDataViewModel> LampDailyDataList => _lampDailyDataList;

    public string LampName => _lamp.Name;

    public DailyAnalysisListViewModel(DatabaseService databaseService, int lampId)
    {
        databaseService.GetDatabaseLamp(lampId, out _lamp);
            
        _lampDailyDataList = new ObservableCollection<LampDailyDataViewModel>();
        UpdateLampDailyDataList();
    }

    private void UpdateLampDailyDataList()
    {
        _lampDailyDataList.Clear();
        foreach (LampDailyData lampDailyData in _lamp.GetAllDailyData())
        {
            LampDailyDataViewModel lampDailyDataViewModel = new(lampDailyData);
            _lampDailyDataList.Add(lampDailyDataViewModel);
        }
    }
}
