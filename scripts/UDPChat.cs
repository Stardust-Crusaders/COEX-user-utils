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
    
    readonly List<IPEndPoint> _clientList = new List<IPEndPoint>();
    

    private UdpConnectedClient _connection;
    #endregion

    #region Unity Events
    public void Awake() {
      instance = this;
      this.isServer = true;
      
      _connection = new UdpConnectedClient();
      
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
      _connection.Close();
    }

    protected void Update()
    {
       
        this.transform.position = new Vector3(_connection.x, _connection.y, _connection.z);
        float OY = 180 * _connection.pitch / 3.14f - 90;
        float OX = -180 * _connection.roll / 3.14f;
        float OZ = -180 * _connection.yaw / 3.14f;
        this.transform.rotation = Quaternion.Euler(OX, OY, OZ);
    }
    #endregion

  }
}
