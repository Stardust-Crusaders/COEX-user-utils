﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace HD
{
  public class TcpChat : MonoBehaviour
  {

    #region Data
    public static TcpChat instance;

    public bool isConnected;

    public bool isServer;
    
    public IPAddress serverIp;

    private TcpConnectedClient _connectedClient;
    
    #endregion


    #region Unity Events

    public void Start()
    {
      instance = this;

      try {
        TcpClient client = new TcpClient();
        _connectedClient = new TcpConnectedClient(client);
        IAsyncResult result = client.BeginConnect(serverIp, Globals.Port, (ar) => _connectedClient.EndConnect(ar), client);
        
        result.AsyncWaitHandle.WaitOne( 5000, true );

        if(!client.Connected)
        {
          isConnected = false;
          client.Close();
          Debug.Log("Failed to connect server.");
        }
        else
        {
          isServer = true;
        }
      }
      catch (SocketException e)
      {
        Debug.Log("error" + e);
      }

    }

    protected void OnApplicationQuit()
    {
      _connectedClient.Close();
    }

    #endregion
    

    #region API

    internal void Send(string message) { 
      _connectedClient.Send(message);
    }
    
    #endregion
  }
}
