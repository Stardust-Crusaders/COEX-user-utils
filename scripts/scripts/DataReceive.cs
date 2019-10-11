using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HD;
using UnityEngine;
using UnityEngine.UI;

public class DataReceive: MonoBehaviour
{
    public GameObject udpServer;
    private UdpChat _udpChat;

    public GameObject bat;
    private Text _text;

    void Start()
    {
        _udpChat = udpServer.GetComponent<UdpChat>();
        _text = bat.GetComponent<Text>();

    }
    void Update()
    {
        this.transform.position = new Vector3(_udpChat.connection.x, _udpChat.connection.y, _udpChat.connection.z);
        float OY = 180 * _udpChat.connection.pitch / 3.14f - 90;
        float OX = -180 * _udpChat.connection.roll / 3.14f;
        float OZ = -180 * _udpChat.connection.yaw / 3.14f;
        _text.text = _udpChat.connection.battery.ToString();
        this.transform.rotation = Quaternion.Euler(OX, OY, OZ);
    }
}
