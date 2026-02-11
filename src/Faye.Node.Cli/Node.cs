public class Node
{
    readonly SockS5 _Socket;
    string _UserAgent;
    uint _Version;
    bool _VerackRecv;
    readonly string _Addr; // Should be NetAddr in the future
    readonly ulong _NodeId;

    public SockS5 Socket => _Socket;
    public string UserAgent => _UserAgent;
    public uint Version => _Version;
    public bool VerackRecv => _VerackRecv;
    public string Addr => _Addr;
    public ulong NodeId => _NodeId;

    public Node(SockS5 socket, string addr, ulong nodeId)
    {
        _Socket = socket;
        _Addr = addr;
        _NodeId = nodeId;
        _VerackRecv = false;
    }
}