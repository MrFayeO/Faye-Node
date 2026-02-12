using System.Buffers;

public readonly record struct VersionMsg : IBitcoinPayload
{
    readonly public int Version { get; init; }
    readonly public ulong Services { get; init; }
    readonly public long Timestamp { get; init; }
    readonly public NetAddr AddrRecv { get; init; }
    readonly public NetAddr AddrFrom { get; init; }
    readonly public ulong Nonce { get; init; }
    readonly public string UserAgent { get; init; }

    public VersionMsg(ulong services)
    {
        Version = Constants.VERSION;
        Services = services;
        Timestamp = Utils.GetUnixTimeInSeconds();
        AddrRecv = new();
        AddrFrom = new();
        Nonce = Utils.GenerateNonce();
        UserAgent = Constants.USER_AGENT;

    }

    public void Serialize(ref ArrayBufferWriter<byte> writer)
    {
        ByteStreamWriter stream = new(writer);
        stream.WriteI32LE(Version);
        stream.WriteU64LE(Services);
        stream.WriteI64LE(Timestamp);
        stream.WriteNetAddrNoTime(AddrRecv);
        stream.WriteNetAddrNoTime(AddrFrom);
        stream.WriteU64LE(Nonce);
        stream.WriteVarString(UserAgent);
    }

    public static IBitcoinPayload Deserialize(byte[] data)
    {
        ByteStreamReader streamReader = new(data);

        return new VersionMsg
        {
            Version = streamReader.ReadI32LE(),
            Services = streamReader.ReadU64LE(),
            Timestamp = streamReader.ReadI64LE(),
            AddrRecv = streamReader.ReadNetAddrWithoutTime(),
            AddrFrom = streamReader.ReadNetAddrWithoutTime(),
            Nonce = streamReader.ReadU64LE(),
            UserAgent = streamReader.ReadVarString(),

        };
    }

    public override string ToString()
    {
        return $"Version: {Version}, Services: {Services}, Timestamp: {Timestamp}, Nonce: {Nonce:X} agent: {UserAgent} \nRecv: {AddrRecv} \nFrom: {AddrFrom}";
    }
}

public readonly record struct PingPongMsg : IBitcoinPayload
{
    public readonly ulong Nonce { get; init; }

    public PingPongMsg()
    {
        Nonce = Utils.GenerateNonce();
    }

    public PingPongMsg(ulong nonce)
    {
        Nonce = nonce;
    }
    public static IBitcoinPayload Deserialize(byte[] data)
    {
        var streamReader = new ByteStreamReader(data);
        return new PingPongMsg
        {
            Nonce = streamReader.ReadU64LE()
        };
    }

    public void Serialize(ref ArrayBufferWriter<byte> writer)
    {
        ByteStreamWriter stream = new(writer);
        stream.WriteU64LE(Nonce);
    }
}