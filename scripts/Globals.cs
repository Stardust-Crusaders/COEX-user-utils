using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net;

namespace cmd {
    enum commands {
        stop = 0,
        step_w = 1,
        step_a = 2,
        step_s = 3,
        step_d = 4,
        step_ctrl = 5,
        step_shift = 6,
        vel_w = 7,
        vel_a = 8,
        vel_s = 9,
        vel_d = 10,
        flip = 11,
    };
}

namespace HD
{
  public static class Globals
  {
    public const int port = 7331;
    public const int portudp = 7332;
        public const string IP = "192.168.0.201";
    public static bool isServer;
  }

  
}
