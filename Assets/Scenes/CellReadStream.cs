using System;
using System.Runtime.InteropServices;
using System.Text;

public class CellReadStream
{
    [DllImport("CppNet100")]
    private static extern IntPtr CellReadStream_Create(IntPtr data, int len);

    [DllImport("CppNet100")]
    private static extern sbyte CellReadStream_ReadInt8(IntPtr cppStreamobj);

    [DllImport("CppNet100")]
    private static extern Int16 CellReadStream_ReadInt16(IntPtr cppStreamobj);

    [DllImport("CppNet100")]
    private static extern Int32 CellReadStream_ReadInt32(IntPtr cppStreamobj);

    [DllImport("CppNet100")]
    private static extern Int64 CellReadStream_ReadInt64(IntPtr cppStreamobj);

    [DllImport("CppNet100")]
    private static extern byte CellReadStream_ReadUInt8(IntPtr cppStreamobj);

    [DllImport("CppNet100")]
    private static extern UInt16 CellReadStream_ReadUInt16(IntPtr cppStreamobj);

    [DllImport("CppNet100")]
    private static extern UInt32 CellReadStream_ReadUInt32(IntPtr cppStreamobj);

    [DllImport("CppNet100")]
    private static extern UInt64 CellReadStream_ReadUInt64(IntPtr cppStreamobj);

    [DllImport("CppNet100")]
    private static extern float CellReadStream_ReadFloat(IntPtr cppStreamobj);

    [DllImport("CppNet100")]
    private static extern double CellReadStream_ReadDouble(IntPtr cppStreamobj);

    [DllImport("CppNet100")]
    private static extern bool CellReadStream_ReadString(IntPtr cppStreamobj, StringBuilder buffer, Int32 len);

    [DllImport("CppNet100")]
    private static extern UInt32 CellReadStream_OnlyReadUint32(IntPtr cppStreamobj);

    private IntPtr _cppStreamObj = IntPtr.Zero;

    //构造函数，接收到的数据和长度
    public CellReadStream(IntPtr data, int len)
    {
        _cppStreamObj = CellReadStream_Create(data, len);
    }

    public sbyte readInt8(sbyte n = 0)
    {
        return CellReadStream_ReadInt8(_cppStreamObj);
    }

    public Int16 readInt16(Int16 n = 0)
    {
        return CellReadStream_ReadInt16(_cppStreamObj);
    }

    public Int32 readInt32(Int32 n = 0)
    {
        return CellReadStream_ReadInt32(_cppStreamObj);
    }

    public Int64 readInt64(Int64 n = 0)
    {
        return CellReadStream_ReadInt64(_cppStreamObj);
    }

    public byte readUInt8(byte n = 0)
    {
        return CellReadStream_ReadUInt8(_cppStreamObj);
    }

    public UInt16 readUInt16(UInt16 n = 0)
    {
        return CellReadStream_ReadUInt16(_cppStreamObj);
    }

    public UInt32 readUInt32(UInt32 n = 0)
    {
        return CellReadStream_ReadUInt32(_cppStreamObj);
    }

    public UInt64 readUInt64(UInt64 n = 0)
    {
        return CellReadStream_ReadUInt64(_cppStreamObj);
    }

    public float readFloat(float n = 0.0f)
    {
        return CellReadStream_ReadFloat(_cppStreamObj);
    }

    public double readDouble(double n = 0)
    {
        return CellReadStream_ReadDouble(_cppStreamObj);
    }

    public UInt32 OnlyReadUint32(IntPtr cppStreamobj)
    {
        return CellReadStream_OnlyReadUint32(_cppStreamObj);
    }

    public string readString()
    {
        Int32 len = (Int32)readUInt32();//字符串长度
        byte[] buffer = new byte[len];
        for (int i = 0; i < len; ++i)
        {
            buffer[i] = readUInt8();
        }
        return Encoding.UTF8.GetString(buffer, 0, len);
    }

    public int[] readInt32s()
    {
        //先把长度读出来
        int len = readInt32();
        Int32[] ret = new Int32[len];
        for (int i = 0; i < len; ++i)
        {
            ret[i] = readInt32();
        }
        return ret;
    }

    public NetCMD readNetCMD()
    {
        UInt16 n = readUInt16();
        return (NetCMD)(n);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
