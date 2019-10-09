using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net;

namespace HD {
  public class scr_controller: MonoBehaviour
  {
    public GameObject tcpClient;

    public GameObject udpServer;
    public GameObject[] button = new GameObject[5];


    public void Start()
    {
      Globals.isServer = false;

      IPAddress ip = IPAddress.Parse(Globals.IP);

      //udp.GetComponent<UDPChat>().serverIp = ip;
      tcpClient.GetComponent<TCPChat>().serverIp = ip;

        tcpClient.SetActive(true);
      udpServer.SetActive(true);
      

    }

    public void Update()
    {
        if(Input.GetKeyDown("w")) {
            button[0].GetComponent<scr_spritechange>().ChangeSprite(1);
            TCPChat.instance.Send("1\0");
        }

        if(Input.GetKeyUp("w")) {
            button[0].GetComponent<scr_spritechange>().ChangeSprite(0);
        }

        if(Input.GetKeyDown("a")) {
            button[1].GetComponent<scr_spritechange>().ChangeSprite(1);
            TCPChat.instance.Send("2\0");
        }

        if(Input.GetKeyUp("a")) {
            button[1].GetComponent<scr_spritechange>().ChangeSprite(0);
        }

        if(Input.GetKeyDown("s")) {
            button[2].GetComponent<scr_spritechange>().ChangeSprite(1);
            TCPChat.instance.Send("3\0");
        }

        if(Input.GetKeyUp("s")) {
            button[2].GetComponent<scr_spritechange>().ChangeSprite(0);
        }

        if(Input.GetKeyDown("d")) {
            button[3].GetComponent<scr_spritechange>().ChangeSprite(1);
            TCPChat.instance.Send("4\0");
        }

        if(Input.GetKeyUp("d")) {
            button[3].GetComponent<scr_spritechange>().ChangeSprite(0);
        }

        if(Input.GetKeyDown("f")) {
            button[4].GetComponent<scr_spritechange>().ChangeSprite(1);
            TCPChat.instance.Send("11\0");
        }

        if(Input.GetKeyUp("f")) {
            button[4].GetComponent<scr_spritechange>().ChangeSprite(0);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            //button[4].GetComponent<scr_spritechange>().ChangeSprite(1);
            TCPChat.instance.Send("6\0");
        }

        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            //button[4].GetComponent<scr_spritechange>().ChangeSprite(1);
            TCPChat.instance.Send("5\0");
        }
    }


  }
}