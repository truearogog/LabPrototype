using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Media;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components;
using System.Collections.Generic;

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

        DetachedFromLogicalTree += (s, e) =>
        {
            if (_vm != null)
            {
                _vm.UpdateViewCalled -= UpdateTable;
            }
        };
    }

    private void UpdateTable(IEnumerable<MeasurementType> measurementTypes)
    {
        TableControl.Columns.Clear();

        var headerTemplateFactory = (string color) => new FuncDataTemplate<string>((value, namescope) =>
        {
            var colorBrush = new SolidColorBrush(Color.Parse(color));
            return new TextBlock {
                FontSize = 20,
                [!TextBlock.TextProperty] = new Binding(".", BindingMode.OneWay),
                Foreground = colorBrush,
            };
        });

        // add date/time column
        TableControl.Columns.Add(new DataGridTextColumn
        {
            Header = "Date/time",
            CanUserSort = true,
            CanUserReorder = true,
            HeaderTemplate = headerTemplateFactory("#ffffff"),
            Binding = new Binding
            {
                Path = $"{nameof(MeasurementHistoryTableViewModel._MeasurementGroup.DateTime)}",
                StringFormat = "{0}",
                Mode = BindingMode.OneWay,
            },
            MinWidth = 150,
        });

        var i = 0;
        foreach (var measurementType in measurementTypes)
        {
            TableControl.Columns.Add(new DataGridTextColumn
            {
                Header = measurementType.Name,
                CanUserSort = true,
                CanUserReorder = true,
                HeaderTemplate = headerTemplateFactory(measurementType.ColorScheme?.PrimaryColor ?? "#ffffff"),
                Binding = new Binding
                {
                    Path = $"{nameof(MeasurementHistoryTableViewModel._MeasurementGroup.Values)}[{i++}]",
                    StringFormat = "{0}",
                    Mode = BindingMode.OneWay,
                },
                MinWidth = 150,
            });
        }
    }
}