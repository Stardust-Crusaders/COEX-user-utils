using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = System.Drawing.Color;

public class colorChange : MonoBehaviour
{
    private Image _componentImage;

    private readonly UnityEngine.Color[] _colors = new UnityEngine.Color[2];
    
    public void Start()
    {
        _componentImage = this.GetComponent<Image>();
        _colors[0]=  UnityEngine.Color.green;
        _colors[1]=  UnityEngine.Color.red;
    }

    public void ChangeColor(int num)
    {
        _componentImage.color = _colors[num];
    }
}
