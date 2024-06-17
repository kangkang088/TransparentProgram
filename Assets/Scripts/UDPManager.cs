using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class UDPManager : MonoBehaviour
{
    public UnityEvent<string> OnReceivedStringDataEvent = new();

    private byte[] data = new byte[1024];
    private Socket socketUDP;

    public void StartNetworkTask()
    {
        socketUDP = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
        EndPoint endPoint = new IPEndPoint(IPAddress.Parse(GetLocalIPAddress()),8081);
        socketUDP.Bind(endPoint);
        EndPoint remoteEndPoin = new IPEndPoint(IPAddress.Any,0);
        socketUDP.BeginReceiveFrom(data,0,data.Length,SocketFlags.None,ref remoteEndPoin,ReceiveFromOver,(socketUDP, remoteEndPoin));
        print($"<color=yellow><b>UDP 启动完成，当前监听IP端口：{GetLocalIPAddress()}:{8081}</b></color>");
    }

    private void ReceiveFromOver(IAsyncResult result)
    {
        (Socket s, EndPoint remotePoint) info = ((Socket, EndPoint))(result.AsyncState);
        int len = info.s.EndReceiveFrom(result,ref info.remotePoint);
        var content = Encoding.UTF8.GetString(data,0,len);
        OnReceivedStringDataEvent.Invoke(content);
        info.s.BeginReceiveFrom(data,0,data.Length,SocketFlags.None,ref info.remotePoint,ReceiveFromOver,(socketUDP, info.remotePoint));
    }

    protected void OnDestroy()
    {
        socketUDP.Shutdown(SocketShutdown.Both);
        socketUDP.Close();
        socketUDP.Dispose();
    }

    private string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach(var ipAddress in host.AddressList)
        {
            if(ipAddress.AddressFamily == AddressFamily.InterNetwork)
            {
                return ipAddress.ToString();
            }
        }
        return null;
    }
}