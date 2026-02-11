using System.Net;

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
        var stream = new ByteStreamReader(data);

        var services = stream.ReadU32LE();
        stream.SkipBytes(12);
        var ip = stream.ReadU32BE();
        var port = stream.ReadI16LE();

        return new NetAddr
        (
            services: services,
            ip: ip,
            port: port

        );
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

    public override string ToString()
    {

        return $"Services: {_Services}, Ip: {new IPAddress(_Ip)}, Port: {_Port}";
    }


}