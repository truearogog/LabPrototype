using Avalonia.Media;
using Avalonia.Media.Immutable;
using LabPrototype.Domain.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPrototype.ViewModels.MainWindow
{
    public class MeterListingItemViewModel : ViewModelBase
    {
        private Meter _meter;
        public Meter Meter
        {
            get => _meter;
            set => this.RaiseAndSetIfChanged(ref _meter, value);
        }

        public MeterListingItemViewModel(Meter meter)
        {
            Meter = meter;
        }
    }
}
