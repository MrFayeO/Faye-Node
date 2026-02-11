using System.Buffers;

public class NetMsg
{
    private readonly byte[] _MemoryNetMsg;
    private readonly IBitcoinPayload _Payload;
    private readonly PacketHeader _Header;

    public byte[] MemoryNetMsg => _MemoryNetMsg;
    public IBitcoinPayload Payload => _Payload;
    public PacketHeader Header => _Header;


    public NetMsg(string command)
    {
        _MemoryNetMsg = new byte[Constants.HEADER_SIZE];
        _Header = PacketHeader.Create(command, []);
        ArrayBufferWriter<byte> writer = new();
        _Header.ToBytes(ref writer);
        writer.WrittenSpan.CopyTo(_MemoryNetMsg);
    }

    public NetMsg(IBitcoinPayload payload, string command)
    {
        _Payload = payload;

        var headerOffset = Constants.HEADER_SIZE;
        ArrayBufferWriter<byte> writer = new();
        payload.Serialize(ref writer);
        var payloadLength = writer.WrittenCount;

        _MemoryNetMsg = new byte[payloadLength + Constants.HEADER_SIZE];

        writer.WrittenSpan.CopyTo(_MemoryNetMsg.AsSpan(headerOffset, payloadLength));

        writer.Clear();

        _Header = PacketHeader.Create(command, _MemoryNetMsg.AsSpan(headerOffset, payloadLength));
        _Header.ToBytes(ref writer);
        writer.WrittenSpan.CopyTo(_MemoryNetMsg);
    }

}