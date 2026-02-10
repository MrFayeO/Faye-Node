using System.Buffers.Binary;

public class Utils
{
    public static int VarIntEncoding(ulong size, Span<byte> output)
    {
        int encodingSize = 1;
        if (size < 0xFD)
        {
            output[0] = (byte)size;
        }
        else if (size <= ushort.MaxValue)
        {
            output[0] = 0xFD;
            BinaryPrimitives.WriteUInt16LittleEndian(output[1..3], (ushort)size);
            encodingSize = 3;
        }
        else if (size <= uint.MaxValue)
        {
            output[0] = 0xFE;
            BinaryPrimitives.WriteUInt32LittleEndian(output[1..5], (uint)size);
            encodingSize = 5;
        }
        else
        {
            output[0] = 0xFF;
            BinaryPrimitives.WriteUInt64LittleEndian(output[1..9], size);
            encodingSize = 9;
        }
        return encodingSize;
    }

    public static ulong VarIntDecodeNoCnt(Span<byte> input)
    {
        var format = VarIntGetFormat(input[0]);
        int idx = 1;
        ulong stringLength = 0;
        switch (format)
        {
            case 1:
                stringLength = input[0];
                break;
            case 3:
                stringLength = BinaryPrimitives.ReadUInt16LittleEndian(input[idx..(idx + 2)]);
                break;
            case 5:
                stringLength = BinaryPrimitives.ReadUInt32LittleEndian(input[idx..(idx + 4)]);
                break;
            case 9:
                stringLength = BinaryPrimitives.ReadUInt64LittleEndian(input[idx..(idx + 8)]);
                break;
        }

        return stringLength;
    }

    public static ulong VarIntDecode(Span<byte> input, ref int cnt)
    {
        var format = VarIntGetFormat(input[0]);
        int idx = 1;
        ulong stringLength = 0;

        switch (format)
        {
            case 1:
                stringLength = input[0];
                break;
            case 3:
                stringLength = ReadU16LE(input[idx..(idx + 2)], ref cnt);
                break;
            case 5:
                stringLength = ReadU32LE(input[idx..(idx + 4)], ref cnt);
                break;
            case 9:
                stringLength = ReadU64LE(input[idx..(idx + 8)], ref cnt);
                break;
        }

        return stringLength;
    }

    public static int VarIntGetFormat(byte format) =>
        format switch
        {
            < 0xFD => 1,
            0xFD => 3,
            0xFE => 5,
            0xFF => 9,

        };

    public static void U16LE(Span<byte> output, ref int cnt, ushort value)
    {
        BinaryPrimitives.WriteUInt16LittleEndian(output, value);
        cnt += 2;
    }

    public static void I16BE(Span<byte> output, ref int cnt, short value)
    {
        BinaryPrimitives.WriteInt16BigEndian(output, value);
        cnt += 2;
    }

    public static void U16BE(Span<byte> output, ref int cnt, ushort value)
    {
        BinaryPrimitives.WriteUInt16BigEndian(output, value);
        cnt += 2;
    }

    public static void I16LE(Span<byte> output, ref int cnt, short value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(output, value);
        cnt += 2;
    }

    public static void U32LE(Span<byte> output, ref int cnt, uint value)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(output, value);
        cnt += 4;
    }

    public static void I32LE(Span<byte> output, ref int cnt, int value)
    {
        BinaryPrimitives.WriteInt32LittleEndian(output, value);
        cnt += 4;
    }

    public static void U32BE(Span<byte> output, ref int cnt, uint value)
    {
        BinaryPrimitives.WriteUInt32BigEndian(output, value);
        cnt += 4;
    }

    public static void I32BE(Span<byte> output, ref int cnt, int value)
    {
        BinaryPrimitives.WriteInt32BigEndian(output, value);
        cnt += 4;
    }

    public static void U64LE(Span<byte> output, ref int cnt, ulong value)
    {
        BinaryPrimitives.WriteUInt64LittleEndian(output, value);
        cnt += 8;
    }

    public static void I64LE(Span<byte> output, ref int cnt, long value)
    {
        BinaryPrimitives.WriteInt64LittleEndian(output, value);
        cnt += 8;
    }

    public static void U64BE(Span<byte> output, ref int cnt, ulong value)
    {
        BinaryPrimitives.WriteUInt64BigEndian(output, value);
        cnt += 8;
    }

    public static void I64BE(Span<byte> output, ref int cnt, long value)
    {
        BinaryPrimitives.WriteInt64BigEndian(output, value);
        cnt += 8;
    }

    public static ushort ReadU16LE(Span<byte> input, ref int cnt)
    {
        cnt += 2;
        return BinaryPrimitives.ReadUInt16LittleEndian(input);
    }

    public static short ReadI16BE(Span<byte> input, ref int cnt)
    {
        cnt += 2;
        return BinaryPrimitives.ReadInt16BigEndian(input);
    }

    public static ushort ReadU16BE(Span<byte> input, ref int cnt)
    {
        cnt += 2;
        return BinaryPrimitives.ReadUInt16BigEndian(input);
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

    public static int ReadI32LE(Span<byte> input, ref int cnt)
    {
        cnt += 4;
        return BinaryPrimitives.ReadInt32LittleEndian(input);
    }

    public static uint ReadU32BE(Span<byte> input, ref int cnt)
    {
        cnt += 4;
        return BinaryPrimitives.ReadUInt32BigEndian(input);
    }

    public static int ReadI32BE(Span<byte> input, ref int cnt)
    {
        cnt += 4;
        return BinaryPrimitives.ReadInt32BigEndian(input);
    }

    public static ulong ReadU64LE(Span<byte> input, ref int cnt)
    {
        cnt += 8;
        return BinaryPrimitives.ReadUInt64LittleEndian(input);
    }

    public static long ReadI64LE(Span<byte> input, ref int cnt)
    {
        cnt += 8;
        return BinaryPrimitives.ReadInt64LittleEndian(input);
    }

    public static ulong ReadU64BE(Span<byte> input, ref int cnt)
    {
        cnt += 8;
        return BinaryPrimitives.ReadUInt64BigEndian(input);
    }

    public static long ReadI64BE(Span<byte> input, ref int cnt)
    {
        cnt += 8;
        return BinaryPrimitives.ReadInt64BigEndian(input);
    }


}