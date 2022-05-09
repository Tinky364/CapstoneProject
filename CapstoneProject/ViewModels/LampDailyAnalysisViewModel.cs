using System.Collections.Generic;
using System.Collections.ObjectModel;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels;

public class LampDailyAnalysisViewModel : ViewModelBase
{
    private readonly Lamp _lamp;

    private ObservableCollection<LampDailyDataViewModel> _lampDailyDataList;
    public IEnumerable<LampDailyDataViewModel> LampDailyDataList => _lampDailyDataList;

    public LampDailyAnalysisViewModel(LampConnectionService lampConnectionService)
    {
        _lamp = lampConnectionService.GetConnectedLamp();
            
        _lampDailyDataList = new ObservableCollection<LampDailyDataViewModel>();
        UpdateLampDailyDataList();
            
        lampConnectionService.AddListenerToLampConnected(OnLampConnected);
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

    private void OnLampConnected(Lamp lamp)
    {
            
    }
}
