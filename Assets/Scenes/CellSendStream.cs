using System.Collections.Generic;
using System;
using System.Text;//针对文本的

public class CellSendStream
{
    private List<byte> _byteList = null;//数据缓冲区

    //构造函数
    public CellSendStream(int nSize = 128)
    {
        _byteList = new List<byte>(nSize);//预分配空间
    }

    //写入数据缓冲区
    public void write(byte[] data)
    {
        _byteList.AddRange(data);
    }

    public void writeInt8(sbyte n)
    {
        _byteList.Add((byte)n);//一个字节，加入数据缓冲区
    }

    public void writeInt16(Int16 n)
    {
        byte[] data = BitConverter.GetBytes(n);//位转换
        write(data);//写入数据缓冲区
    }

    public void writeInt32(Int32 n)
    {
        byte[] data = BitConverter.GetBytes(n);//位转换
        write(data);//写入数据缓冲区
    }

    public void writeInt64(Int64 n)
    {
        byte[] data = BitConverter.GetBytes(n);//位转换
        write(data);//写入数据缓冲区
    }

    public void writeUInt8(byte n)
    {
        _byteList.Add(n);//一个字节，加入数据缓冲区
    }

    public void writeUInt16(UInt16 n)
    {
        byte[] data = BitConverter.GetBytes(n);//位转换
        write(data);//写入数据缓冲区
    }

    public void writeUInt32(UInt32 n)
    {
        byte[] data = BitConverter.GetBytes(n);//位转换
        write(data);//写入数据缓冲区
    }

    public void writeUInt64(UInt64 n)
    {
        byte[] data = BitConverter.GetBytes(n);//位转换
        write(data);//写入数据缓冲区
    }

    public void writeFloat(float n)
    {
        byte[] data = BitConverter.GetBytes(n);//位转换
        write(data);//写入数据缓冲区
    }

    public void writeDouble(double n)
    {
        byte[] data = BitConverter.GetBytes(n);//位转换
        write(data);//写入数据缓冲区
    }

    public void writeString(string s)
    {
        byte[] data = Encoding.UTF8.GetBytes(s);//要求传入utf8
        writeUInt32((UInt32)data.Length + 1);//将字符串长度+1写入数据缓冲区，把最后的结束符也写入
        write(data);//写入数据缓冲区
        writeUInt8(0);//结束符
    }

    //写入字节数组
    public void writeBytes(byte[] data)
    {
        writeUInt32((UInt32)data.Length);//将数组长度写入数据缓冲区
        write(data);//写入数据缓冲区
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
        if (_byteList.Count > UInt16.MaxValue)
        {
            //给出警告，字节流过大，需要分包
        }
        UInt16 len = (UInt16)_byteList.Count;
        len += 2;//插入字节流总长度后，长度加2
        _byteList.InsertRange(0, BitConverter.GetBytes(len));//直接插入到字节流头部
    }

    public byte[] Array
    {
        get
        {
            return _byteList.ToArray();
        }
    }

}
