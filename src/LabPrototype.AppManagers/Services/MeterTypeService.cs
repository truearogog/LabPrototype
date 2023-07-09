using AutoMapper;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeterTypeService : ServiceBase<MeterTypeEntity, MeterType, IMeterTypeRepository>, IMeterTypeService
    {
        public MeterTypeService(IMapper mapper, IMeterTypeRepository repository) : base(mapper, repository)
        {
        }

        public IEnumerable<MeasurementType> GetMeasurementTypes(int id)
        {
            var entities = Repository.GetMeasurementTypes(id);
            return Mapper.Map<IEnumerable<MeasurementType>>(entities);
        }
    }
}
