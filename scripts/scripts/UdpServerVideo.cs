using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;

namespace HD
{
    public class UdpServerVideo: MonoBehaviour
    {
        #region Data
        public static UdpServerVideo instance;
    
        public bool isServer;
        
        public IPAddress serverIp = null;
    
        readonly List<IPEndPoint> _clientList = new List<IPEndPoint>();
    
        public  UdpConnectedClientVideo _connection;
        
        #endregion

        #region Unity Events
        public void Awake() {
            instance = this;
            this.isServer = true;

            Debug.Log("im awake");
            _connection = new UdpConnectedClientVideo();
        }

        public void Update()
        {
            //Debug.Log("1");
            _connection.Update();
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

       
        
        #endregion

    }
}