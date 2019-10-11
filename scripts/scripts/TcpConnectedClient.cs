using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net;

namespace HD
{
  public class TcpConnectedClient
  {
    #region Data
    readonly TcpClient _connection;
    
    NetworkStream Stream {
      get {
        return _connection.GetStream();
      }
    }
    #endregion

    #region Init
    public TcpConnectedClient(TcpClient tcpClient) {
      this._connection = tcpClient;
      this._connection.NoDelay = true; 
    }

    internal void Close() {
      _connection.Close();
    }
    #endregion

    #region Async Events

    internal void EndConnect(IAsyncResult ar)
    {
      _connection.EndConnect(ar);
      Debug.Log("Client Connected");
    }
    
    #endregion
    
    #region API

    internal void Send(string message) {
      char[] buf = message.ToCharArray();
      byte[] buffer = System.Text.Encoding.ASCII.GetBytes(buf);
      Stream.Write(buffer, 0, buffer.Length);
    }
    
    
    internal void SendChar(char[] arr,int size)
    {
      byte[] buffer = new byte[size];
      for (int i = 0; i < size; i++)
      {
        buffer[i] = (byte)arr[i];
      }
      Stream.Write(buffer, 0, buffer.Length);
    }

    #endregion
  }
}
