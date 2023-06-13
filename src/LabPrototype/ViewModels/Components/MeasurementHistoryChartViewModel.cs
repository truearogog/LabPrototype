using System.Linq;
using System.Drawing;
using System.Windows.Input;
using System.Threading.Tasks;
using LabPrototype.Models.Interfaces;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Domain.IStores;
using ReactiveUI;
using System.Collections.Generic;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryChartViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeasurementGroupStore _measurementGroupStore;
        private readonly ToggleMeasurementListingViewModel _toggleMeasurementListingViewModel;

        private Meter? _meter = null;
        public Meter? Meter
        {
            get => _meter;
            set
            {
                this.RaiseAndSetIfChanged(ref _meter, value);
            }
        }

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

        public IPlotProvider? PlotProvider { get; set; } = null;

        public ICommand ToggleLockXAxisCommand { get; }
        public ICommand ToggleLockYAxisCommand { get; }

        private IEnumerable<MeasurementGroup>? _measurementGroups = null;

        public MeasurementHistoryChartViewModel(ToggleMeasurementListingViewModel toggleMeasurementListingViewModel)
        {
            _measurementGroupStore = GetRequiredService<IMeasurementGroupStore>();
            _measurementGroupStore.ModelsLoaded += _MeasurementGroupsLoaded;
            Task.Run(_measurementGroupStore.LoadAll);

            _toggleMeasurementListingViewModel = toggleMeasurementListingViewModel;
            _toggleMeasurementListingViewModel.OnChecked += _OnChecked;

            ToggleLockXAxisCommand = ReactiveCommand.Create(() => LockXAxis = !LockXAxis);
            ToggleLockYAxisCommand = ReactiveCommand.Create(() => LockYAxis = !LockYAxis);
        }

        public override void Dispose()
        {
            _measurementGroupStore.ModelsLoaded -= _MeasurementGroupsLoaded;
            _toggleMeasurementListingViewModel.OnChecked -= _OnChecked;
            base.Dispose();
        }

        private void _MeasurementGroupsLoaded(IEnumerable<MeasurementGroup> measurementGroups)
        {
            _measurementGroups = measurementGroups;
            CreateSeries();
        }

        public void UpdateNearestMeasurementGroup(int nearestIndex)
        {
            if (_measurementGroups is not null)
            {
                var measurementGroup = _measurementGroups.ElementAt(nearestIndex);
                _toggleMeasurementListingViewModel.UpdateMeasurementGroup(measurementGroup);
            }
        }

        private void CreateSeries()
        {
            if (PlotProvider is not null && Meter is not null)
            {
                PlotProvider.ClearPlots();

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

        private void _OnChecked(int id, bool isChecked)
        {
            PlotProvider?.SetPlotVisibility(id, isChecked);
        }
    }
}
