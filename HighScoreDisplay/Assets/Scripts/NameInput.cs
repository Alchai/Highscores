using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class NameInput : MonoBehaviour
{
    public string myName;
    private Highscore hs;
    TouchScreenKeyboard ts;
    private GUIText enterName;
    private GUIText done;
    private GUIText nameInput;
    public bool doneInputting = false;
    private string tempName = "";

    void Start()
    {
        done = GameObject.Find("Done").guiText;
        enterName = GameObject.Find("EnterName").guiText;
        hs = GetComponent<Highscore>();
        nameInput = GameObject.Find("nameInput").guiText;

        GetNameInput();
    }

    public void GetNameInput()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            StartCoroutine("getInputMobile");
        else
            StartCoroutine("getInputPC");
    }

    private IEnumerator getInputPC()
    {
        done.enabled = true;
        enterName.enabled = true;
        nameInput.enabled = true;

        nameInput.text = "(Use Keyboard To Enter Text)";
        doneInputting = false;

        while (!doneInputting)
        {
            yield return new WaitForEndOfFrame();
        }

        done.enabled = false;
        enterName.enabled = false;
        nameInput.enabled = false;
        tempName = "";
        hs.myName = nameInput.text;
        hs.myScore = "123";
        hs.PostScores();
    }


    void Update()
    {
        if (!doneInputting && Input.anyKey && tempName.Length < 15)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (tempName.Length > 0)
                {
                    tempName = tempName.Remove(tempName.Length - 1);
                    nameInput.text = tempName;
                }
            }
            if (!Input.GetKey(KeyCode.Backspace))
            {
                nameInput.text = "";
                tempName += Input.inputString;
                nameInput.text = tempName;
            }
        }

    }

    private IEnumerator getInputMobile()
    {
        ts = TouchScreenKeyboard.Open("(Enter Name)", TouchScreenKeyboardType.Default, false, false, false, false, "");

        while (!ts.done)
        {
            if (ts.text.Length > 15)
                ts.text.Remove(15);

            yield return new WaitForEndOfFrame();
        }

        hs.myName = ts.text;
    }
}
