using System;
using System.Linq;
using System.Threading.Tasks;
using LabPrototype.Extensions;
using LabPrototype.Domain.Models;
using LabPrototype.Models.Interfaces;
using LabPrototype.Services.Interfaces;
using System.Windows.Input;
using ReactiveUI;
using System.Drawing;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryChartViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeasurementService _measurementService;
        private readonly ToggleMeasurementListingViewModel _toggleMeasurementListingViewModel;

        public Meter? Meter { get; set; } = null;
        public IPlotProvider? PlotProvider { get; set; } = null;

        private bool _lockXAxis;
        public bool LockXAxis
        {
            get => _lockXAxis;
            set
            {
                if (PlotProvider != null)
                {
                    PlotProvider.LockXAxis = value;
                }
                this.RaiseAndSetIfChanged(ref _lockXAxis, value);
            }
        }

        private bool _lockYAxis;
        public bool LockYAxis
        {
            get => _lockYAxis;
            set
            {
                if (PlotProvider != null)
                {
                    PlotProvider.LockYAxis = value;
                }
                this.RaiseAndSetIfChanged(ref _lockYAxis, value);
            }
        }

        public ICommand ToggleLockXAxisCommand { get; }
        public ICommand ToggleLockYAxisCommand { get; }

        public MeasurementHistoryChartViewModel(ToggleMeasurementListingViewModel toggleMeasurementListingViewModel)
        {
            _measurementService = GetRequiredService<IMeasurementService>();
            _toggleMeasurementListingViewModel = toggleMeasurementListingViewModel;
            _toggleMeasurementListingViewModel.OnChecked += OnChecked;

            ToggleLockXAxisCommand = ReactiveCommand.Create(() => LockXAxis = !LockXAxis);
            ToggleLockYAxisCommand = ReactiveCommand.Create(() => LockYAxis = !LockYAxis);
        }

        public override void Dispose()
        {
            _toggleMeasurementListingViewModel.OnChecked -= OnChecked;
            base.Dispose();
        }

        public void UpdateNearestMeasurement(int nearestIndex)
        {
            if (Meter != null)
            {
                var measurement = _measurementService.LoadedMeasurements[Meter.Id].ElementAt(nearestIndex);
                _toggleMeasurementListingViewModel.UpdateMeasurement(measurement);
            }
        }

        private void CreateSeries()
        {
            if (PlotProvider != null && Meter != null)
            {
                PlotProvider.ClearPlots();

                var measurements = _measurementService.LoadedMeasurements[Meter.Id];
                var plotIds = Meter.MeasurementAttributes.Select(a => a.Id).ToArray();
                var xs = measurements.Select(x => x.DateTime.ToOADate()).ToArray();
                var ys = Meter.MeasurementAttributes.Select(a => measurements.Select(m => (double)a.ValueGetter(m)).ToArray()).ToArray();
                var colors = Meter.MeasurementAttributes.Select(a => a.ColorScheme?.Primary.ToColor() ?? Color.White).ToArray();

                PlotProvider.AddPlots(plotIds, xs, ys, colors);
            }
        }

        public void UpdateMeter(Meter? meter)
        {
            Meter = meter;
            if (Meter != null)
            {
                Task.Run(async () => {
                    await _measurementService.LoadMeter(Meter.Id);
                    CreateSeries();
                    PlotProvider?.AddCrosshair();
                }).Wait();
            }
        }

        private void OnChecked(Guid id, bool isChecked)
        {
            PlotProvider?.SetPlotVisibility(id, isChecked);
        }
    }
}
