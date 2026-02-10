using System.Buffers;

public sealed class Network
{
    private const string NODE_ADDR = "25miqadfzmfkt6s5lg6alb7jhztqkxpq66hefqhj7vd4mmvuhnvullad.onion";
    private readonly Dictionary<ulong, Node> _NodeLookUpTable = [];
    private ulong _NodeIdCounter = 0;


    public async Task PeerConnect()
    {
        SockS5 nodeSocket = new(Constants.TOR_PORT);
        var domainRes = nodeSocket.ConnectToDomain(NODE_ADDR);

        var currentNodeIndex = _NodeIdCounter;
        _NodeLookUpTable[_NodeIdCounter++] = new Node(socket: nodeSocket, addr: NODE_ADDR, nodeId: currentNodeIndex);

        ArrayBufferWriter<byte> payloadWriter = new();
        var VersionMessage = new VersionMessage(services: 0);
        VersionMessage.Serialize(ref payloadWriter, protocolVersion: 0);

        byte[] versionHeader = new byte[24];
        PacketHeader.Create(CommandName.Version, payloadWriter.WrittenSpan).ToBytes(versionHeader);

        var ClientIO = nodeSocket.AsNetworkStream();

        var res = await domainRes;
        if (res != (int)SockS5Reply.SOCK5_REPLY_GRANTED)
        {
            // Not handled, log for now
            Console.WriteLine($"Failed to connect to domain with Node index: {currentNodeIndex}");
            return;
        }

        await ClientIO.WriteAsync(versionHeader);
        await ClientIO.WriteAsync(payloadWriter.WrittenMemory);

        await ClientIO.ReadExactlyAsync(versionHeader);
        var respHeader = PacketHeader.Parse(versionHeader);

        Console.WriteLine(respHeader);

        byte[] respPayload = new byte[respHeader.PayloadLength];
        await ClientIO.ReadExactlyAsync(respPayload);

        var resp = VersionMessage.Parse(respPayload);
        Console.WriteLine(resp);

        byte[] verackHeader = new byte[24];
        PacketHeader.Create(CommandName.VerAck, []).ToBytes(verackHeader);

        await ClientIO.WriteAsync(verackHeader);

        await ClientIO.ReadExactlyAsync(verackHeader);
        var verack = PacketHeader.Parse(verackHeader);

        Console.WriteLine(verack);

    }

}