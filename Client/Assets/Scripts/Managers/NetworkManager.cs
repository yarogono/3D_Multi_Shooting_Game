using ServerCore;
using System.Collections.Generic;
using System;
using System.Net;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using UnityEngine;

public class NetworkManager : CustomSingleton<NetworkManager>
{
    ServerSession _session = new ServerSession();

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        // DNS (Domain Name System)
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

        Connector connector = new Connector();

        connector.Connect(endPoint,
            () => { return _session; },
            1);
    }

    void Update()
    {
        List<PacketMessage> list = PacketQueue.Instance.PopAll();
        if (list.Count != 0)
        {
            foreach (PacketMessage packet in list)
            {
                Action<PacketSession, IMessage> handler = PacketManager.Instance.GetPacketHandler(packet.Id);
                handler?.Invoke(_session, packet.Message);
            }
        }
    }

    public void Send(IMessage packet)
    {
        _session.Send(packet);
    }

    internal void PingCheck(S_Ping pingPacket)
    {
        var clientTimestamp = DateTimeOffset.UtcNow;
        var latency = clientTimestamp - pingPacket.ServerTimestamp.ToDateTimeOffset();
        Debug.Log($"Round-Trip Latency : {latency}");
    }
}
