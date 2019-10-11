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

    public static TcpConnectedClient connectedClient;
    
    #endregion

    #region Unity Events

    public void Start()
    {
      instance = this;
      
      try {
        TcpClient client = new TcpClient();
        connectedClient = new TcpConnectedClient(client);
        IAsyncResult result = client.BeginConnect(serverIp, Globals.Port, (ar) => connectedClient.EndConnect(ar), client);
        
        result.AsyncWaitHandle.WaitOne( 5000, true );

        if(!client.Connected)
        {
          Debug.Log("Failed to connect server.");
        }
      }
      catch (SocketException e)
      {
        Debug.Log("error" + e);
      }
    }

    protected void OnApplicationQuit()
    {
      connectedClient.Close();
    }
    
    #endregion
    

    #region API

    internal void Send(string message) { 
      connectedClient.Send(message);
    }
    
    
    internal void SendChar(char[] message,int size) { 
      connectedClient.SendChar(message,size);
    }
    
    
    
    #endregion
  }
}
