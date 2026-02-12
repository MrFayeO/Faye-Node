using System.Threading.Channels;

public class Node
{
    readonly SockS5 _Socket;
    string _UserAgent;
    int _Version;
    bool _VerackRecv;
    readonly string _Addr; // Should be NetAddr in the future
    readonly ulong _NodeId;

    public SockS5 Socket => _Socket;
    public string UserAgent => _UserAgent;
    public int Version => _Version;
    public bool VerackRecv => _VerackRecv;
    public string Addr => _Addr;
    public ulong NodeId => _NodeId;

    private Channel<NetMsg> _Inbound = Channel.CreateUnbounded<NetMsg>();
    private Channel<NetMsg> _Outbound = Channel.CreateUnbounded<NetMsg>();


    public IAsyncEnumerable<NetMsg> InboundMessages()
        => _Inbound.Reader.ReadAllAsync();

    public IAsyncEnumerable<NetMsg> OutboundMessages()
        => _Outbound.Reader.ReadAllAsync();




    public Node(SockS5 socket, string addr, ulong nodeId)
    {
        _Socket = socket;
        _Addr = addr;
        _NodeId = nodeId;
        _VerackRecv = false;
        _UserAgent = "Undetermined";
    }

    public bool AddInboundMsg(NetMsg msg)
    {
        return _Inbound.Writer.TryWrite(msg);
    }

    public bool AddOutboundMsg(NetMsg msg)
    {
        return _Outbound.Writer.TryWrite(msg);
    }

    public NetMsg? GetInboundMsg()
    {
        _Inbound.Reader.TryRead(out var msg);
        return msg;
    }

    public NetMsg? GetOutboundMsg()
    {
        _Outbound.Reader.TryRead(out var msg);
        return msg;
    }

    public void SetVersion(int version)
    {
        _Version = version;
    }

    public void setUserAgent(string userAgent)
    {
        _UserAgent = userAgent;

    }

    public void setVerack(bool val)
    {
        _VerackRecv = val;
    }

}