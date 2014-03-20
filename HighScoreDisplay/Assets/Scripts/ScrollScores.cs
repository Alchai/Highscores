using UnityEngine;
using System.Collections;

public class ScrollScores : MonoBehaviour
{
    private ScoreDisplayer scoreScript;

    void Start()
    {
        scoreScript = GameObject.Find("Scores").GetComponent<ScoreDisplayer>();
    }

    void OnMouseDown()
    {
        if (name.Contains("Up"))
            scoreScript.ScrollScores(true);
        if (name.Contains("Down"))
            scoreScript.ScrollScores(false);
    }
}
