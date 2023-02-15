using LabPrototype.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace LabPrototype.EntityFramework.Dtos
{
    public class MeterDto
    {
        [Key] 
        public Guid Id { get; set; }
        public string SerialCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public IEnumerable<MeasurementDto> Measurements { get; set; }
    }
}
