using Avalonia.Controls;
using Avalonia.Data;
using LabPrototype.Domain.Models;
using LabPrototype.ViewModels.Components;
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

    private void UpdateTable(Meter meter)
    {
        TableControl.Columns.Clear();

        var measurementAttributes = meter.MeasurementAttributes.Prepend(new MeasurementAttribute(null, null, null, "DateTime", null));

        foreach (var measurementAttribute in measurementAttributes)
        {
            var column = new DataGridTextColumn()
            {
                Binding = new Binding(measurementAttribute.BindingName),
            };
            TableControl.Columns.Add(column);
        }
    }
}