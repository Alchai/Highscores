using UnityEngine;
using System.Collections;
using System.Text;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using System;

public class Highscore : MonoBehaviour
{
    [NonSerialized] //this tag makes the following variables not show up in the inspector
    public string secretKey = "12345",
        PostScoreUrl = "http://grastondoc.com/PHP/postScore.php?",
        GetHighscoreUrl = "http://grastondoc.com/PHP/getHighscore.php",
        myName = "Name", myScore = "Score";

    private string WindowTitel = "", DS = "";
    private List<string> names = new List<string>(), scores = new List<string>();
    private ScoreDisplayer scoreDisp;

    public int maxNameLength = 10;
    public int getLimitScore = 15;

    //======= TO DO =============
    // -clean up this script in general
    // -start thinking about how this script will be implemented (package form)

    void Start()
    {
        scoreDisp = GetComponent<ScoreDisplayer>();
        StartCoroutine("GetScore");
    }

    public void GetScores()
    {
        StartCoroutine("GetScore");
    }

    public void PostScores()
    {
        StartCoroutine("PostScore");
    }

    private IEnumerator GetScore()
    {
        List<string> retStrings = new List<string>();
        int i = 0;
        WindowTitel = "Loading";
        DS = "";

        WWWForm form = new WWWForm();
        form.AddField("limit", getLimitScore);
        WWW www = new WWW(GetHighscoreUrl, form);
        yield return www;

        if (www.text == "")
        {
            print("There was an error getting the high score: " + www.error);
            WindowTitel = "There was an error getting the high score";
        }
        else
        {
            WindowTitel = "Done";
            DS = www.text;
            retStrings = DS.Split(';').ToList();
            while (i < retStrings.Count() - 1)
            {
                names.Add(retStrings[i]);
                scores.Add(retStrings[i + 1]);
                i += 2;

            }

            scoreDisp.DisplayScores(names, scores, 0, 0);
        }
    }

    private IEnumerator PostScore(string name, int score)
    {
        string _name = name;
        int _score = score;

        string hash = Md5Sum(_name + _score + secretKey).ToLower();

        WWWForm form = new WWWForm();
        form.AddField("name", _name);
        form.AddField("score", _score);
        form.AddField("hash", hash);

        WWW www = new WWW(PostScoreUrl, form);
        WindowTitel = "Wait";
        yield return www;

        if (www.text == "done")
        {
            StartCoroutine("GetScore");
        }
        else
        {
            print("There was an error posting the high score: " + www.error);
            WindowTitel = "There was an error posting the high score";
        }
    }

    public string Md5Sum(string input)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

}