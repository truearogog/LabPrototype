using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Domain.IServices;
using LabPrototype.Providers.PlotProvider;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Extensions;
using ReactiveUI;
using LabPrototype.Providers.IntegrationCacheProvider;
using LabPrototype.Models;
using System.Drawing;
using System.Threading.Tasks;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryChartViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeterTypeService _meterTypeService;
        private readonly IMeasurementGroupRepository _measurementGroupRepository;
        private readonly IMeasurementGroupSchemaService _measurementGroupSchemaService;
        private readonly IMeasurementCacheProvider _measurementCacheProvider;

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
        private IntegralMeasurementListingViewModel _integralMeasurementListingViewModel;

        public ICommand ToggleLockXAxisCommand { get; }
        public ICommand ToggleLockYAxisCommand { get; }

        private IEnumerable<MeasurementGroupEntity>? _measurementGroups = null;

        public MeasurementHistoryChartViewModel(IntegralMeasurementListingViewModel integralMeasurementListingViewModel)
        {
            _integralMeasurementListingViewModel = integralMeasurementListingViewModel;

            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _measurementGroupRepository = GetRequiredService<IMeasurementGroupRepository>();
            _measurementGroupSchemaService = GetRequiredService<IMeasurementGroupSchemaService>();
            _measurementCacheProvider = GetRequiredService<IMeasurementCacheProvider>();

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

        public void Update(Meter? meter, MeasurementGroupArchive? archive, MeasurementDisplayMode displayMode)
        {
            if (meter is not null && archive is not null)
            {
                ToggleMeasurementListingViewModel.Update(meter, displayMode);
                _measurementGroups = _measurementGroupRepository.GetAll().Where(x => x.MeasurementGroupArchiveId.Equals(archive.Id)).ToList();
                CreateSeries(meter, archive, displayMode);
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

        public void UpdateSelectionEdges(double? selectionMin, double? selectionMax)
        {
            _integralMeasurementListingViewModel.UpdateSelectionEdges(selectionMin, selectionMax);
            Task.Run(_integralMeasurementListingViewModel.UpdateSelectionIntegration);
        }

        private void CreateSeries(Meter meter, MeasurementGroupArchive archive, MeasurementDisplayMode displayMode)
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

                    var _xs = _measurementGroups.Select(x => x.DateTime.ToOADate()).ToArray();
                    var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
                    foreach (var measurementType in measurementTypes)
                    {
                        var xsys = _measurementCacheProvider.GetMeasurements(meter.Id, archive.Id, displayMode, measurementType.Id);
                        var color = measurementType.ColorScheme?.PrimaryColor?.ToColor() ?? Color.White;
                        if (xsys == default)
                        {
                            var ys = new double[_xs.Length];
                            var i = 0;
                            foreach (var measurementGroup in _measurementGroups ?? Enumerable.Empty<MeasurementGroupEntity>())
                            {
                                if (schemaTypeIndexes[measurementGroup.MeasurementGroupSchemaId].TryGetValue(measurementType.Id, out var index))
                                {
                                    var group = displayMode.ValueSelector?.Invoke(measurementGroup);
                                    ys[i++] = group?.ElementAt(index) ?? 0;
                                }
                                else
                                {
                                    ys[i++] = 0;
                                }
                            }

                            _measurementCacheProvider.AddMeasurements(meter.Id, archive.Id, displayMode, measurementType.Id, _xs, ys);
                            PlotProvider.AddPlot(measurementType.Id, _xs, ys, color);
                        }
                        else
                        {
                            var (xs, ys) = xsys;
                            PlotProvider.AddPlot(measurementType.Id, xs.ToArray(), ys.ToArray(), color);
                        }
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
