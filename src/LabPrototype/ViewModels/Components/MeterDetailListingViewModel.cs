using LabPrototype.Domain.Models.Presentation;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class MeterDetailListingViewModel : ViewModelBase
    {
        public ObservableCollection<MeterDetailListingItemViewModel> Items { get; } = new();

        public MeterDetailListingViewModel()
        {
            CreateDetails();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        private void CreateDetails()
        {
            Items.Clear();
            Items.Add(new MeterDetailListingItemViewModel("Name", x => x?.Name));
            Items.Add(new MeterDetailListingItemViewModel("Serial code", x => x?.SerialCode));
            Items.Add(new MeterDetailListingItemViewModel("Address", x => x?.Address));
        }

        public void UpdateMeter(Meter? meter)
        {
            if (meter is not null)
            {
                foreach (var detailViewModel in Items)
                {
                    detailViewModel.Update(meter);
                }
            }
        }
    }
}
