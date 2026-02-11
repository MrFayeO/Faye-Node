using System.Buffers;
using System.Buffers.Binary;
using System.Text;

ref struct ByteStreamReader
{
    private int _Cnt;
    private readonly ReadOnlySpan<byte> _Block;

    public ByteStreamReader(byte[] block)
    {
        _Cnt = 0;
        _Block = block;
    }

    public byte ReadByte()
    {
        return _Block[_Cnt++];
    }

    public short ReadI16LE()
    {
        var val = BinaryPrimitives.ReadInt16LittleEndian(_Block.Slice(_Cnt, 2));
        _Cnt += 2;
        return val;
    }
    public short ReadI16BE()
    {
        var val = BinaryPrimitives.ReadInt16BigEndian(_Block.Slice(_Cnt, 2));
        _Cnt += 2;
        return val;
    }

    public ushort ReadU16LE()
    {
        var val = BinaryPrimitives.ReadUInt16LittleEndian(_Block.Slice(_Cnt, 2));
        _Cnt += 2;
        return val;
    }
    public ushort ReadU16BE()
    {
        var val = BinaryPrimitives.ReadUInt16BigEndian(_Block.Slice(_Cnt, 2));
        _Cnt += 2;
        return val;
    }

    public int ReadI32LE()
    {
        var val = BinaryPrimitives.ReadInt32LittleEndian(_Block.Slice(_Cnt, 4));
        _Cnt += 4;
        return val;
    }
    public int ReadI32BE()
    {
        var val = BinaryPrimitives.ReadInt32BigEndian(_Block.Slice(_Cnt, 4));
        _Cnt += 4;
        return val;
    }

    public uint ReadU32LE()
    {
        var val = BinaryPrimitives.ReadUInt32LittleEndian(_Block.Slice(_Cnt, 4));
        _Cnt += 4;
        return val;
    }
    public uint ReadU32BE()
    {
        var val = BinaryPrimitives.ReadUInt32BigEndian(_Block.Slice(_Cnt, 4));
        _Cnt += 4;
        return val;
    }

    public long ReadI64LE()
    {
        var val = BinaryPrimitives.ReadInt64LittleEndian(_Block.Slice(_Cnt, 8));
        _Cnt += 8;
        return val;
    }
    public long ReadI64BE()
    {
        var val = BinaryPrimitives.ReadInt64BigEndian(_Block.Slice(_Cnt, 8));
        _Cnt += 8;
        return val;
    }

    public ulong ReadU64LE()
    {
        var val = BinaryPrimitives.ReadUInt64LittleEndian(_Block.Slice(_Cnt, 8));
        _Cnt += 8;
        return val;
    }
    public ulong ReadU64BE()
    {
        var val = BinaryPrimitives.ReadUInt64BigEndian(_Block.Slice(_Cnt, 8));
        _Cnt += 8;
        return val;
    }

    public string ReadVarString()
    {
        var format = ReadByte();
        var formatType = Utils.VarIntGetFormat(format);

        ulong bytesToRead = format;
        switch (formatType)
        {
            case 1:
                break;
            case 3:
                bytesToRead = ReadU16LE();
                break;
            case 5:
                bytesToRead = ReadU32LE();
                break;
            case 9:
                bytesToRead = ReadU64LE();
                break;
        }

        var str = Encoding.ASCII.GetString(_Block.Slice(_Cnt, (int)bytesToRead)); // Problem for large strings
        _Cnt += (int)bytesToRead;
        return str;
    }

    public string ReadString(int len)
    {
        string command = Encoding.ASCII.GetString(_Block.Slice(_Cnt, len));
        _Cnt += len;
        return command;
    }

    public NetAddr ReadNetAddrWithoutTime()
    {
        // TODO: Actually read something

        _Cnt += 26;
        return new NetAddr();

    }



    public void SkipBytes(int amount)
    {
        _Cnt += amount;
    }

}

ref struct ByteStreamWriter
{
    private readonly IBufferWriter<byte> _Writer;
    // private int _Cnt; TODO: Optomize to use _Cnt instead so we can Advance(_Cnt) once;
    public ByteStreamWriter(IBufferWriter<byte> writer)
    {
        _Writer = writer;

    }

    public void WriteByte(byte value)
    {
        var output = _Writer.GetSpan(1);
        output[0] = value;
        _Writer.Advance(1);
    }

    public void WriteU16LE(ushort value)
    {
        var output = _Writer.GetSpan(2);
        BinaryPrimitives.WriteUInt16LittleEndian(output, value);
        _Writer.Advance(2);
    }

    public void WriteI16BE(short value)
    {
        var output = _Writer.GetSpan(2);
        BinaryPrimitives.WriteInt16BigEndian(output, value);
        _Writer.Advance(2);
    }

    public void WriteU16BE(ushort value)
    {
        var output = _Writer.GetSpan(2);
        BinaryPrimitives.WriteUInt16BigEndian(output, value);
        _Writer.Advance(2);
    }

    public readonly void WriteI16LE(short value)
    {
        var output = _Writer.GetSpan(2);
        BinaryPrimitives.WriteInt16LittleEndian(output, value);
        _Writer.Advance(4);
    }

    public void WriteU32LE(uint value)
    {
        var output = _Writer.GetSpan(4);
        BinaryPrimitives.WriteUInt32LittleEndian(output, value);
        _Writer.Advance(4);
    }

    public void WriteI32LE(int value)
    {
        var output = _Writer.GetSpan(4);
        BinaryPrimitives.WriteInt32LittleEndian(output, value);
        _Writer.Advance(4);
    }

    public void WriteU32BE(uint value)
    {
        var output = _Writer.GetSpan(4);
        BinaryPrimitives.WriteUInt32BigEndian(output, value);
        _Writer.Advance(4);
    }

    public void WriteI32BE(int value)
    {
        var output = _Writer.GetSpan(4);
        BinaryPrimitives.WriteInt32BigEndian(output, value);
        _Writer.Advance(4);
    }

    public void WriteU64LE(ulong value)
    {
        var output = _Writer.GetSpan(4);
        BinaryPrimitives.WriteUInt64LittleEndian(output, value);
        _Writer.Advance(8);
    }

    public void WriteI64LE(long value)
    {
        var output = _Writer.GetSpan(4);
        BinaryPrimitives.WriteInt64LittleEndian(output, value);
        _Writer.Advance(8);
    }

    public void WriteU64BE(ulong value)
    {
        var output = _Writer.GetSpan(4);
        BinaryPrimitives.WriteUInt64BigEndian(output, value);
        _Writer.Advance(8);
    }

    public void WriteI64BE(long value)
    {
        var output = _Writer.GetSpan(4);
        BinaryPrimitives.WriteInt64BigEndian(output, value);
        _Writer.Advance(8);
    }

    public void WriteNetAddrNoTime(NetAddr value)
    {
        var output = _Writer.GetSpan(26);
        output.Clear();
        _Writer.Advance(26);
    }

    public void WriteString(string str)
    {
        var output = _Writer.GetSpan(str.Length);
        Encoding.ASCII.GetBytes(str, output);
        _Writer.Advance(str.Length);
    }

    public void WriteVarString(string str)
    {
        // UserAgent name > 255 is silly 
        // to begin with even if supported by the protocol

        WriteByte((byte)str.Length);
        WriteString(str);

    }

    public void WriteString(string str, int len)
    {
        var output = _Writer.GetSpan(len);
        output.Clear();
        Encoding.ASCII.GetBytes(str, output);
        _Writer.Advance(len);
    }
}
