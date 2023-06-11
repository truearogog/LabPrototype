﻿using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class ColorSchemeService : ServiceBase<ColorSchemeEntity, ColorScheme>, IColorSchemeService
    {
        public ColorSchemeService(IMapper mapper, IColorSchemeRepository repository) : base(mapper, repository)
        {
        }
    }
}