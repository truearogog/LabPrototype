using AutoMapper;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Forms;

namespace LabPrototype.Profiles
{
    public class MeasurementTypeForm_Profile : Profile
    {
        public MeasurementTypeForm_Profile()
        {
            CreateMap<MeasurementType, MeasurementTypeFormViewModel>();
            CreateMap<MeasurementTypeFormViewModel, MeasurementType>();
        }
    }
}
