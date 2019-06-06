using UnityEngine;
using System.Runtime.InteropServices;
using System;
using AOT;

//命令
public enum NetCMD
{
    CMD_LOGIN,
    CMD_LOGIN_RESULT,
    CMD_LOGOUT,
    CMD_LOGOUT_RESULT,
    CMD_ERROR,
    CMD_NEW_USER_JOIN,
    CMD_C2S_HEART,
    CMD_S2C_HEART
};

public class CellTCPClient : MonoBehaviour
{
    public delegate void OnNetMsgCallBack(IntPtr csObj, IntPtr data, int len);

    //处理字节流
    [MonoPInvokeCallback(typeof(OnNetMsgCallBack))]
    public static void OnNetMsgCallBack1(IntPtr csObj, IntPtr data, int len)
    {
        Debug.Log("OnNetMsgCallBack1: " + len);
        GCHandle h = GCHandle.FromIntPtr(csObj);//句柄，将c++传入的对象指针还原为c#对象
        CellTCPClient obj = h.Target as CellTCPClient;

        if (obj)
        {
            obj.OnNetMsgBytes(data, len);
        }
    }

    [DllImport("CppNet100")]
    private static extern IntPtr CellClient_Create(IntPtr csObj, OnNetMsgCallBack cb, int sendSize, int recvSize);

    //连接
    [DllImport("CppNet100")]
    private static extern bool CellClient_Connect(IntPtr ccpClientObj, string ip, short port);

    //运行
    [DllImport("CppNet100")]
    private static extern bool CellClient_OnRun(IntPtr ccpClientObj);

    //关闭
    [DllImport("CppNet100")]
    private static extern void CellClient_Close(IntPtr ccpClientObj);

    //发数据，字节数据流
    [DllImport("CppNet100")]
    private static extern int CellClient_SendData(IntPtr ccpClientObj, byte[] pData, int len);

    [DllImport("CppNet100")]
    private static extern int CellClient_SendStream(IntPtr ccpClientObj, IntPtr cppStreamObj);

    //public:
    private GCHandle _handleThis;
    IntPtr _csThisObj = IntPtr.Zero;//this 对象指针 在c++消息回调中传回
    IntPtr _ccpClientObj = IntPtr.Zero;//c++ NativeTCPClient 对象指针

    public void create()
    {
        _handleThis = GCHandle.Alloc(this);//句柄
        _csThisObj = GCHandle.ToIntPtr(_handleThis);
        _ccpClientObj = CellClient_Create(_csThisObj, OnNetMsgCallBack1, 10240, 10240);
    }

    public bool connect(string ip, short port)
    {
        if (_ccpClientObj == IntPtr.Zero)
            return false;
        return CellClient_Connect(_ccpClientObj, ip, port);
    }

    public bool onRun()
    {
        if (_ccpClientObj == IntPtr.Zero)
            return false;
        return CellClient_OnRun(_ccpClientObj);
    }

    public void close()
    {
        if (_ccpClientObj == IntPtr.Zero)
            return;
        CellClient_Close(_ccpClientObj);
        _ccpClientObj = IntPtr.Zero;
        _handleThis.Free();
    }

    public int sendData(byte[] data)
    {
        if (_ccpClientObj == IntPtr.Zero)
            return 0;
        return CellClient_SendData(_ccpClientObj, data, data.Length);
    }

    public int sendData(CellSendStream ss)
    {
        return sendData(ss.Array);
    }

    public int sendData(CellWriteStream ws)
    {
        if (_ccpClientObj == IntPtr.Zero)
            return 0;
        return CellClient_SendStream(_ccpClientObj, ws.cppObj);
    }

    public virtual void OnNetMsgBytes(IntPtr data, int len)
    {
        //byte 无符号整数 0-255
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
