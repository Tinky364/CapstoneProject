namespace CapstoneProject.ViewModels
{
    public class LampConnectedViewModel : ViewModelBase
    {
        public LampOverviewViewModel LampOverviewViewModel { get; }
        public LampDailyAnalysisViewModel LampDailyAnalysisViewModel { get; }

        public LampConnectedViewModel(
            LampOverviewViewModel lampOverviewViewModel,
            LampDailyAnalysisViewModel lampDailyAnalysisViewModel
        )
        {
            LampOverviewViewModel = lampOverviewViewModel;
            LampDailyAnalysisViewModel = lampDailyAnalysisViewModel;
        }
    }
}
