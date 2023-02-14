using Serilog.Events;

namespace LabPrototype.Configuration
{
    public class LoggingConfiguration
    {
        public string LogFileName { get; set; }
        public long LimitBytes { get; set; }
        public LogEventLevel DefaultLogLevel { get; set; }
        public LogEventLevel MicrosoftLogLevel { get; set; }
    }
}
