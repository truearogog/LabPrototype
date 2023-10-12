using LabPrototype.Modbus;
using System.Text;

namespace LabPrototype.ModbusTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                try
                {
                    var protocol = new ModbusProtocolTcp("192.168.1.22", 5000);
                    var bytes = await protocol.ReadRegistersBytesAsync(1, 0, 32);

                    if (bytes is not null)
                    {
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
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }).Wait();
        }
    }
}