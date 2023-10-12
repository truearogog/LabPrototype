namespace LabPrototype.Modbus
{
    public interface IModbusProtocol
    {
        Task<ushort[]?> ReadRegistersAsync(byte slaveId, ushort start, ushort count);
        Task<byte[]?> ReadRegistersBytesAsync(byte slaveId, ushort start, ushort count);
    }
}
