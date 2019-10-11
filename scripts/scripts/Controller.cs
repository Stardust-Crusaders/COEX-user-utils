using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine.UIElements;

namespace HD {
  public class Controller: MonoBehaviour
  {
    public GameObject tcpClient;

    public GameObject udpServer;

    private char _pointNum;

    public char[] _x;
    public char[] _y;
    public char[] _z;

    public GameObject sphere;
    
    private string _message;
        //= "3987789987";

    public void Start()
    {
        IPAddress ip = IPAddress.Parse(Globals.IP);
        tcpClient.GetComponent<TcpChat>().serverIp = ip;

        tcpClient.SetActive(true);
        udpServer.SetActive(true);
        
        TestPoints();
        
        for (int i = 0; i < _pointNum; i++)
        {
            Instantiate(sphere,new Vector3(_x[i] * 3, _z[i] * 3, _y[i] * 3),Quaternion.identity);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Sending();
        }
    }

    private void Sending()
    {
        char[] arr = new char[_pointNum * 3 + 1];
        
        arr[0] = _pointNum;
        for (int i = 0; i < _pointNum; i++)
        {
            arr[1 + i * 3] =_x[i];
            arr[2 + i * 3] = _y[i];
            arr[3 + i * 3] = _z[i];
        }
        
        tcpClient.GetComponent<TcpChat>().SendChar(arr, 3* _pointNum +1);
    }

    private void TestPoints()
    {
        _pointNum = (char)6;

        _x = new char[_pointNum];
        _y = new char[_pointNum];
        _z = new char[_pointNum];
        
        _x[0] = (char)8;
        _y[0] = (char)5;
        _z[0] = (char)1;
        
        _x[1] = (char)7;
        _y[1] = (char)8;
        _z[1] = (char)2;
        
        _x[2] = (char)4;
        _y[2] = (char)3;
        _z[2] = (char)3;

        _x[3] = (char)2;
        _y[3] = (char)8;
        _z[3] = (char)2;
        
        _x[4] = (char)5;
        _y[4] = (char)3;
        _z[4] = (char)2;
        
        _x[5] = (char)8;
        _y[5] = (char)8;
        _z[5] = (char)1;


    }

  }
}