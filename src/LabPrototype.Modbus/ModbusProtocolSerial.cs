using System.IO.Ports;

namespace LabPrototype.Modbus
{
    public class ModbusProtocolSerial : ModbusProtocolBase
    {
        private SerialPort? _serialPort;
        private readonly string _portName;
        private readonly int _baudRate;
        private readonly Parity _parity;
        private readonly int _dataBits;
        private readonly StopBits _stopBits;

        public ModbusProtocolSerial(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            _portName = portName;
            _baudRate = baudRate;
            _parity = parity;
            _dataBits = dataBits;
            _stopBits = stopBits;
        }

        protected override void Open()
        {
            _serialPort = new()
            {
                PortName = _portName,
                BaudRate = _baudRate,
                Parity = _parity,
                DataBits = _dataBits,
                StopBits = _stopBits
            };
            _serialPort.Open();
        }

        protected override void Close()
        {
            _serialPort?.Close();
        }

        protected override void Send(byte[] message)
        {
            _serialPort?.Write(message, 0, message.Length);
        }

        protected override byte[]? Receive(int count)
        {
            if (_serialPort is null)
            {
                return null;
            }

            var size = count * 2 + 5;
            var recv = new byte[size];
            for (int i = 0; i < size; ++i)
            {
                recv[i] = (byte)_serialPort.ReadByte();
            }

            return recv;
        }
    }
}
