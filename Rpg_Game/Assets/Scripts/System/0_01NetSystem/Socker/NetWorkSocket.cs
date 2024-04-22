using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetWorkSocket : MonoBehaviour
{
    private static NetWorkSocket instance;
    public static NetWorkSocket Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject("NetWorkSocket");
                if (gameObject.GetComponent<NetWorkSocket>() == null)
                {
                    instance = gameObject.AddComponent<NetWorkSocket>();
                    GameObject.DontDestroyOnLoad(gameObject);
                }
                else
                {
                    instance = gameObject.GetComponent<NetWorkSocket>();
                }
            }
            return instance;
        }
    }

    

    private Socket client;

    //������Ϣ
    private Queue<byte[]> m_SendQueue = new Queue<byte[]>();
    private Action m_CheckSendQueue;
    //������Ϣ
    /// <summary>
    /// �������ݻ�����
    /// </summary>
    private byte[] m_ReceiveBuff = new byte[10240];
    private Queue<byte[]> m_ReciveQueue = new Queue<byte[]>();
    private int m_ReceiveCount = 0;

    /// <summary>
    /// 
    /// </summary>
    MemoryStreamTool ms = new MemoryStreamTool();
    //ѹ������ĳ��Ƚ���
    private const int m_CompressLen = 200000;
    /// <summary>
    /// ���ӳɹ���Ӧ�¼�
    /// </summary>
    public Action ConnectSuccessEvent;

    private void Update()
    {
        while (true) 
        {
            if (m_ReceiveCount <= 5)
            {
                m_ReceiveCount++;
                lock (m_ReciveQueue)
                {
                    if (m_ReciveQueue.Count > 0)
                    {
                        //�õ������е����ݰ�
                        byte[] buffer = m_ReciveQueue.Dequeue();
                        GetBuffer(buffer);


                        ////���֮�������
                        //byte[] bufferNew = new byte[buffer.Length - 3];

                        //bool isCompress = false;
                        //ushort crc = 0;

                        //using (MemoryStreamTool ms = new MemoryStreamTool(buffer))
                        //{
                        //    isCompress = ms.ReadBool();
                        //    crc = ms.ReadUShort();
                        //    ms.Read(bufferNew, 0, bufferNew.Length);
                        //}

                        ////��crc
                        //int newCrc = Crc16.CalculateCrc16(bufferNew);

                        //if (newCrc == crc)
                        //{
                        //    //��� �õ�ԭʼ����
                        //    bufferNew = SecurityUtil.Xor(bufferNew);

                        //    if (isCompress)
                        //    {
                        //        bufferNew = ZlibHelper.DeCompressBytes(bufferNew);
                        //    }

                        //    ushort protoCode = 0;
                        //    byte[] protoContent = new byte[bufferNew.Length - 2];
                        //    using (MemoryStreamTool ms = new MemoryStreamTool(bufferNew))
                        //    {
                        //        //Э����
                        //        protoCode = ms.ReadUShort();
                        //        ms.Read(protoContent, 0, protoContent.Length);                               
                        //    }
                        //    ((MsgSystem)GameManager.Instance.GameSystemDic[SystemType.Msg]).FireNetEvent(protoCode, protoContent);
                        //}



                        //δ����Э��
                        //ushort protoCode = 0;
                        //byte[] protoContent = new byte[buffer.Length - 2];
                        //using (MemoryStreamTool ms2 = new MemoryStreamTool(buffer))
                        //{
                        //    protoCode = ms2.ReadUShort();
                        //    ms2.Read(protoContent, 0, protoContent.Length);
                        //}
                        //((MsgSystem)GameManager.Instance.GameSystemDic[SystemType.Msg]).FireNetEvent(protoCode, protoContent);

                        //δ�����¼�Э��
                        //if (protoCode == 10000)
                        //{
                        //    User_LoginProto user_LoginProto = User_LoginProto.GetProto(protoContent);
                        //    Debug.Log("Id = " + user_LoginProto.id);
                        //    Debug.Log("username = " + user_LoginProto.username);
                        //    //user_LoginProto.username = "����";
                        //    //this.SendMsg(user_LoginProto.ToArray());
                        //}


                        //��ͨ���ݲ���
                        //using (MemoryStreamTool ms2 = new MemoryStreamTool(buffer))
                        //{
                        //    string msg = ms2.ReadUTF8String();
                        //    Debug.Log(msg);
                        //}
                        ////Test
                        //using (MemoryStreamTool ms = new MemoryStreamTool())
                        //{
                        //    ms.WriteUTF8String("ѭ��-----");
                        //    NetWorkSocket.Instance.SendMsg(ms.ToArray());
                        //}
                    }
                    else 
                    {
                        break;
                    }
                }
            }
            else
            {
                m_ReceiveCount = 0;
                break;
            }
        }

    }


    public void Connect(string ip, int port)
    {
        if (client != null && client.Connected) return;

        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            m_CheckSendQueue = OnCheckSendQueueCallBack;
            ReciveMsg();
            Debug.Log("���ӳɹ�");
            if (ConnectSuccessEvent != null) 
            {
                ConnectSuccessEvent();
            }
        }
        catch
        {
            Debug.Log("����ʧ��");
        }
    }

    private void Send(byte[] byteBuffer)  
    {
        client.BeginSend(byteBuffer, 0, byteBuffer.Length, SocketFlags.None, SendCallback, client);
    }

    private void SendCallback(IAsyncResult ar)
    {
        client.EndSend(ar);
        OnCheckSendQueueCallBack();
    }






    private void OnCheckSendQueueCallBack()
    {
        lock (m_SendQueue) 
        {
            if (m_SendQueue.Count > 0) 
            {
                Send(m_SendQueue.Dequeue());
            }
        }
    }
    /// <summary>
    /// �������ݰ�
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private void GetBuffer(byte[] buffer)
    {
        //���֮�������
        byte[] bufferNew = new byte[buffer.Length - 3];

        bool isCompress = false;
        ushort crc = 0;

        using (MemoryStreamTool ms = new MemoryStreamTool(buffer))
        {
            isCompress = ms.ReadBool();
            crc = ms.ReadUShort();
            ms.Read(bufferNew, 0, bufferNew.Length);
        }

        //��crc
        int newCrc = Crc16.CalculateCrc16(bufferNew);

        if (newCrc == crc)
        {
            //��� �õ�ԭʼ����
            bufferNew = SecurityUtil.Xor(bufferNew);

            if (isCompress)
            {
                bufferNew = ZlibHelper.DeCompressBytes(bufferNew);
            }

            ushort protoCode = 0;
            byte[] protoContent = new byte[bufferNew.Length - 2];
            using (MemoryStreamTool ms = new MemoryStreamTool(bufferNew))
            {
                //Э����
                protoCode = ms.ReadUShort();
                ms.Read(protoContent, 0, protoContent.Length);


            }
         ((MsgSystem)GameManager.Instance.GameSystemDic[SystemType.Msg]).FireNetEvent(protoCode, protoContent);
        }
    }


    /// <summary>
    /// ��װ���ݱ�
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private byte[] MakeBuffer(byte[] data) 
    {
        byte[] retBuffer = null;

        //1.������ݰ��ĳ��� ������m_CompressLen �����ѹ��
        bool isCompress = data.Length > m_CompressLen ? true : false;
        if (isCompress)
        {
            data = ZlibHelper.CompressBytes(data);
        }

        //2.���
        data = SecurityUtil.Xor(data);

        //3.CrcУ�� ѹ�����
        ushort crc = Crc16.CalculateCrc16(data);

        using (MemoryStreamTool ms = new MemoryStreamTool())
        {
            ms.WriteUShort((ushort)(data.Length + 3));
            ms.WriteBool(isCompress);
            ms.WriteUShort(crc);
            ms.Write(data, 0, data.Length);

            retBuffer = ms.ToArray();
        }
        return retBuffer;

        //δ����Э���װ
        //byte[] readBuffer = null;
        //using (MemoryStreamTool ms = new MemoryStreamTool()) 
        //{
        //    ms.WriteUShort((ushort)data.Length);
        //    ms.Write(data,0, data.Length);
        //    readBuffer = ms.ToArray();
        //}
        //return readBuffer;
    }

    public void SendMsg(byte[] data) 
    {
        //byte[] data = Encoding.UTF8.GetBytes(msg);

        byte[] sendBuffer = MakeBuffer(data);
        //for (int i = 0; i < sendBuffer.Length; i++)
        //{
        //    Debug.Log(sendBuffer[i].ToString() + "\n");
        //}
        lock (m_SendQueue) 
        {
            m_SendQueue.Enqueue(sendBuffer);
            m_CheckSendQueue.BeginInvoke(null,null);
        }
        
    }









    private void ReciveMsg()
    {
        client.BeginReceive(m_ReceiveBuff, 0, m_ReceiveBuff.Length, SocketFlags.None, ReceiveCallback, client);
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            int length = client.EndReceive(ar);
            if (length > 0)
            {
                ms.Position = ms.Length;
                ms.Write(m_ReceiveBuff, 0, length);
                if (ms.Length > 2)
                {
                    while (true)
                    {
                        ms.Position = 0;
                        int curMsgLength = ms.ReadUShort();
                        int curFullLength = 2 + curMsgLength;

                        if (ms.Length >= curFullLength)
                        {
                            byte[] buffer = new byte[curMsgLength];
                            ms.Position = 2;
                            ms.Read(buffer, 0, curMsgLength);

                            lock (m_ReciveQueue) 
                            {
                                m_ReciveQueue.Enqueue(buffer);
                            }
                            

                            //��ͨ���ݲ���
                            //using (MemoryStreamTool ms2 = new MemoryStreamTool(buffer))
                            //{
                            //    string msg = ms2.ReadUTF8String();
                            //    Debug.Log(msg);
                            //}


                            int remainLength = (int)ms.Length - curFullLength;
                            if (remainLength > 0)
                            {
                                ms.Position = curFullLength;
                                byte[] remainBuff = new byte[remainLength];
                                ms.Read(remainBuff, 0, remainLength);

                                ms.Position = 0;
                                ms.SetLength(0);

                                ms.Write(remainBuff, 0, remainBuff.Length);
                                remainBuff = null;
                            }
                            else
                            {
                                ms.Position = 0;
                                ms.SetLength(0);
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                ReciveMsg();
            }
            else
            {
                Debug.Log("�������ѶϿ�...");
            }


        }
        catch
        {
            Debug.Log("�������ѶϿ�...");
        }

    }










    private void OnDestroy()
    {
        if (client != null && client.Connected) 
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }

}
