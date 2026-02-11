using System.Buffers;
using System.Buffers.Binary;
using System.Security.Cryptography;

public readonly record struct VersionMsg
{
    readonly public int _Version { get; init; }
    readonly public ulong _Services { get; init; }
    readonly public long _Timestamp { get; init; }
    readonly public NetAddr _AddrRecv { get; init; }
    readonly public NetAddr _AddrFrom { get; init; }
    readonly public ulong _Nonce { get; init; }
    readonly public string _UserAgent { get; init; }

    public VersionMsg(ulong services)
    {
        _Version = Constants.VERSION;
        _Services = services;
        _Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        _AddrRecv = new();
        _AddrFrom = new();
        _Nonce = BinaryPrimitives.ReadUInt64LittleEndian(RandomNumberGenerator.GetBytes(8));
        _UserAgent = Constants.USER_AGENT;

    }

    public void Serialize(ref ArrayBufferWriter<byte> writer)
    {
        ByteStreamWriter stream = new(writer);
        stream.WriteI32LE(_Version);
        stream.WriteU64LE(_Services);
        stream.WriteI64LE(_Timestamp);
        stream.WriteNetAddrNoTime(_AddrRecv);
        stream.WriteNetAddrNoTime(_AddrFrom);
        stream.WriteU64LE(_Nonce);
        stream.WriteVarString(_UserAgent);
    }

    public override string ToString()
    {
        return $"Version: {_Version}, Services: {_Services}, Timestamp: {_Timestamp}, Nonce: {_Nonce:X} agent: {_UserAgent}";
    }
}