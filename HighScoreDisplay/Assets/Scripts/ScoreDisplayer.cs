﻿//Script by Steven Belowsky and Joshua Bush, March 19th, 2014

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplayer : MonoBehaviour
{
    private List<string> playerNames = new List<string>(), playerScores = new List<string>();
    private GUIText displayText;
    private int topRank, bottomRank, myCurrentRank;
    public int numScoresDisplayed;
    public Font font;
    private Highscore hsScript;

    //============ TO DO ============//
    // -make 3 guitexts instead of 1 (even spacing): 
    //        -rankDisplay, nameDisplay, scoreDisplay (only have to change in the for loopz)
    // -specific ordering (need to utilize orderID parameter in displayScores
    // -adjust on resolution change
    //=============================//

    public int GetRank(string myname, string myscore)
    {
        int rank = -1;

        for (int i = 0; i < playerNames.Count; i++)
            if (playerNames[i] == hsScript.myName)
                if (playerScores[i] == hsScript.myScore.ToString())
                    rank = i + 1;

        return rank;
    }

    void OnEnable()
    {
        hsScript = GetComponent<Highscore>();
        displayText = GameObject.Find("DisplayText").guiText;
        displayText.text = "Loading High Scores...";
    }

    public void DisplayScores(List<string> players, List<string> scores, int myRank, int whichOrder /* for future */)
    {
        displayText.text = "";
        displayText.font = font;

        print("playercount: " + players.Count);
        if (players.Count < 1 || scores.Count < 1)
        {
            print("no players in database. nothing to display");
            return;
        }
        if (numScoresDisplayed > players.Count)
        {
            numScoresDisplayed = players.Count - 1;
            print("not enough players to display that many scores. displaying " + numScoresDisplayed + " instead.");
        }
        if (numScoresDisplayed < 1)
        {
            numScoresDisplayed = 1;
            print("tried to display 0 scores, numScoresDisplayed set to 1 instead");
        }

        playerNames = players;
        playerScores = scores;

        myCurrentRank = GetRank(hsScript.myName, hsScript.myScore);
        topRank = myCurrentRank;
        myRank = myCurrentRank;
        bottomRank = myRank + numScoresDisplayed - 1;

        if (myRank == -1)
        {
            print("rank not found");
            return;
        }

        if (playerNames.Count - myRank < numScoresDisplayed)
        {
            topRank = myRank + 1;
            topRank -= numScoresDisplayed - (playerNames.Count - myRank);
            bottomRank = topRank + numScoresDisplayed - 1;
        }

        for (int i = topRank; i < topRank + numScoresDisplayed; i++)
            displayText.text += i.ToString() + "         " + players[i - 1] + "      " + scores[i - 1] + "\n";

    }

    public void ScrollScores(bool down_or_up)
    {
        if (down_or_up && topRank > 1)
        {
            bottomRank--;
            topRank--;
            displayText.text = "";

            for (int i = topRank; i < topRank + numScoresDisplayed; i++)
                displayText.text += i.ToString() + "         " + playerNames[i - 1] + "      " + playerScores[i - 1] + "\n";
        }
        else if (!down_or_up && bottomRank < playerScores.Count)
        {
            bottomRank++;
            topRank++;
            displayText.text = "";

            for (int i = topRank; i < topRank + numScoresDisplayed; i++)
                displayText.text += i.ToString() + "         " + playerNames[i - 1] + "      " + playerScores[i - 1] + "\n";

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Mouse ScrollWheel") < 0)
            ScrollScores(false);
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Mouse ScrollWheel") > 0)
            ScrollScores(true);
    }
}
