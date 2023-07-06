using NModbus;
using NModbus.Serial;
using System.IO.Ports;
using System.Text;

namespace LabPrototype.ModbusTest
{
    public class Program
    {

        static void Main(string[] args)
        {
            using var masterPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
            masterPort.Open();

            var factory = new ModbusFactory();
            var master = factory.CreateRtuMaster(masterPort);
            
            byte slaveID = 1;
            ushort startAddress = 0;
            ushort numOfPoints = 32;
            ushort[] holding_register = master.ReadHoldingRegisters(slaveID, startAddress, numOfPoints);
            var bytes = holding_register.Select(x => new byte[] { (byte)(x >> 8), (byte)x }).SelectMany(x => x).ToArray();

            var i = 0;
            foreach (var x in bytes)
            {
                Console.WriteLine($"{i++} {x}");
            }

            var nameBytes = bytes.Take(16);
            Console.WriteLine(string.Join(", ", nameBytes));
            Console.WriteLine(Encoding.UTF8.GetString(nameBytes.ToArray()));

            var sysCodeBytes = bytes.Skip(17).Take(1);
            Console.WriteLine(string.Join(", ", sysCodeBytes));

            var pwBytes = bytes.Skip(18).Take(1);
            Console.WriteLine(string.Join(", ", pwBytes));

            var commaBytes = bytes.Skip(20).Take(5);
            Console.WriteLine(string.Join(", ", commaBytes));

            var timeBytes = bytes.Skip(26).Take(6).ToArray();
            Console.WriteLine(string.Join(", ", timeBytes));
            var dateTime = new DateTime(2000 + timeBytes[5], timeBytes[4], timeBytes[3], timeBytes[2], timeBytes[1], timeBytes[0]);
            Console.WriteLine(dateTime);

            var flowValues = holding_register.Skip(16).Take(6).ToArray();
            Console.WriteLine(string.Join(", ", flowValues));

            //var s = Encoding.UTF8.GetString(bytes);

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}