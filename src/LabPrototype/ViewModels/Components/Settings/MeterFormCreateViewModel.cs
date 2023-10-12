using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.ViewModels.Components.Settings
{
    public class MeterFormCreateViewModel : MeterFormBaseViewModel
    {
        private readonly IMeterService _meterService;
        private readonly IArchiveService _archiveService;
        private readonly IArchiveStore _archiveStore;

        public MeterFormCreateViewModel() : base()
        {
            _meterService = GetRequiredService<IMeterService>();
            _archiveService = GetRequiredService<IArchiveService>();
            _archiveStore = GetRequiredService<IArchiveStore>();
        }

        public bool Create()
        {
            var createdMeter = Submit((store, model) => store.Create(_meterService, model));
            if (createdMeter is not null )
            {
                var archives = new[]
                {
                    new Archive
                    {
                        Name = "10 minutes",
                        DiscretizationMinutes = 10,
                        MinDiscretizationMinutes = 1,
                        MaxDescretizationMinutes = 10,
                        DiscretizationMonths = 0,
                        Order = 1,
                        IsActive = true,
                        IsEditable = true,
                        MeterId = createdMeter.Id,
                    },
                    new Archive
                    {
                        Name = "60 minutes",
                        DiscretizationMinutes = 60,
                        MinDiscretizationMinutes = 10,
                        MaxDescretizationMinutes = 60,
                        DiscretizationMonths = 0,
                        Order = 2,
                        IsActive = true,
                        IsEditable = true,
                        MeterId = createdMeter.Id,
                    },
                    new Archive
                    {
                        Name = "24 hours",
                        DiscretizationMinutes = 1440,
                        MinDiscretizationMinutes = 0,
                        MaxDescretizationMinutes = 0,
                        DiscretizationMonths = 0,
                        Order = 3,
                        IsActive = true,
                        IsEditable = false,
                        MeterId = createdMeter.Id,
                    },
                    new Archive
                    {
                        Name = "30 days",
                        DiscretizationMinutes = 0,
                        MinDiscretizationMinutes = 0,
                        MaxDescretizationMinutes = 0,
                        DiscretizationMonths = 1,
                        Order = 4,
                        IsActive = true,
                        IsEditable = false,
                        MeterId = createdMeter.Id,
                    },
                };

                foreach (var archive in archives)
                {
                    _archiveStore.Create(_archiveService, archive);
                }

                return true;
            }
            return false;
        }
    }
}
