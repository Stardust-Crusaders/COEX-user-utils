using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    private string _dataMes;
    private System.DateTime _curTime;
    public GameObject data;
    private UnityEngine.UI.Text _compText;

    void Start()
    {
        _compText = data.GetComponent<Text>();
    }
    void Update()
    {
        _curTime = System.DateTime.Now;
        
        if(_curTime.Minute < 10) {
            _dataMes = _curTime.Hour + ":0" + _curTime.Minute;
        }
        else {
            _dataMes = _curTime.Hour + ":" + _curTime.Minute; 
        }
        
        _compText.text = _dataMes;
    }


}
