using System.Buffers;

public interface IBitcoinMessage
{
    void Serialize(ref ArrayBufferWriter<byte> writer, int protocolVersion);
    abstract static IBitcoinMessage Parse(byte[] data);
}