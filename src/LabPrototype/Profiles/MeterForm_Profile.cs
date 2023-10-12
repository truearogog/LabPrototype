using AutoMapper;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Forms;

namespace LabPrototype.Models.Profiles
{
    public class MeterForm_Profile : Profile
    {
        public MeterForm_Profile()
        {
            CreateMap<Meter, MeterFormViewModel>()
                .ForPath(dst => dst.InformationForm.Name, opts => opts.MapFrom(src => src.Name))
                .ForPath(dst => dst.InformationForm.Address, opts => opts.MapFrom(src => src.Address))
                .ForPath(dst => dst.InformationForm.SerialCode, opts => opts.MapFrom(src => src.SerialCode))
                .ForPath(dst => dst.InformationForm.MeterNr, opts => opts.MapFrom(src => src.MeterNr))
                .ForPath(dst => dst.SerialConnectionForm.PortName, opts => opts.MapFrom(src => src.PortName))
                .ForPath(dst => dst.SerialConnectionForm.BaudRate, opts => opts.MapFrom(src => src.BaudRate))
                .ForPath(dst => dst.SerialConnectionForm.Parity, opts => opts.MapFrom(src => src.Parity))
                .ForPath(dst => dst.SerialConnectionForm.DataBits, opts => opts.MapFrom(src => src.DataBits))
                .ForPath(dst => dst.SerialConnectionForm.StopBits, opts => opts.MapFrom(src => src.StopBits))
                .ForPath(dst => dst.InternetConnectionForm.IpAddress, opts => opts.MapFrom(src => src.IpAddress))
                .ForPath(dst => dst.InternetConnectionForm.Port, opts => opts.MapFrom(src => src.Port));
            CreateMap<MeterFormViewModel, Meter>()
                .ForMember(dst => dst.Name, opts => opts.MapFrom(src => src.InformationForm.Name))
                .ForMember(dst => dst.Address, opts => opts.MapFrom(src => src.InformationForm.Address))
                .ForMember(dst => dst.SerialCode, opts => opts.MapFrom(src => src.InformationForm.SerialCode))
                .ForMember(dst => dst.MeterNr, opts => opts.MapFrom(src => src.InformationForm.MeterNr))
                .ForMember(dst => dst.PortName, opts => opts.MapFrom(src => src.SerialConnectionForm.PortName))
                .ForMember(dst => dst.BaudRate, opts => opts.MapFrom(src => src.SerialConnectionForm.BaudRate))
                .ForMember(dst => dst.Parity, opts => opts.MapFrom(src => src.SerialConnectionForm.Parity))
                .ForMember(dst => dst.DataBits, opts => opts.MapFrom(src => src.SerialConnectionForm.DataBits))
                .ForMember(dst => dst.StopBits, opts => opts.MapFrom(src => src.SerialConnectionForm.StopBits))
                .ForMember(dst => dst.IpAddress, opts => opts.MapFrom(src => src.InternetConnectionForm.IpAddress))
                .ForMember(dst => dst.Port, opts => opts.MapFrom(src => src.InternetConnectionForm.Port));
        }
    }
}
