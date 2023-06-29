using System.Linq;
using System.Drawing;
using System.Windows.Input;
using System.Threading.Tasks;
using LabPrototype.Models.Interfaces;
using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System.Collections.Generic;
using LabPrototype.Domain.IServices;
using LabPrototype.Framework.Extensions;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryChartViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeterTypeService _meterTypeService;
        private readonly IMeasurementGroupService _measurementGroupService;
        private readonly ToggleMeasurementListingViewModel _toggleMeasurementListingViewModel;

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
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _measurementGroupService = GetRequiredService<IMeasurementGroupService>();

            _toggleMeasurementListingViewModel = toggleMeasurementListingViewModel;
            _toggleMeasurementListingViewModel.OnChecked += _OnChecked;

            ToggleLockXAxisCommand = ReactiveCommand.Create(() => LockXAxis = !LockXAxis);
            ToggleLockYAxisCommand = ReactiveCommand.Create(() => LockYAxis = !LockYAxis);
        }

        public override void Dispose()
        {
            _toggleMeasurementListingViewModel.OnChecked -= _OnChecked;
            base.Dispose();
        }

        public void UpdateMeter(Meter? meter)
        {
            if (meter is not null)
            {
                Task.Run(() =>
                {
                    var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
                    _measurementGroups = _measurementGroupService.GetAll(x => x.MeterId.Equals(meter.Id));
                    CreateSeries(measurementTypes);
                    PlotProvider?.AddCrosshair();
                });
            }
        }

        public void UpdateNearestMeasurementGroup(int nearestIndex)
        {
            if (_measurementGroups is not null && _measurementGroups.Any())
            {
                var measurementGroup = _measurementGroups.ElementAt(nearestIndex);
                _toggleMeasurementListingViewModel.UpdateMeasurementGroup(measurementGroup);
            }
        }

        private void CreateSeries(IEnumerable<MeasurementType> measurementTypes)
        {
            if (PlotProvider is not null)
            {
                PlotProvider.ClearPlots();

                if (_measurementGroups is not null && _measurementGroups.Any())
                {
                    var xs = _measurementGroups.Select(x => x.Created.ToOADate()).ToArray();

                    foreach (var measurementType in measurementTypes)
                    {
                        var measurements = _measurementGroups?
                            .Select(x => x.Measurements?.First(y => y.MeasurementTypeId.Equals(measurementType.Id)))
                            .OfType<Measurement>() ?? Enumerable.Empty<Measurement>();

                        var ys = measurements.Select(x => x.Value).ToArray();
                        var color = measurementType.ColorScheme?.PrimaryColor?.ToColor() ?? Color.White;
                        PlotProvider.AddPlot(measurementType.Id, xs, ys, color);
                    }
                }
            }
        }

        private void _OnChecked(int id, bool isChecked)
        {
            PlotProvider?.SetPlotVisibility(id, isChecked);
        }
    }
}
