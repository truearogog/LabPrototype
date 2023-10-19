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
                    var protocol = new ModbusProtocolTcp("185.147.58.54", 5000);
                    var bytes = await protocol.ReadRegistersBytesAsync(1, 512, 16);

                    if (bytes is not null)
                    {
                        Console.WriteLine(Encoding.UTF8.GetString(bytes.ToArray()));
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