using Avalonia.Controls;
using Avalonia.Data;
using LabPrototype.Domain.Models;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components;
using System.Collections.Generic;
using System.Linq;

namespace LabPrototype.Views.Components;

public partial class MeasurementHistoryTable : UserControl
{
    private MeasurementHistoryTableViewModel? _vm;

    public MeasurementHistoryTable()
    {
        InitializeComponent();

        DataContextChanged += (s, e) =>
        {
            _vm = DataContext as MeasurementHistoryTableViewModel;
            if (_vm != null)
            {
                _vm.UpdateViewCalled += UpdateTable;
            }
        };

        DetachedFromVisualTree += (s, e) =>
        {
            if (_vm != null)
            {
                _vm.UpdateViewCalled -= UpdateTable;
            }
        };
    }

    private void UpdateTable(IEnumerable<MeasurementType> measurementTypes, IEnumerable<int> measurementTypeIds)
    {
        TableControl.Columns.Clear();

        int i = 0;
        foreach (var measurementTypeId in measurementTypeIds)
        {
            var measurementType = measurementTypes.First(x => x.Id.Equals(measurementTypeId));
            var column = new DataGridTextColumn()
            {
                Binding = new Binding($"{nameof(MeasurementGroup.Measurements)}[{i++}]"),
            };
            TableControl.Columns.Add(column);
        }
    }
}