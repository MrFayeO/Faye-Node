using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

public enum SockS5Reply
{
    SOCK5_REPLY_GRANTED = 0x00,
    SOCK5_REPLY_GENERAL_FAILURE = 0x01,
    SOCK5_REPLY_RULESET_BLOCKED = 0x02,
    SOCK5_REPLY_NETWORK_UNREACHABLE = 0x03,
    SOCK5_REPLY_HOST_UNREACHABLE = 0x04,
    SOCK5_REPLY_CONNECTION_REFUSED = 0x05,
    SOCK5_REPLY_TTL_EXPIRED = 0x06,
    SOCK5_REPLY_COMMAND_NOT_SUPPORTED = 0x07,
    SOCK5_REPLY_ADDRESS_TYPE_UNSUPPORTED = 0x08,
};

public sealed class SockS5
{

    const byte SOCKS5_VERSION = 0x05;

    const byte SOCKS5_NMETHODS_ONE = 0x1;
    const byte SOCKS5_METHOD_NO_AUTH = 0x0;
    const byte SOCKS5_CMD_CONNECT = 0x01;

    const byte SOCKS5_ATYP_DOMAIN_NAME = 0x03;
    const byte SOCKS5_RESERVED = 0x0;

    const short SOCKS5_CONNECT_DOMAIN_HEADER_SIZE = 7;
    const short MAX_DOMAIN_SIZE = 255;

    private readonly short _TorPort;
    private const short BITCOIN_TOR_PORT = 8333;
    private readonly Socket _ClientSock;

    public SockS5(short TorPort)
    {
        _TorPort = TorPort;
        _ClientSock = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public NetworkStream AsNetworkStream()
    {
        return new NetworkStream(_ClientSock);
    }

    public async Task<int> ConnectToDomain(string Hostname)
    {

        if (Hostname.Length > MAX_DOMAIN_SIZE)
        {
            return -1;
        }

        IPEndPoint connEndpoint = new(IPAddress.Loopback, _TorPort);
        await _ClientSock.ConnectAsync(connEndpoint);

        NetworkStream clientIO = new(_ClientSock);


        await clientIO.WriteAsync([SOCKS5_VERSION, SOCKS5_NMETHODS_ONE, SOCKS5_METHOD_NO_AUTH], 0, 3);

        CancellationTokenSource cts = new();
        cts.CancelAfter(7500);

        byte[] greeting = new byte[2];
        await clientIO.ReadExactlyAsync(greeting, cts.Token);

        if (greeting[0] != SOCKS5_VERSION || greeting[1] != SOCKS5_METHOD_NO_AUTH)
        {
            return -1;
        }

        int payloadLength = Hostname.Length + SOCKS5_CONNECT_DOMAIN_HEADER_SIZE;
        byte[] payload = new byte[payloadLength];

        int idx = 0;
        payload[idx++] = SOCKS5_VERSION;
        payload[idx++] = SOCKS5_CMD_CONNECT;
        payload[idx++] = SOCKS5_RESERVED;
        payload[idx++] = SOCKS5_ATYP_DOMAIN_NAME;
        payload[idx++] = (byte)Hostname.Length;

        idx += Encoding.ASCII.GetBytes(Hostname, payload.AsSpan()[idx..(idx + Hostname.Length)]);

        BinaryPrimitives.WriteInt16BigEndian(payload.AsSpan()[idx..(idx + 2)], BITCOIN_TOR_PORT);

        await clientIO.WriteAsync(payload);
        byte[] buffer = new byte[10];
        await clientIO.ReadExactlyAsync(buffer, cts.Token);

        return buffer[1]; // Holds the SOCKS5 Status
    }

}