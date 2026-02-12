using System.Buffers.Binary;
using System.Security.Cryptography;

public class Utils
{

    public static int VarIntGetFormat(byte format) =>
        format switch
        {
            < 0xFD => 1,
            0xFD => 3,
            0xFE => 5,
            0xFF => 9,

        };

    public static ulong GenerateNonce()
    {
        return BinaryPrimitives.ReadUInt64LittleEndian(RandomNumberGenerator.GetBytes(8));
    }

    public static long GetUnixTimeInSeconds()
    {
        return DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    public static uint CalculateChecksum(ReadOnlySpan<byte> payload)
    {
        var digest1 = SHA256.HashData(payload);
        var digest2 = SHA256.HashData(digest1);
        return BinaryPrimitives.ReadUInt32LittleEndian(digest2.AsSpan()[..4]);

    }

    public static string GetStringWithNoPadding(string str)
    {
        var idx = str.IndexOf('\0');

        return str[..idx];
    }



}