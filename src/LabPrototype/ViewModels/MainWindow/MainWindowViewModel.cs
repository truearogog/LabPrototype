namespace LabPrototype.ViewModels.MainWindow
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