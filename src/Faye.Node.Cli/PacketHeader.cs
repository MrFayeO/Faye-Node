using System.Buffers;
public sealed class PacketHeader
{
    readonly uint _Magic;
    readonly string _Command;
    readonly uint _PayloadLength;
    readonly uint _Checksum;

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
        stream.WriteString(_Command, Constants.CMD_LENGTH);
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
        var stream = new ByteStreamReader(h24);
        return new
        (
            magic: stream.ReadU32LE(),
            command: stream.ReadString(Constants.CMD_LENGTH),
            payloadLength: stream.ReadU32LE(),
            checksum: stream.ReadU32LE()
        );

    }

    public override string ToString()
    {
        return $"Magic: {_Magic:X}, Command: {_Command}, PayloadLength: {_PayloadLength}, Checksum: {_Checksum:X}";
    }

}