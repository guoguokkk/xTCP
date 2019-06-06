using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;//针对文本的
using System.Runtime.InteropServices;


//接收服务器端的数据并解析
public class CellRecvStream
{
    private byte[] _buffer = null;//数据缓冲区
    private int _nReadPos;

    //构造函数，接收到的数据
    public CellRecvStream(IntPtr data, int len)
    {
        //将c++传入的数据转化为c#的字节数组
        byte[] buffer = new byte[len];//字节数组
        Marshal.Copy(data, buffer, 0, len);//源数据，目标数据，开始拷贝的偏移位置，拷贝长度
        _buffer = buffer;
    }

    //读取数据缓冲区  
    private void pop(int n)
    {
        _nReadPos += n;
    }

    private bool canRead(int n)
    {
        return _buffer.Length - _nReadPos >= n;
    }

    //Sbyte:代表有符号的8位整数，数值范围从-128 ～ 127
    //Byte:代表无符号的8位整数，数值范围从0～255
    public sbyte readInt8(sbyte n = 0)
    {
        if (canRead(1))
        {
            n = (sbyte)_buffer[_nReadPos];//从_nReadPos开始读取
            pop(1);//2字节            
        }
        return n;
    }

    public Int16 readInt16(Int16 n = 0)
    {
        if (canRead(2))
        {
            n = BitConverter.ToInt16(_buffer, _nReadPos);//从_nReadPos开始读取
            pop(2);//2字节            
        }
        return n;
    }

    public Int32 readInt32(Int32 n = 0)
    {
        if (canRead(4))
        {
            n = BitConverter.ToInt32(_buffer, _nReadPos);//从_nReadPos开始读取
            pop(4);//4字节
        }
        return n;
    }

    public Int64 readInt64(Int64 n = 0)
    {
        if (canRead(8))
        {
            n = BitConverter.ToInt64(_buffer, _nReadPos);//从_nReadPos开始读取
            pop(8);//4字节
        }
        return n;
    }

    public byte readUInt8(byte n = 0)
    {
        if (canRead(1))
        {
            n = _buffer[_nReadPos];//从_nReadPos开始读取
            pop(1);//2字节            
        }
        return n;
    }

    public UInt16 readUInt16(UInt16 n = 0)
    {
        if (canRead(2))
        {
            n = BitConverter.ToUInt16(_buffer, _nReadPos);//从_nReadPos开始读取
            pop(2);//2字节            
        }
        return n;
    }

    public UInt32 readUInt32(UInt32 n = 0)
    {
        if (canRead(4))
        {
            n = BitConverter.ToUInt32(_buffer, _nReadPos);//从_nReadPos开始读取
            pop(4);//4字节
        }
        return n;
    }

    public UInt64 readUInt64(UInt64 n = 0)
    {
        if (canRead(8))
        {
            n = BitConverter.ToUInt64(_buffer, _nReadPos);//从_nReadPos开始读取
            pop(8);//4字节
        }
        return n;
    }

    public float readFloat(float n = 0.0f)
    {
        if (canRead(4))
        {
            n = BitConverter.ToSingle(_buffer, _nReadPos);
            pop(4);
        }
        return n;
    }

    public double readDouble(double n = 0)
    {
        if (canRead(8))
        {
            n = BitConverter.ToDouble(_buffer, _nReadPos);
            pop(8);
        }
        return n;
    }

    public string readString()
    {
        string s = string.Empty;
        if (canRead(4))
        {
            //先把长度读出来
            int len = readInt32();
            if (canRead(len) && len > 0)
            {
                s = Encoding.UTF8.GetString(_buffer, _nReadPos, len);
                pop(len);
            }
        }
        return s;
    }

    public byte[] readBytes()
    {
        byte[] ret = null;
        return ret;
    }

    public int[] readInt32s()
    {
        //先把长度读出来
        int len = readInt32();
        Int32[] ret = new Int32[len];
        if (len < 1)
        {
            //error
            return ret;
        }
        if (canRead(len * 4) && len > 0)
        {
            for (int i = 0; i < len; ++i)
                ret[i] = readInt32();
        }
        return ret;
    }

    public NetCMD readNetCMD()
    {
        UInt16 n = readUInt16();
        return (NetCMD)(n);
    }
}
