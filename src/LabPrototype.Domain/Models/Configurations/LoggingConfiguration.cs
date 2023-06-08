using Serilog.Events;

namespace LabPrototype.Domain.Models.Configurations
{
    public class LoggingConfiguration : ConfigurationBase
    {
        public string? LogFileName { get; set; }
        public long LimitBytes { get; set; }
        public LogEventLevel DefaultLogLevel { get; set; }
        public LogEventLevel MicrosoftLogLevel { get; set; }
    }
}
