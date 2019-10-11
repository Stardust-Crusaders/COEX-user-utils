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
    /// <summary>
    /// For Clients, the connection to the server.
    /// For Servers, the connection to a client.
    /// </summary>
    readonly TcpClient _connection;

    readonly byte[] _readBuffer = new byte[5000];

    NetworkStream Stream {
      get {
        return _connection.GetStream();
      }
    }
    #endregion

    #region Init
    public TcpConnectedClient(TcpClient tcpClient) {
      this._connection = tcpClient;
      this._connection.NoDelay = true; // Disable Nagle's cache algorithm
    }

    internal void Close() {
      _connection.Close();
    }
    #endregion

    #region Async Events
    
    
    void OnRead(IAsyncResult ar)
    {
      int length = Stream.EndRead(ar);
      Debug.Log(length);
      Debug.Log("passed");
      if(length <= 0)
      { 
        // Connection closed
        Debug.Log("Connection end");
        _connection.Close();
        return;
      }
      
      Stream.BeginRead(_readBuffer, 0, _readBuffer.Length, OnRead, null);
    }
    
    internal void EndConnect(IAsyncResult ar)
    {
      _connection.EndConnect(ar);
      
      Debug.Log("try connect");
      Stream.BeginRead(_readBuffer, 0, _readBuffer.Length, OnRead, null);
    }
    #endregion
    

    #region API
    internal void Send(string message) {

      char[] buf = message.ToCharArray();
      byte[] buffer = System.Text.Encoding.ASCII.GetBytes(buf);
      Stream.Write(buffer, 0, buffer.Length);

    }
    #endregion
  }
}
