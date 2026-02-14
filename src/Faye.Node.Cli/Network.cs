using System.Collections.Concurrent;
using System.Threading.Channels;

public sealed class Network : INetworkEvent
{
    private Channel<string> _OutboundConnectionQueue = Channel.CreateUnbounded<string>();
    private ulong _Cnt = 0;
    ConcurrentDictionary<ulong, Node> _Peers = new();

    public event Action<Node>? OnNodeConnected;
    public async Task Run()
    {
        await foreach (var addr in _OutboundConnectionQueue.Reader.ReadAllAsync())
        {

            SockS5 nodeSocket = new(Constants.TOR_PORT);

            try
            {
                await nodeSocket.ConnectToDomain(addr);
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine($"Failed to connect to peer {addr}");
                continue;
            }

            Console.WriteLine($"Connected to peer:{addr}");
            Node newNode = new(nodeSocket, addr, _Cnt);
            _Peers[_Cnt] = newNode;
            _Cnt++;

            OnNodeConnected?.Invoke(newNode);
            _ = ProcessOutboundMsg(newNode);
            _ = ProcessInboundMsg(newNode);

        }
    }


    private async Task ProcessOutboundMsg(Node peer)
    {
        var netStream = peer.Socket.AsNetworkStream();
        await foreach (var msg in peer.OutboundMessages())
        {
            Console.WriteLine("Sending out msg");
            Console.WriteLine(msg.Header);
            Console.WriteLine(msg.Payload);
            await netStream.WriteAsync(msg.MemoryNetMsg);

        }
    }

    private async Task ProcessInboundMsg(Node peer)
    {
        while (true)
        {
            var netStream = peer.Socket.AsNetworkStream();
            byte[] h24 = new byte[24];

            await netStream.ReadExactlyAsync(h24);
            var parsedHeader = PacketHeader.Parse(h24);

            // TODO: Create a Dispatch to take command raw payload
            //  and construct the correct NetMsg
            NetMsg newMsg = new();
            var cmd = Utils.GetStringWithNoPadding(parsedHeader.Command);
            switch (cmd)
            {
                case "version":
                    byte[] payload = new byte[parsedHeader.PayloadLength];
                    await netStream.ReadExactlyAsync(payload);
                    IBitcoinPayload parsedVersion = VersionMsg.Deserialize(payload);
                    newMsg = new(parsedVersion, parsedHeader);
                    break;

                case "ping":
                case "pong":
                    payload = new byte[parsedHeader.PayloadLength];
                    await netStream.ReadExactlyAsync(payload);
                    IBitcoinPayload parsedPingPong = PingPongMsg.Deserialize(payload);
                    newMsg = new(parsedPingPong, parsedHeader);
                    break;

                case "verack":
                    newMsg = new(parsedHeader);
                    break;

                case "wtxidrelay":
                case "feefilter":
                case "sendcmpct":
                case "sendaddrv2":
                case "alert":
                case "inv":
                    payload = new byte[parsedHeader.PayloadLength];
                    await netStream.ReadExactlyAsync(payload);
                    continue;

                default:
                    Console.WriteLine($"Unsupported Cmd: {cmd}");
                    continue;

            }
            Console.WriteLine($"New inbound message for {peer.NodeId}");
            Console.WriteLine(newMsg.Header);
            Console.WriteLine(newMsg.Payload);
            Console.WriteLine("---------------------------------------------");
            peer.AddInboundMsg(newMsg);
            await Task.Delay(1000);
        }
    }

    public void AddNewPeer(string addr)
    {
        _OutboundConnectionQueue.Writer.TryWrite(addr);
    }

}