using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace HD
{
  public class UdpConnectedClient
  {
    #region Data

    public float x=0,y=0,z=0,yaw=0,pitch=0,roll=0,battery;

    private bool _isupdate = false;

    private readonly UdpClient _connection;

    public GameObject player;
    public Transform playerTransform;
    
    
    //private static string path = @"ScenarioLogs/text";
    //private FileInfo fileInf = new FileInfo(path);
    #endregion

    #region Init
    public UdpConnectedClient(IPAddress ip = null) {
      
      /*
      using(StreamWriter sw = new StreamWriter((path),false,System.Text.Encoding.Default))
      {
        sw.WriteLine("its a file!");
      }
      */
      
        
      _connection = new UdpClient(Globals.PortUdp);
      
      player = GameObject.Find("player");
      playerTransform = player.GetComponent<Transform>();

      Debug.Log(player);

      //Debug.Log("connect with port " + Globals.PortUdp + " creating");
      
      _connection.BeginReceive(OnReceive, null);
    }

    public void Update()
    {
      
      if (_isupdate)
      {
        playerTransform.position = new Vector3(-3* z, 3*y, 3*x);
        playerTransform.rotation =
          Quaternion.Euler(-180 * roll / 3.14f, 180 * pitch / 3.14f - 90, -180 * yaw / 3.14f);
        _isupdate = false;
      }
      
    }
    
    
    public void Close() {
      _connection.Close();
    }
    #endregion

    #region API
    void OnReceive(IAsyncResult ar) {
      IPEndPoint ipEndpoint = null;
        byte[] data = _connection.EndReceive(ar, ref ipEndpoint);
        
        UdpChat.AddClient(ipEndpoint);
        if (!_isupdate)
        {
          try
          {
            z = -BitConverter.ToSingle(data, 0);
            x = BitConverter.ToSingle(data, 4);
            y = BitConverter.ToSingle(data, 8);
            pitch = -BitConverter.ToSingle(data, 12);
            yaw = BitConverter.ToSingle(data, 16);
            roll = BitConverter.ToSingle(data, 20); //TODO(me): NaN check
            battery = BitConverter.ToSingle(data, 24);

            /*
            using(StreamWriter sw = new StreamWriter((path),true,System.Text.Encoding.Default))
            {
              sw.WriteLine("data: x =  "+x + ";y = "+ y + ";z = "+ z+";p = " + pitch + "; yaw =  " + yaw  + "; roll = "+roll);
            }
            */
            
          }
          catch (Exception e)
          {
            Debug.Log(e);
          }
          _isupdate = true;
        }
        
        _connection.BeginReceive(OnReceive, null);
      
    }

    #endregion
  }
}
