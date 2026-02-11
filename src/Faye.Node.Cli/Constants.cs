
public enum NetworkChain : uint
{
    Mainnet = 0xD9B4BEF9,
    Testnet = 0xDAB5BFFA,
    Testnet3 = 0x0709110B,
    Signet = 0x40CF030A,
    NameCoin = 0xFEB4BEF9,
}

public readonly record struct CommandName(string Value)
{
    public static readonly CommandName VerAck = new("verack");
    public static readonly CommandName Version = new("version");

    public static implicit operator string(CommandName c) => c.Value;
}

public class Constants
{
    public const short TOR_PORT = 9050;
    public const uint CHAIN = (uint)NetworkChain.Mainnet;
    public const int VERSION = 70015;
    public const string USER_AGENT = "Faye-Node";
    public const int HEADER_SIZE = 24;
    public const uint EMPTY_CHECKSUM = 0xE2E0F65D;
}