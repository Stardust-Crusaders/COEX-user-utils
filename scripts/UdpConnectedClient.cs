using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

namespace HD
{
  public class UdpConnectedClient
  {
    #region Data

    public float x,y,z,yaw,pitch,roll;

    //public GameObject copter;
    /// <summary>
    /// For Clients, the connection to the server.
    /// For Servers, the connection to a client.
    /// </summary>
    readonly UdpClient connection;
    #endregion

    #region Init
    public UdpConnectedClient(IPAddress ip = null)
    {

      //copter = GameObject.FindGameObjectWithTag("Player");
      if(UDPChat.instance.isServer)
      {
        connection = new UdpClient(Globals.portudp);
      }
      else
      {
        connection = new UdpClient(); // Auto-bind port
      }
      connection.BeginReceive(OnReceive, null);
    }

    public void Close()
    {
      connection.Close();
    }
    #endregion

    
    public void Update()
    {
        
    }


    #region API
    void OnReceive(IAsyncResult ar)
    {
      try
      {
        IPEndPoint ipEndpoint = null;
        byte[] data = connection.EndReceive(ar, ref ipEndpoint);
        try
        {
        z = -BitConverter.ToSingle(data,0);
        x = BitConverter.ToSingle(data,4);
        y = BitConverter.ToSingle(data,8);
        pitch = BitConverter.ToSingle(data,12);
        yaw = BitConverter.ToSingle(data,16);
        roll = BitConverter.ToSingle(data,20); //TODO(me): NaN check
        }
        catch(SocketException e)
        {

        }
        
      /* 
        //Debug.Log(data);
        //UDPChat.AddClient(ipEndpoint)


        Debug.Log("x:" + x + "y:" + y + "z:" + z);

        //UDPChat.messageToDisplay += message + Environment.NewLine;

        if(UDPChat.instance.isServer)
        {
          //UDPChat.BroadcastChatMessage(message);
        }
      */
      }
      catch(SocketException e)
      {
        // This happens when a client disconnects, as we fail to send to that port.
      }
      
      connection.BeginReceive(OnReceive, null);
      
    }

    

    internal void Send(string message, IPEndPoint ipEndpoint)
    {
      byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
      connection.Send(data, data.Length, ipEndpoint);
    }
    #endregion
  }
}
