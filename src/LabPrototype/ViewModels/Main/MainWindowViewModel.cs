namespace LabPrototype.ViewModels.Main
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; }

        public MainWindowViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }
    }
}