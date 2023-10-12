using AutoMapper;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeterService : 
        ServiceBase<MeterEntity, Meter, IMeterRepository>, 
        IMeterService
    {
        public MeterService(IMapper mapper, IMeterRepository repository) : base(mapper, repository)
        {
        }

        public IEnumerable<MeasurementType> GetMeasurementTypes(int id)
        {
            var entities = Repository.GetMany(id, x => x.MeasurementTypes);
            return Mapper.Map<IEnumerable<MeasurementType>>(entities);
        }
    }
}
