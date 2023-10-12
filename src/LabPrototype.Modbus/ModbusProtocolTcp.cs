using System.Net;
using System.Net.Sockets;

namespace LabPrototype.Modbus
{
    public class ModbusProtocolTcp : ModbusProtocolBase
    {
        private readonly IPAddress _ipAddress;
        private readonly int _port;
        private Socket? _socket;

        public ModbusProtocolTcp(string ipAddress, int port)
        {
            _ipAddress = IPAddress.Parse(ipAddress);
            _port = port;
        }

        protected override void Open()
        {
            var endPoint = new IPEndPoint(_ipAddress, _port);
            _socket = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _socket!.Connect(endPoint);
            }
            catch (Exception)
            {
                Close();
            }
        }

        protected override void Close()
        {
            _socket?.Shutdown(SocketShutdown.Both);
            _socket?.Close();
        }

        protected override void Send(byte[] message)
        {
            _socket?.Send(message);
        }

        protected override byte[]? Receive(int count)
        {
            if (_socket is null)
            {
                return null;
            }

            var size = count * 2 + 5;
            var recv = new byte[size];
            var totalRecv = 0;
            do
            {
                totalRecv += _socket.Receive(recv, totalRecv, size - totalRecv, SocketFlags.None);
            } while (totalRecv < size);

            return recv;
        }
    }
}
