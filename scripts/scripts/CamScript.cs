using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public GameObject receiver;

    void Update()
    {
        this.transform.position = receiver.transform.position;
    }
}
