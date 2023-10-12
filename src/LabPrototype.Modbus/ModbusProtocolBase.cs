namespace LabPrototype.Modbus
{
    public abstract class ModbusProtocolBase : IModbusProtocol
    {
        public async Task<ushort[]?> ReadRegistersAsync(byte slaveId, ushort start, ushort count)
        {
            var bytes = await ReadRegistersBytesAsync(slaveId, start, count);
            if (bytes is null)
            {
                return null;
            }
            var registers = new ushort[count];
            for (int i = 0; i < count; i++)
            {
                registers[i] = bytes[2 * i + 3];
                registers[i] <<= 8;
                registers[i] += bytes[2 * i + 4];
            }
            return registers;
        }

        public async Task<byte[]?> ReadRegistersBytesAsync(byte slaveId, ushort start, ushort count)
        {
            return await Task.Run(() =>
            {
                Open();
                var message = new byte[8];
                BuildMessage(slaveId, 3, start, count, ref message);
                Send(message);
                var bytes = Receive(count);
                if (bytes is null)
                {
                    return null;
                }
                Close();
                if (CheckResponse(bytes))
                {
                    return bytes[3..^2];
                }
                else
                {
                    return null;
                }
            });
        }

        private static void BuildMessage(byte slaveId, byte type, ushort start, ushort count, ref byte[] message)
        {
            message[0] = slaveId;
            message[1] = type;
            message[2] = (byte)(start >> 8);
            message[3] = (byte)start;
            message[4] = (byte)(count >> 8);
            message[5] = (byte)count;

            var crc = GetCRC(message);
            message[^2] = crc[0];
            message[^1] = crc[1];
        }

        private static bool CheckResponse(byte[] response)
        {
            var crc = GetCRC(response);
            return crc[0] == response[^2] && crc[1] == response[^1];
        }

        private static byte[] GetCRC(byte[] message)
        {
            var crc = new byte[2];
            ushort crcFull = 0xFFFF;
            char CRCLSB;

            for (int i = 0; i < message.Length - 2; ++i)
            {
                crcFull = (ushort)(crcFull ^ message[i]);

                for (int j = 0; j < 8; ++j)
                {
                    CRCLSB = (char)(crcFull & 0x0001);
                    crcFull = (ushort)((crcFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                    {
                        crcFull = (ushort)(crcFull ^ 0xA001);
                    }
                }
            }
            crc[1] = (byte)((crcFull >> 8) & 0xFF);
            crc[0] = (byte)(crcFull & 0xFF);
            return crc;
        }

        protected abstract void Open();
        protected abstract void Close();
        protected abstract void Send(byte[] message);
        protected abstract byte[]? Receive(int count);
    }
}
