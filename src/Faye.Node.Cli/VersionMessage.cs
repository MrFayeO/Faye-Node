using System.Buffers;
using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

sealed class VersionMessage : IBitcoinMessage
{
    readonly private int _Version;
    readonly private ulong _Services;
    readonly private long _Timestamp;
    readonly private NetAddr _AddrRecv;
    readonly private NetAddr _AddrFrom;
    readonly private ulong _Nonce;
    readonly private string _UserAgent;

    private const byte NO_RELAY = 0;

    public int Version => _Version;
    public ulong Services => _Services;
    public long Timestamp => _Timestamp;
    public NetAddr AddrRecv => _AddrRecv;
    public NetAddr AddrFrom => AddrFrom;
    public ulong Nonce => _Nonce;
    public string UserAgent => _UserAgent;



    public VersionMessage(ulong services)
    {
        _Version = Constants.VERSION;
        _Services = services;
        _Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        _AddrRecv = new();
        _AddrFrom = new();
        _Nonce = BinaryPrimitives.ReadUInt64LittleEndian(RandomNumberGenerator.GetBytes(8));
        _UserAgent = Constants.USER_AGENT;

    }

    public VersionMessage(int version, ulong services, long timestamp, NetAddr addrRecv, NetAddr addrFrom, ulong nonce, string userAgent)
    {
        _Version = version;
        _Services = services;
        _Timestamp = timestamp;
        _AddrRecv = addrRecv;
        _AddrFrom = addrFrom;
        _Nonce = nonce;
        _UserAgent = userAgent;
    }

    public string Command => throw new NotImplementedException();

    public static IBitcoinMessage Parse(byte[] data)
    {
        var dataSpan = data.AsSpan();
        int cnt = 0;


        var version = Utils.ReadI32LE(dataSpan, ref cnt);
        var services = Utils.ReadU64LE(dataSpan[cnt..(cnt + 8)], ref cnt);
        var timestamp = Utils.ReadI64LE(dataSpan[cnt..(cnt + 8)], ref cnt);

        var addrRecv = NetAddr.ParseWithoutTime(data[cnt..(cnt + 26)]);
        cnt += 26;
        var addrFrom = NetAddr.ParseWithoutTime(data[cnt..(cnt + 26)]);
        cnt += 26;

        var nonce = Utils.ReadU64LE(dataSpan[cnt..(cnt + 8)], ref cnt);
        int stringLength = (int)Utils.VarIntDecode(dataSpan[cnt..(cnt + 9)], ref cnt);
        cnt++; // account for format
        var userAgent = Encoding.ASCII.GetString(dataSpan[cnt..(cnt + stringLength)]);
        cnt += stringLength;

        return new VersionMessage(version, services, timestamp, addrRecv, addrFrom, nonce, userAgent);
    }

    public void Serialize(ref ArrayBufferWriter<byte> writer, int protocolVersion)
    {
        var span = writer.GetSpan(255); // len should be based on User Agents length
        int cnt = 0;
        Utils.I32LE(span, ref cnt, _Version);
        Utils.U64LE(span[cnt..(cnt + 8)], ref cnt, _Services);
        Utils.I64LE(span[cnt..(cnt + 8)], ref cnt, _Timestamp);
        _AddrRecv.SerializeWithoutTime(span[cnt..(cnt + 26)], ref cnt);
        _AddrFrom.SerializeWithoutTime(span[cnt..(cnt + 26)], ref cnt);
        Utils.U64LE(span[cnt..(cnt + 8)], ref cnt, _Nonce);
        cnt += Utils.VarIntEncoding((ulong)_UserAgent.Length, span[cnt..(cnt + 9)]);
        cnt += Encoding.ASCII.GetBytes(_UserAgent, span[cnt..(cnt + _UserAgent.Length)]);
        Utils.I32LE(span[cnt..(cnt + 4)], ref cnt, 0);
        span[cnt++] = NO_RELAY;
        writer.Advance(cnt);
    }

    public override string ToString()
    {
        return $"Version: {_Version}, Services: {_Services}, Timestamp: {_Timestamp}, Nonce: {_Nonce:X} agent: {_UserAgent}";
    }
}