using ReactiveUI;
using System;

namespace LabPrototype.ViewModels.Components
{
    public class DateTimePickerViewModel : ViewModelBase
    {
        private string _notSelectedText = "Not Selected";
        public string NotSelectedText
        {
            get => _notSelectedText;
            set => this.RaiseAndSetIfChanged(ref _notSelectedText, value);
        }

        private TimeSpan _selectedTime = TimeSpan.Zero;
        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTime, value);
                this.RaisePropertyChanged(nameof(SelectedDateTime));
            }
        }

        private DateTimeOffset? _selectedDate;
        public DateTimeOffset? SelectedDate
        {
            get => _selectedDate;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedDate, value);
                this.RaisePropertyChanged(nameof(HasSelected));
                this.RaisePropertyChanged(nameof(SelectedDateTime));
            }
        }

        public bool HasSelected => SelectedDate != null;
        public DateTime? SelectedDateTime
        {
            get => new (SelectedDate.GetValueOrDefault().Date.Ticks + SelectedTime.Ticks);
            set
            {
                SelectedDate = value?.Date;
                SelectedTime = value.GetValueOrDefault().TimeOfDay;
            }
        }

        public DateTimePickerViewModel()
        {

        }
    }
}
