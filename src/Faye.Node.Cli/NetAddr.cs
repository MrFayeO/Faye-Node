public class NetAddr
{
    private readonly uint _Time;
    private readonly long _Services;
    private readonly uint _Ip;
    private readonly short _Port;

    public uint Time => _Time;
    public long Services => _Services;
    public uint Ip => _Ip;
    public short Port => _Port;
    public NetAddr() : base()
    {

    }

    public NetAddr(long services, uint ip, short port)
    {
        _Services = services;
        _Ip = ip;
        _Port = port;
    }

    public NetAddr(uint time, long services, uint ip, short port)
    {
        _Time = time;
        _Services = services;
        _Ip = ip;
        _Port = port;
    }

    public static NetAddr ParseWithoutTime(byte[] data)
    {
        int cnt = 0;
        var dataSpan = data.AsSpan();
        // var time = Utils.ReadU32LE(dataSpan, ref cnt);
        var services = Utils.ReadI64LE(dataSpan[cnt..(cnt + 8)], ref cnt);
        cnt += 12; // Skip to IPv4
        var ip = Utils.ReadU32LE(dataSpan[cnt..(cnt + 4)], ref cnt);
        var port = Utils.ReadI16LE(dataSpan[cnt..(cnt + 2)], ref cnt);

        return new NetAddr(services, ip, port);
    }

    public static NetAddr ParseWithTime(byte[] data)
    {
        throw new NotImplementedException();
    }

    public void SerializeWithoutTime(Span<byte> output, ref int cnt)
    {
        output.Clear();
        cnt += 26;
    }

    public void SerializeWithTime(Span<byte> output, ref int cnt)
    {
        throw new NotImplementedException();
    }


}