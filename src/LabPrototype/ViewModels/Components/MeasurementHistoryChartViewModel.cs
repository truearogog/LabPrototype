using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Extensions;
using LabPrototype.Models;
using LabPrototype.Providers.IntegrationCacheProvider;
using LabPrototype.Providers.PlotProvider;
using ReactiveUI;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryChartViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeterService _meterService;
        private readonly IMeasurementGroupRepository _measurementGroupRepository;
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

            _meterService = GetRequiredService<IMeterService>();
            _measurementGroupRepository = GetRequiredService<IMeasurementGroupRepository>();
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

        public void Update(Meter? meter, Archive? archive, MeasurementDisplayMode displayMode)
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

        private void CreateSeries(Meter meter, Archive archive, MeasurementDisplayMode displayMode)
        {
            if (PlotProvider is not null)
            {
                PlotProvider.ClearPlots();

                if (_measurementGroups is not null && _measurementGroups.Any())
                {
                    var _xs = _measurementGroups.Select(x => x.DateTime.ToOADate()).ToArray();
                    var measurementTypes = _meterService.GetMeasurementTypes(meter.Id);
                    var measurementTypeIndex = 0;
                    foreach (var measurementType in measurementTypes)
                    {
                        var xsys = _measurementCacheProvider.GetMeasurements(meter.Id, archive.Id, displayMode, measurementType.Id);
                        var color = measurementType.PrimaryColor?.ToColor() ?? Color.White;
                        if (xsys == default)
                        {
                            var ys = new double[_xs.Length];
                            var i = 0;
                            foreach (var measurementGroup in _measurementGroups ?? Enumerable.Empty<MeasurementGroupEntity>())
                            {
                                var group = displayMode.ValueSelector?.Invoke(measurementGroup);
                                ys[i++] = group?.ElementAt(measurementTypeIndex) ?? 0;
                            }

                            _measurementCacheProvider.AddMeasurements(meter.Id, archive.Id, displayMode, measurementType.Id, _xs, ys);
                            PlotProvider.AddPlot(measurementType.Id, _xs, ys, color);
                        }
                        else
                        {
                            var (xs, ys) = xsys;
                            PlotProvider.AddPlot(measurementType.Id, xs.ToArray(), ys.ToArray(), color);
                        }

                        ++measurementTypeIndex;
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
