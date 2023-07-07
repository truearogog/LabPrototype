﻿using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.Models.Forms;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class Meter_Profile : Profile
    {
        public Meter_Profile()
        {
            CreateMap<Meter, MeterEntity>();
            CreateMap<MeterEntity, Meter>();

            CreateMap<Meter, MeterForm>();
            CreateMap<MeterForm, Meter>();
        }
    }
}
