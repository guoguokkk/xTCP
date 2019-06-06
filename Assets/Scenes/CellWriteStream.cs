using System;
using System.Runtime.InteropServices;
using System.Text;

public class CellWriteStream
{
    [DllImport("CppNet100")]
    private static extern IntPtr CellWriteStream_Create(int nSize);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteInt8(IntPtr cppStreamObj, sbyte n);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteInt16(IntPtr cppStreamObj, Int16 n);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteInt32(IntPtr cppStreamObj, Int32 n);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteInt64(IntPtr cppStreamObj, Int64 n);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteUInt8(IntPtr cppStreamObj, byte n);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteUInt16(IntPtr cppStreamObj, UInt16 n);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteUInt32(IntPtr cppStreamObj, UInt32 n);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteUInt64(IntPtr cppStreamObj, UInt64 n);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteFloat(IntPtr cppStreamObj, float n);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteDouble(IntPtr cppStreamObj, double n);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteString(IntPtr cppStreamObj, string s);

    [DllImport("CppNet100")]
    private static extern bool CellWriteStream_WriteArray(IntPtr cppStreamObj, byte[] pData, UInt32 len);



    /////////////////////////////

    private IntPtr cppStreamObj = IntPtr.Zero;

    //构造函数
    public CellWriteStream(int nSize = 128)
    {
        cppStreamObj = CellWriteStream_Create(nSize);
    }

    //写入数据缓冲区
    public void writeInt8(sbyte n)
    {
        CellWriteStream_WriteInt8(cppStreamObj, n);
    }

    public void writeInt16(Int16 n)
    {
        CellWriteStream_WriteInt16(cppStreamObj, n);
    }

    public void writeInt32(Int32 n)
    {
        CellWriteStream_WriteInt32(cppStreamObj, n);
    }

    public void writeInt64(Int64 n)
    {
        CellWriteStream_WriteInt64(cppStreamObj, n);
    }

    public void writeUInt8(byte n)
    {
        CellWriteStream_WriteUInt8(cppStreamObj, n);
    }

    public void writeUInt16(UInt16 n)
    {
        CellWriteStream_WriteUInt16(cppStreamObj, n);
    }

    public void writeUInt32(UInt32 n)
    {
        CellWriteStream_WriteUInt32(cppStreamObj, n);
    }

    public void writeUInt64(UInt64 n)
    {
        CellWriteStream_WriteUInt64(cppStreamObj, n);
    }

    public void writeFloat(float n)
    {
        CellWriteStream_WriteFloat(cppStreamObj, n);
    }

    public void writeDouble(double n)
    {
        CellWriteStream_WriteDouble(cppStreamObj, n);
    }

    public void writeString(string s)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(s);//要求传入utf8
        writeUInt32((UInt32)buffer.Length + 1);//将字符串长度+1写入数据缓冲区，把最后的结束符也写入
        for (int i = 0; i < buffer.Length; ++i)
        {
            writeUInt8(buffer[i]);
        }
        writeUInt8(0);//结束符
    }

    //写入字节数组
    public void writeBytes(byte[] data)
    {
        writeUInt32((UInt32)data.Length);//将数组长度写入数据缓冲区
        for (int i = 0; i < data.Length; ++i)
        {
            writeUInt8(data[i]);
        }//写入数据缓冲区
    }

    //写入整型数组
    public void writeInt32s(int[] data)
    {
        writeUInt32((UInt32)data.Length);//将数组长度写入数据缓冲区
        for (int i = 0; i < data.Length; ++i)
        {
            writeInt32(data[i]);//写入数据缓冲区
        }
    }

    public void setNetCmd(NetCMD cmd)
    {
        writeUInt16((UInt16)cmd);
    }

    //计算字节流总长度
    public void finish()
    {

    }

    public IntPtr cppObj
    {
        get
        {
            return cppStreamObj;
        }
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
