using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;

namespace HD
{
  public class UdpChat : MonoBehaviour
  {
    #region Data
    
    public static UdpChat instance;
    
    public bool isServer;

    public IPAddress serverIp = null;
    
    List<IPEndPoint> _clientList = new List<IPEndPoint>();
    
    public UdpConnectedClient connection;
    #endregion

    #region Unity Events
    public void Start() {
      instance = this;
      this.isServer = true;
      
      connection = new UdpConnectedClient();
    }


    public void Update()
    {
      connection.Update();
    }
      
      
    internal static void AddClient(IPEndPoint ipEndpoint) {
      if(instance._clientList.Contains(ipEndpoint) == false) { // If it's a new client, add to the client list
        Debug.Log($"Connect to {ipEndpoint}");
        instance._clientList.Add(ipEndpoint);
      }
    }
    
    internal static void RemoveClient(IPEndPoint ipEndpoint) { 
      instance._clientList.Remove(ipEndpoint);
    }

    private void OnApplicationQuit()
    {
      connection.Close();
    }
    
    #endregion

  }
}
