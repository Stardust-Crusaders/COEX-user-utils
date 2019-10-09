using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;

namespace HD
{
  public class UDPChat : MonoBehaviour
  {
    #region Data
    public static UDPChat instance;



    public bool isServer;

    /// <summary>
    /// IP for clients to connect to. Null if you are the server.
    /// </summary>
    public IPAddress serverIp = null;

    /// <summary>
    /// For Clients, there is only one and it's the connection to the server.
    /// For Servers, there are many - one per connected client.
    /// </summary>
    List<IPEndPoint> clientList = new List<IPEndPoint>();

    /// <summary>
    /// The string to render in Unity.
    /// </summary>
  

    UdpConnectedClient connection;
    #endregion

    #region Unity Events
    public void Awake()
    {


      instance = this;

      if(this.isServer)
      {
        //this.isServer = true;
        connection = new UdpConnectedClient();
      }
      else
      {
        Debug.Log("ya loh");
        connection = new UdpConnectedClient(ip: serverIp);
        AddClient(new IPEndPoint(serverIp, Globals.portudp));
      }
    }

    internal static void AddClient(
      IPEndPoint ipEndpoint)
    {
      if(instance.clientList.Contains(ipEndpoint) == false)
      { // If it's a new client, add to the client list
        UnityEngine.MonoBehaviour.print($"Connect to {ipEndpoint}");
        instance.clientList.Add(ipEndpoint);
      }
    }

    /// <summary>
    /// TODO: We need to add timestamps to timeout and remove clients from the list.
    /// </summary>
    internal static void RemoveClient(
    IPEndPoint ipEndpoint)
    { 
      instance.clientList.Remove(ipEndpoint);
    }

    private void OnApplicationQuit()
    {
      connection.Close();
    }

    protected void Update()
    {
       
        this.transform.position = new Vector3(connection.x, connection.y, connection.z);
        float OY = 180 * connection.pitch / 3.14f - 90;
        float OX = -180 * connection.roll / 3.14f;
        float OZ = -180 * connection.yaw / 3.14f;
        this.transform.rotation = Quaternion.Euler(OX, OY, OZ);
    }
    #endregion

    #region API
    public void Send(
      string message)
    {
      if(isServer)
      {
        //Debug.Log("1");
      }
      
      BroadcastChatMessage(message);
    }

    internal static void BroadcastChatMessage(string message)
    {
      foreach(var ip in instance.clientList)
      {
        instance.connection.Send(message, ip);
      }
    }
    #endregion

    
  }
}
