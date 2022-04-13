using CapstoneProject.Models;

namespace CapstoneProject.ViewModels
{
    public class LampDailyAnalysisViewModel : ViewModelBase
    {
        private readonly Lamp _lamp;

        public LampDailyAnalysisViewModel(Lamp lamp)
        {
            _lamp = lamp;
        }
    }
}
