using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net;

namespace HD {
  public class Controller: MonoBehaviour
  {
    public GameObject tcpClient;

    public GameObject udpServer;
    public GameObject[] button = new GameObject[5];
   //private readonly SpriteChange[] _spritesScripts = new SpriteChange[5];

    private readonly KeyCode[] _keys = new KeyCode[7];
    private readonly string[] _mesKeys = new string[7];
    
    public void Start()
    { 
        IPAddress ip = IPAddress.Parse(Globals.IP);
        
        KeyDefinition();
        
        tcpClient.GetComponent<TcpChat>().serverIp = ip;

        tcpClient.SetActive(true);
        udpServer.SetActive(true);

        /*
        for (int i = 0; i < 7; i++)
        {
            _spritesScripts[i] = button[i].GetComponent<SpriteChange>();
        }
        */

    }

    public void Update()
    {    
        
        for(int i = 0; i < 7; i++) {
            if (Input.GetKeyDown(_keys[i])) {
                //_spritesScripts[i].ChangeSprite(1);
                TcpChat.instance.Send(_mesKeys[i]);
            }
        }
    }

    private void KeyDefinition()
    {
        _keys[0] = KeyCode.W;
        _keys[1] = KeyCode.A;
        _keys[2] = KeyCode.S;
        _keys[3] = KeyCode.D;
        _keys[4] = KeyCode.F;
        _keys[5] = KeyCode.LeftShift;
        _keys[6] = KeyCode.LeftControl;

        _mesKeys[0] = "1\0";
        _mesKeys[1] = "2\0";
        _mesKeys[2] = "3\0";
        _mesKeys[3] = "4\0";
        _mesKeys[4] = "11\0";
        _mesKeys[5] = "6\0";
        _mesKeys[6] = "5\0";
    }

  }
}