using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class scr_spritechange: MonoBehaviour
{
    public Sprite[] spr = new Sprite[2];

    public void ChangeSprite(int num)
    {
        this.GetComponent<Image>().overrideSprite = spr[num];
    }

}
