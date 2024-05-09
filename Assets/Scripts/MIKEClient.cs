using System.Net.Sockets;
using UnityEngine;

public class MIKEClient
{

    public bool clientConnected;
    public TcpClient tcpClient;

    public void CloseClient()
    {

        tcpClient.Close();
        clientConnected = false;

    }

}
