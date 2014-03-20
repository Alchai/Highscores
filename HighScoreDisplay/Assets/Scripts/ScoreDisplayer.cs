//Script by Steven Belowsky and Joshua Bush, March 19th, 2014

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplayer : MonoBehaviour
{
    private List<string> playerNames = new List<string>(), playerScores = new List<string>();
    private GUIText displayText, rankDisplay, nameDisplay, scoreDisplay;
    private int topRank, bottomRank, myCurrentRank;
    public int numScoresDisplayed;
    public Font font;
    private Highscore hsScript;

    //============ TO DO ============//
    // -make 3 guitexts instead of 1 (even spacing): 
    //        -rankDisplay, nameDisplay, scoreDisplay (only have to change in the for loopz)
    // -specific ordering (need to utilize orderID parameter in displayScores
    // -adjust on resolution change
    // -get plane "grid-ish" picture to put behind the scores (a bit transparent or something)
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

        rankDisplay = GameObject.Find("Display_Ranks").guiText;
        nameDisplay = GameObject.Find("Display_Names").guiText;
        scoreDisplay = GameObject.Find("Display_Scores").guiText;

        rankDisplay.text = "Loading High Scores...";
    }

    public void DisplayScores(List<string> players, List<string> scores, int myRank, int whichOrder /* for future */)
    {
        rankDisplay.text = ""; rankDisplay.font = font;
        nameDisplay.text = ""; nameDisplay.font = font;
        scoreDisplay.text = ""; scoreDisplay.font = font;

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

        rankDisplay.text = "RANK" + "\n";
        nameDisplay.text = "NAME" + "\n";
        scoreDisplay.text = "SCORE" + "\n";

        for (int i = topRank; i < topRank + numScoresDisplayed; i++)
        {
            //displayText.text += i.ToString() + "         " + players[i - 1] + "      " + scores[i - 1] + "\n";

            rankDisplay.text += "   " + i.ToString() + "\n";
            nameDisplay.text += players[i - 1] + "\n";
            scoreDisplay.text += scores[i - 1] + "\n";
        }

    }

    public void ScrollScores(bool down_or_up)
    {
        if (down_or_up && topRank > 1)
        {
            bottomRank--;
            topRank--;

            //displayText.text = "";

            rankDisplay.text = "RANK" + "\n";
            nameDisplay.text = "PLAYER NAME" + "\n";
            scoreDisplay.text = "SCORE" + "\n";
            for (int i = topRank; i < topRank + numScoresDisplayed; i++)
            {
                //displayText.text += i.ToString() + "         " + playerNames[i - 1] + "      " + playerScores[i - 1] + "\n";

                rankDisplay.text += "  " + i.ToString() + "\n";
                nameDisplay.text += playerNames[i - 1] + "\n";
                scoreDisplay.text += playerScores[i - 1] + "\n";
            }
        }
        else if (!down_or_up && bottomRank < playerScores.Count)
        {
            bottomRank++;
            topRank++;

            //displayText.text = "";

            rankDisplay.text = "RANK" + "\n";
            nameDisplay.text = "PLAYER NAME" + "\n";
            scoreDisplay.text = "SCORE" + "\n";
            for (int i = topRank; i < topRank + numScoresDisplayed; i++)
            {
                //displayText.text += i.ToString() + "         " + playerNames[i - 1] + "      " + playerScores[i - 1] + "\n";

                rankDisplay.text += "  " + i.ToString() + "\n";
                nameDisplay.text += playerNames[i - 1] + "\n";
                scoreDisplay.text += playerScores[i - 1] + "\n";
            }
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
