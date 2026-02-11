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


    public static void U32LE(Span<byte> output, ref int cnt, uint value)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(output, value);
        cnt += 4;
    }


    public static short ReadI16LE(Span<byte> input, ref int cnt)
    {
        cnt += 2;
        return BinaryPrimitives.ReadInt16LittleEndian(input);
    }

    public static uint ReadU32LE(Span<byte> input, ref int cnt)
    {
        cnt += 4;
        return BinaryPrimitives.ReadUInt32LittleEndian(input);
    }


    public static long ReadI64LE(Span<byte> input, ref int cnt)
    {
        cnt += 8;
        return BinaryPrimitives.ReadInt64LittleEndian(input);
    }
    public static uint CalculateChecksum(ReadOnlySpan<byte> payload)
    {
        var digest1 = SHA256.HashData(payload);
        var digest2 = SHA256.HashData(digest1);
        return BinaryPrimitives.ReadUInt32LittleEndian(digest2.AsSpan()[..4]);

    }


}