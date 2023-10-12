using LabPrototype.Domain.IProviders;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Modbus;

namespace LabPrototype.AppManagers.Providers
{
    public class MeterConnectionProvider : IMeterConnectionProvider
    {
        private readonly IModbusProtocol? _modbusProtocol;

        public MeterConnectionProvider(Meter meter)
        {
            if (meter.UseInternetConnection)
            {
                _modbusProtocol = new ModbusProtocolTcp(meter.IpAddress, meter.Port);
            }
            else if (meter.UseSerialConnection)
            {
                _modbusProtocol = new ModbusProtocolSerial(meter.PortName, meter.BaudRate, meter.Parity, meter.DataBits, meter.StopBits);
            }
        }

        public IEnumerable<MeasurementType> GetMeasurementTypes()
        {
            throw new NotImplementedException();
        }
    }
}
