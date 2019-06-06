using UnityEngine;
using System;

public class CellClientNode : CellTCPClient
{

    public string IP = "202.114.7.16";
    public short PORT = 8099;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        this.create();
        this.connect(IP, PORT);
    }

    // Update is called once per frame  
    void Update()
    {
        this.onRun();
        CellWriteStream s = new CellWriteStream(256);
        s.setNetCmd(NetCMD.CMD_LOGOUT);
        s.writeInt8(1);
        s.writeInt16(2);
        s.writeInt32(3);
        s.writeFloat(4.5f);
        s.writeDouble(6.7);
        s.writeString("client");
        s.writeString("kzj");
        int[] b = { 1, 2, 3, 4, 5, 6 };
        s.writeInt32s(b);
        s.finish();
        this.sendData(s);
    }

    void OnDestroy()
    {
        this.close();
    }

    public override void OnNetMsgBytes(IntPtr data, int len)
    {
        CellReadStream r = new CellReadStream(data, len);

        //注意：在构造函数中已经包含了消息长度和消息类型的解析，所以不需要再次解析
        //消息长度
        //UInt16 n1 = r.readUInt16();
        //Debug.Log(n1);
        //消息类型
        //NetCMD n2 = r.readNetCMD();
        //Debug.Log(n2);

        sbyte n3 = r.readInt8();
        Debug.Log(n3);

        Int16 n4 = r.readInt16();
        Debug.Log(n4);

        Int32 n5 = r.readInt32();
        Debug.Log(n5);

        float n6 = r.readFloat();
        Debug.Log(n6);

        double n7 = r.readDouble();
        Debug.Log(n7);

        string s1 = r.readString();
        Debug.Log(s1);

        string s2 = r.readString();
        Debug.Log(s2);

        Int32[] arr = r.readInt32s();
        for (int i = 0; i < arr.Length; i++)
        {
            Debug.Log(arr[i]);
        }
    }
}
