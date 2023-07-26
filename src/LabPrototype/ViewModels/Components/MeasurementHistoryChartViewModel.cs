using System.Linq;
using System.Windows.Input;
using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System.Collections.Generic;
using LabPrototype.Domain.IServices;
using LabPrototype.Providers.PlotProvider;
using System;
using LabPrototype.Extensions;
using System.Drawing;
using static LabPrototype.ViewModels.Components.MeasurementHistoryTableViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryChartViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeterTypeService _meterTypeService;
        private readonly IMeasurementGroupService _measurementGroupService;
        private readonly IMeasurementGroupSchemaService _measurementGroupSchemaService;

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
            _measurementGroupSchemaService = GetRequiredService<IMeasurementGroupSchemaService>();

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

        public void Update(Meter meter, MeasurementGroupArchive archive, Func<MeasurementGroup, IEnumerable<double>>? valueSelector = null)
        {
            if (meter is not null && archive is not null)
            {
                ToggleMeasurementListingViewModel.Update(meter, valueSelector);
                _measurementGroups = _measurementGroupService.GetAll(x => x.MeasurementGroupArchiveId.Equals(archive.Id));
                CreateSeries(meter, valueSelector);
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

        private void CreateSeries(Meter meter, Func<MeasurementGroup, IEnumerable<double>>? groupSelector = null)
        {
            if (PlotProvider is not null)
            {
                PlotProvider.ClearPlots();

                if (_measurementGroups is not null && _measurementGroups.Any())
                {
                    // create dictionary that contains schema id -> measurement type -> array index
                    var schemas = _measurementGroupSchemaService.GetAll(x => x.MeterTypeId.Equals(meter.MeterTypeId));
                    var schemaTypeIndexes = new Dictionary<int, Dictionary<int, int>>();
                    foreach (var schema in schemas)
                    {
                        var typeIndexes = new Dictionary<int, int>();
                        var measurements = _measurementGroupSchemaService.GetMeasurementTypes(schema.Id);
                        var i = 0;
                        foreach (var measurement in measurements)
                        {
                            typeIndexes.Add(measurement.Id, i++);
                        }
                        schemaTypeIndexes.Add(schema.Id, typeIndexes);
                    }

                    var xs = _measurementGroups.Select(x => x.DateTime.ToOADate()).ToArray();
                    var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
                    foreach (var measurementType in measurementTypes)
                    {
                        var ys = new double[xs.Length];
                        var i = 0;
                        foreach (var measurementGroup in _measurementGroups ?? Enumerable.Empty<MeasurementGroup>())
                        {
                            if (schemaTypeIndexes[measurementGroup.MeasurementGroupSchemaId].TryGetValue(measurementType.Id, out var index))
                            {
                                var group = groupSelector?.Invoke(measurementGroup);
                                ys[i++] = group?.ElementAt(index) ?? 0;
                            }
                            else
                            {
                                ys[i++] = 0;
                            }
                        }
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
