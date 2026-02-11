using System.Buffers;

public readonly record struct VersionMsg : IBitcoinPayload
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
        _Timestamp = Utils.GetUnixTimeInSeconds();
        _AddrRecv = new();
        _AddrFrom = new();
        _Nonce = Utils.GenerateNonce();
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

    public static IBitcoinPayload Deserialize(byte[] data)
    {
        ByteStreamReader streamReader = new(data);

        return new VersionMsg
        {
            _Version = streamReader.ReadI32LE(),
            _Services = streamReader.ReadU64LE(),
            _Timestamp = streamReader.ReadI64LE(),
            _AddrRecv = streamReader.ReadNetAddrWithoutTime(),
            _AddrFrom = streamReader.ReadNetAddrWithoutTime(),
            _Nonce = streamReader.ReadU64LE(),
            _UserAgent = streamReader.ReadVarString(),

        };
    }



    public override string ToString()
    {
        return $"Version: {_Version}, Services: {_Services}, Timestamp: {_Timestamp}, Nonce: {_Nonce:X} agent: {_UserAgent} \nRecv: {_AddrRecv} \nFrom: {_AddrFrom}";
    }
}