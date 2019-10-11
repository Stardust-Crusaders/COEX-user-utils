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
    
    private readonly UdpClient _connection;
    #endregion

    #region Init
    public UdpConnectedClient(IPAddress ip = null) {
        
      _connection = new UdpClient(Globals.PortUdp);
      
      _connection.BeginReceive(OnReceive, null);
    }

    public void Close() {
      _connection.Close();
    }
    #endregion

    #region API
    void OnReceive(IAsyncResult ar) {
      try {
        IPEndPoint ipEndpoint = null;
        byte[] data = _connection.EndReceive(ar, ref ipEndpoint);
        
        try { 
          z = -BitConverter.ToSingle(data,0);
          x = BitConverter.ToSingle(data,4);
          y = BitConverter.ToSingle(data,8);
          pitch = BitConverter.ToSingle(data,12);
          yaw = BitConverter.ToSingle(data,16);
          roll = BitConverter.ToSingle(data,20); //TODO(me): NaN check
        }
        
        catch(SocketException e) {
          Debug.Log("Error NaN" + e);
        }
        
      }
      
      catch(SocketException e) {
        Debug.Log("Error Receive" + e);
      }
      
      _connection.BeginReceive(OnReceive, null);
      
    }

    #endregion
  }
}
