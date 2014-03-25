using UnityEngine;
using System.Collections;

public class DoneInput : MonoBehaviour {

    private NameInput nameInput;

    void Start()
    {
        nameInput = GameObject.Find("Scores").GetComponent<NameInput>();
    }
    void OnMouseDown()
    {
        nameInput.doneInputting = true;
    }
}
