public sealed class Network
{
    private const string NODE_ADDR = "25miqadfzmfkt6s5lg6alb7jhztqkxpq66hefqhj7vd4mmvuhnvullad.onion";

    public async Task PeerConnect()
    {
        SockS5 nodeSocket = new(Constants.TOR_PORT);
        var domainRes = nodeSocket.ConnectToDomain(NODE_ADDR);

        IBitcoinPayload versionPayload = new VersionMsg(services: 0);
        NetMsg versionMsg = new(versionPayload, CommandName.Version);

        var ClientIO = nodeSocket.AsNetworkStream();

        var res = await domainRes;
        if (res != (int)SockS5Reply.SOCK5_REPLY_GRANTED)
        {
            // Not handled, log for now
            Console.WriteLine($"Failed to connect to domain");
            return;
        }

        await ClientIO.WriteAsync(versionMsg.MemoryNetMsg);

        byte[] header = new byte[24];
        await ClientIO.ReadExactlyAsync(header);
        var respHeader = PacketHeader.Parse(header);

        Console.WriteLine(respHeader);

        byte[] versionReply = new byte[respHeader.PayloadLength];
        await ClientIO.ReadExactlyAsync(versionReply);

        var parsedVersion = VersionMsg.Deserialize(versionReply);

        Console.WriteLine(parsedVersion);

        NetMsg verackMsg = new(CommandName.VerAck);

        await ClientIO.WriteAsync(verackMsg.MemoryNetMsg);

        await ClientIO.ReadExactlyAsync(header);
        var verack = PacketHeader.Parse(header);

        Console.WriteLine(verack);

    }

}