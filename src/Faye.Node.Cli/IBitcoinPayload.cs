using System.Buffers;

public interface IBitcoinPayload
{
    public void Serialize(ref ArrayBufferWriter<byte> writer);
    public abstract static IBitcoinPayload Deserialize(byte[] data);

}