using AutoMapper;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;

namespace LabPrototype.Models.Profiles
{
    public class MeasurementGroupSchemaMeasurementTypeForm_Profile : Profile
    {
        public MeasurementGroupSchemaMeasurementTypeForm_Profile()
        {
            CreateMap<MeasurementGroupSchemaMeasurementType, MeasurementGroupSchemaMeasurementTypeForm>();
            CreateMap<MeasurementGroupSchemaMeasurementTypeForm, MeasurementGroupSchemaMeasurementType>();
        }
    }
}
