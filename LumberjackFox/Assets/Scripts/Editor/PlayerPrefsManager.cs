
using UnityEngine;
using UnityEditor;

public class PlayerPrefsManager : EditorWindow
{
    [@MenuItem("4Ever_Alone/Set FirstTime")]
    private static void PrefsShow()
    {
        PlayerPrefs.SetInt("FirstTimePlay", 1);
    }
}
