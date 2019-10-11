using System;
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

    public bool isServer;
    
    public IPAddress serverIp;

    private TcpConnectedClient _connectedClient;
    
    #endregion


    #region Unity Events

    public void Awake()
    {
      instance = this;

      try {
        TcpClient client = new TcpClient();
        _connectedClient = new TcpConnectedClient(client);
        client.BeginConnect(serverIp, Globals.Port, (ar) => _connectedClient.EndConnect(ar), null);
      }
      catch (ObjectDisposedException e)
      {
        Debug.Log(e);
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
