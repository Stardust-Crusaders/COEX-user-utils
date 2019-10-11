using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RecordScript : MonoBehaviour
{

    private bool _isgetvalue = false;
    private float _x, _y, _z, _yaw, _pitch, _roll;
    
    private static string path = @"ScenarioLogs/text";
    private FileInfo fileInf = new FileInfo(path);


    private void Start()
    {
        using(StreamWriter sw = new StreamWriter((path),false,System.Text.Encoding.Default))
        {
            sw.WriteLine("its a file!");
        }
    }

    void FixedUpdate()
    {
        if (_isgetvalue)
        {
            using(StreamWriter sw = new StreamWriter((path),true,System.Text.Encoding.Default))
            {
                sw.WriteLine();
            }
        }
    }

    private void UpdateData(float x,float y, float z, float yaw, float pitch, float roll)
    {
        _isgetvalue = true;
        _x = x;
        _y = y;
        _z = z;
        _yaw = yaw;
        _pitch = pitch;
        _roll = roll;
    }
}
