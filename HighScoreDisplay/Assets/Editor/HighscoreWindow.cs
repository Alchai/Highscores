using UnityEngine;
using UnityEditor;
public class HighscoreWindow : EditorWindow
{
    string[] modeOptions = new string[3] { "Standard", "Racing", "Collectibles" };
    string[] raceOptions = new string[4] { "Best Lap", "Best Overall", "Top Speed", "All" };
    bool groupEnabled;
    bool myBool = true;
    int myInt = 10;
    int index = 0;
    int index2 = 0;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/High Score Settings")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        HighscoreWindow window = (HighscoreWindow)EditorWindow.GetWindow(typeof(HighscoreWindow));
        window.title = "HS Settings";
    }

    void OnGUI()
    {
        GUILayout.Label("Mode Settings", EditorStyles.boldLabel);
        index = EditorGUILayout.Popup(index, modeOptions);
        if (index == 0)
        {
            groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("Ascending?", myBool);
            myInt = EditorGUILayout.IntSlider("List Length", myInt, 1, 100);
            EditorGUILayout.EndToggleGroup();
        }
        else if (index == 1)
        {
            index2 = EditorGUILayout.Popup(index2, raceOptions);
            groupEnabled = EditorGUILayout.BeginToggleGroup("Report Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("Self Only?", myBool);
            myInt = EditorGUILayout.IntSlider("List Length", myInt, 1, 100);
            EditorGUILayout.EndToggleGroup();
        }

    }
}