using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPControl : IControlScheme
{
    private UdpClient[] udpClient = new UdpClient[2];
    private IPEndPoint[] endPoint = new IPEndPoint[2];

    public delegate void MoveDelegate(float move);
    public delegate void RotateDelegate(float rotate);

    public MoveDelegate OnSetMove;
    public RotateDelegate OnSetRotate;

    public UDPControl(string ipAddress, int port)
    {
        udpClient[0] = new UdpClient(port);
        udpClient[1] = new UdpClient(port+1);
        endPoint[0] = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        endPoint[1] = new IPEndPoint(IPAddress.Parse(ipAddress), port+1);
    }

    public void UpdateControl()
    {
        if (udpClient[0].Available > 0)
        {
            byte[] data = udpClient[0].Receive(ref endPoint[0]);
            ParseReceivedData(data);
            Debug.Log("receive!");
            Debug.Log(data[0]);
        }
        if (udpClient[1].Available > 0)
        {
            byte[] data = udpClient[1].Receive(ref endPoint[1]);
            ParseReceivedData(data);
            Debug.Log("receive!");
            Debug.Log(data[0]);
        }
    }

    private void ParseReceivedData(byte[] receivedData)
    {
        // if ((byte)((receivedData[0] >> 0) & 0b00000001) == 1)
        // {
        //     OnSetMove?.Invoke(1.0f);
        // }
        // else
        // {
        //     OnSetMove?.Invoke(0.0f);
        // }

        if ((byte)((receivedData[0] >> 1) & 0b00000001) == 1)
        {
            OnSetRotate?.Invoke(1.0f);
        }
        else if ((byte)((receivedData[0] >> 2) & 0b00000001) == 1)
        {
            OnSetRotate?.Invoke(-1.0f);
        }
        else
        {
            OnSetRotate?.Invoke(0.0f);
        }
    }
}