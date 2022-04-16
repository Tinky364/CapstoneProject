using System.Collections.Generic;
using System.Collections.ObjectModel;
using CapstoneProject.Models;

namespace CapstoneProject.ViewModels
{
    public class LampDailyAnalysisViewModel : ViewModelBase
    {
        private readonly Lamp _lamp;

        private ObservableCollection<LampDailyDataViewModel> _lampDailyDataList;

        public IEnumerable<LampDailyDataViewModel> LampDailyDataList => _lampDailyDataList;

        public LampDailyAnalysisViewModel(Lamp lamp)
        {
            _lamp = lamp;
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
}
