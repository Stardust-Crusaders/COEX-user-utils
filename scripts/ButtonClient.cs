using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

namespace HD
{
  public class ButtonClient : MonoBehaviour
  {
    public GameObject tcpClient;

    public void OnClick()
    {
      IPAddress ip = IPAddress.Parse(GameObject.Find("InputFieldIP").GetComponent<InputField>().text);

      tcpClient.GetComponent<TCPChat>().serverIp = ip;

      tcpClient.SetActive(true);

      Destroy(this);

      Debug.Log("11");
    }
  }
}
