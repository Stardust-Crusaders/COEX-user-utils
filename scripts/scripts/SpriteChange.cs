using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChange: MonoBehaviour
{
    public Sprite[] spr = new Sprite[2];
    private Image _componentImage;
    
    public void Awake()
    {
        _componentImage = this.GetComponent<Image>();
    }

    public void Start()
    {
        _componentImage = this.GetComponent<Image>();
    }
    
    public void ChangeSprite(int num)
    {
        _componentImage.overrideSprite = spr[num];
    }
}
