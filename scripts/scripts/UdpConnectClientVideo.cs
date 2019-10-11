using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using UnityEngine.UI;

namespace HD
{
    public class UdpConnectedClientVideo
    {
        #region Data

        public float x,y,z,yaw,pitch,roll;

        private int _packSize = 24;
        private int _numInPack;
        private int _lastNum = -1;//packetsize 4096 byte

        private byte[] _fullPackages = new byte[24 * 4088];
        private byte[] _fullPackagesbuf = new byte[24 * 4088];
        private bool _isbuf;

        private static string path = @"ScenarioLogs/picture.jpg";
        private FileInfo fileInf = new FileInfo(path);

        //= new byte[4096][]; 
        private int _totalSize;
        private bool _isreceiving;

        private int i = 0;

        private int time;
        //private Texture2D tex = new Texture2D(0,0);
        private GameObject videoTex;
    
        private UnityEngine.UI.RawImage _sprComp;

        //private Camera _cameraBack;
        //private Renderer _renderer;

        private bool _isChanged;



        private readonly UdpClient _connection;
        #endregion

        #region Init
        public UdpConnectedClientVideo(IPAddress ip = null) {
        
            _connection = new UdpClient(Globals.PortUdp);

            videoTex = GameObject.Find("Video");

            //_cameraBack = videoTex.GetComponent<Camera>();

            //videoTex.transform.Rotate(0, 0, 45);
            Debug.Log(videoTex);
            
            _sprComp = videoTex.GetComponent<RawImage>();
            //_renderer = videoTex.GetComponent<Renderer>();
            _connection.BeginReceive(OnReceive, null);
            //_sprComp.texture = Texture2D.normalTexture;
            
        }

        
        
        public void Close() {
            _connection.Close();
        }
        #endregion
        

        #region API
        void OnReceive(IAsyncResult ar) {
            IPEndPoint ipEndpoint = null;
            
            byte[] data = _connection.EndReceive(ar, ref ipEndpoint);
            
            
            int bufTotal = BitConverter.ToInt32(data, 0);
                int buf = BitConverter.ToInt32(data, 4);
                //Debug.Log("B=" + bufTotal + "; b = " + buf + "; len = " + data.Length);

                //Debug.Log(data.Length);
                //Debug.Log(buf+ " " +_lastNum);
                    
                    
                if (buf < _lastNum) {
                    _isChanged = true;
                    _totalSize = _packSize;
                    _packSize = bufTotal;
                    if (_isbuf) {
                        _fullPackages = new byte[4088 * _packSize];
                        _isbuf = false;
                    }
                    
                    else {
                        _fullPackagesbuf = new byte[4088 * _packSize];
                        _isbuf = true;
                    }
                }
                
                for (int i = 0; i < 4088; i++) {
                    if (!_isbuf) {
                        _fullPackages[i + 4088 * buf] = data[8 + i];
                    }
                    else {
                        _fullPackagesbuf[i + 4088 * buf] = data[8 + i];
                    }
                }
                _lastNum = buf;
            
                _connection.BeginReceive(OnReceive, null);
        }
        
        public void Update()
        {
            if (_isChanged)
            {
                
                Debug.Log(_fullPackagesbuf.Length);
                
                
                var tex = new Texture2D(2, 2);
                if (_isbuf)
                {
                    //Debug.Log(_fullPackagesbuf.Length);
                    tex.LoadImage(_fullPackages);
                    tex.Apply();

                    //_cameraBack. = tex;
                    //_renderer.material.mainTexture = tex;
                    _sprComp.texture = tex;

                }
                else
                {
                    //Debug.Log(_fullPackages.Length);
                    tex.LoadImage(_fullPackagesbuf);
                    tex.Apply();
                    //_renderer.material.mainTexture = tex;
                    _sprComp.texture = tex;
                }
                
                _isChanged = false;
            }
            //Debug.Log(System.DateTime.Now);
            
        }

        #endregion
    }
}