using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanv;
    private bool _isActive;
    public GameObject[] buttons;
    private readonly SpriteChange[] _butSprChange = new SpriteChange[5];
    private int _curPos;
    void Start()
    {
        _isActive = false;
        _curPos = 0;
        for (int i = 0; i < 5; i++)
        {
            _butSprChange[i] = buttons[i].GetComponent<SpriteChange>();
        }
        //UpdateCur();
        menuCanv.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _curPos = 0;
            if (_isActive) {
                menuCanv.SetActive(false);
                _isActive = false;
            }
            else {
                menuCanv.SetActive(true);
                _isActive = true;
                UpdateCur();
            }
        }

        if(_isActive) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                _curPos--;

                if (_curPos < 0) {
                    _curPos = 4;
                }

                UpdateCur();

            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                _curPos++;
                if (_curPos > 4) {
                    _curPos = 0;
                }

                UpdateCur();

            }
        }

    }

    private void UpdateCur()
    {
        for (int i = 0; i < 5; i++)
        {
            _butSprChange[i].ChangeSprite(0);
            _butSprChange[_curPos].ChangeSprite(1);
        }
    }
}
