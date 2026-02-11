using System.Buffers;
using System.Buffers.Binary;
using System.Text;

public sealed class PacketHeader
{
    readonly uint _Magic;
    readonly string _Command;
    readonly uint _PayloadLength;
    readonly uint _Checksum;

    private const int CMD_LENGTH = 12;

    PacketHeader(uint magic, string command, uint payloadLength, uint checksum)
    {
        _Magic = magic;
        _Command = command;
        _PayloadLength = payloadLength;
        _Checksum = checksum;

    }

    public uint Magic => _Magic;
    public string Command => _Command;
    public uint PayloadLength => _PayloadLength;
    public uint Checksum => _Checksum;

    public void ToBytes(ref ArrayBufferWriter<byte> writer)
    {
        var stream = new ByteStreamWriter(writer);

        stream.WriteU32LE(_Magic);
        stream.WriteString(_Command, CMD_LENGTH);
        stream.WriteU32LE(_PayloadLength);
        stream.WriteU32LE(_Checksum);
    }

    public static PacketHeader Create(string command, ReadOnlySpan<byte> payload)
    {
        var checksum = Utils.CalculateChecksum(payload);
        return new PacketHeader(Constants.CHAIN, command, (uint)payload.Length, checksum);
    }

    public static PacketHeader Parse(byte[] h24)
    {
        var h24Span = h24.AsSpan();
        uint magic = BinaryPrimitives.ReadUInt32LittleEndian(h24Span[..4]);
        string command = Encoding.ASCII.GetString(h24Span[4..16]);
        uint payload = BinaryPrimitives.ReadUInt32LittleEndian(h24Span[16..20]);
        uint checksum = BinaryPrimitives.ReadUInt32LittleEndian(h24Span[20..24]);
        return new PacketHeader(magic, command, payload, checksum);

    }

    public override string ToString()
    {
        return $"Magic: {_Magic:X}, Command: {_Command}, PayloadLength: {_PayloadLength}, Checksum: {_Checksum:X}";
    }

}