using System.Linq;
using System.Windows.Input;
using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System.Collections.Generic;
using LabPrototype.Domain.IServices;
using LabPrototype.Providers.PlotProvider;
using LabPrototype.Domain.Models.Presentation.MeasurementGroups;
using System;
using LabPrototype.Domain.Models.Presentation.Measurements;
using LabPrototype.Extensions;
using System.Drawing;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryChartViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeterTypeService _meterTypeService;
        private readonly IMeasurementGroupService _measurementGroupService;

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

        public ToggleMeasurementListingViewModel ToggleMeasurementListingViewModel { get; }

        public ICommand ToggleLockXAxisCommand { get; }
        public ICommand ToggleLockYAxisCommand { get; }

        private IEnumerable<MeasurementGroup>? _measurementGroups = null;

        public MeasurementHistoryChartViewModel()
        {
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _measurementGroupService = GetRequiredService<IMeasurementGroupService>();

            ToggleMeasurementListingViewModel = new ToggleMeasurementListingViewModel();
            ToggleMeasurementListingViewModel.OnChecked += _OnChecked;

            ToggleLockXAxisCommand = ReactiveCommand.Create(() => LockXAxis = !LockXAxis);
            ToggleLockYAxisCommand = ReactiveCommand.Create(() => LockYAxis = !LockYAxis);
        }

        public override void Dispose()
        {
            ToggleMeasurementListingViewModel.OnChecked -= _OnChecked;

            ToggleMeasurementListingViewModel.Dispose();
            base.Dispose();
        }

        public void Update(Meter? meter, MeasurementGroupArchive? archive, Func<Measurement, double>? valueSelector = null)
        {
            if (meter is not null && archive is not null)
            {
                ToggleMeasurementListingViewModel.Update(meter, valueSelector);

                var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
                _measurementGroups = _measurementGroupService.GetAll(x => x.MeasurementGroupArchiveId.Equals(archive.Id));
                CreateSeries(measurementTypes, valueSelector);
                PlotProvider?.AddCrosshair();
            }
        }

        public void UpdateNearestMeasurementGroup(int nearestIndex)
        {
            if (_measurementGroups is not null && _measurementGroups.Any())
            {
                var measurementGroup = _measurementGroups.ElementAt(nearestIndex);
                ToggleMeasurementListingViewModel.UpdateMeasurementGroup(measurementGroup);
            }
        }

        private void CreateSeries(IEnumerable<MeasurementType> measurementTypes, Func<Measurement, double>? valueSelector = null)
        {
            if (PlotProvider is not null)
            {
                PlotProvider.ClearPlots();

                if (_measurementGroups is not null && _measurementGroups.Any())
                {
                    var xs = _measurementGroups.Select(x => x.DateTime.ToOADate()).ToArray();

                    foreach (var measurementType in measurementTypes)
                    {
                        var measurements = _measurementGroups?
                            .Select(x => x.Measurements?.First(y => y.MeasurementTypeId.Equals(measurementType.Id)))
                            .OfType<Measurement>() ?? Enumerable.Empty<Measurement>();

                        var ys = measurements.Select(x => valueSelector?.Invoke(x) ?? 0d).ToArray();
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
