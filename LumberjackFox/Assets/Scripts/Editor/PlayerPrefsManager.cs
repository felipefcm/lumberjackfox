
using UnityEngine;
using UnityEditor;

public class PlayerPrefsManager
{
    [MenuItem("4Ever_Alone/Set FirstTime")]
    private static void PrefsShow()
    {
        PlayerPrefs.SetInt("FirstTimePlay", 1);
    }

	[MenuItem("4Ever_Alone/Clear PlayerPrefs")]
	private static void ClearPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}

	[MenuItem("4Ever_Alone/UnlockChapter1")]
    private static void UnlockChapter1()
    {
        PlayerPrefs.SetInt("Chapter1", 1);
    }

	[MenuItem("4Ever_Alone/UnlockChapter2")]
    private static void UnlockChapter2()
    {
        PlayerPrefs.SetInt("Chapter2", 1);
    }

	[MenuItem("4Ever_Alone/UnlockChapter3")]
    private static void UnlockChapter3()
    {
        PlayerPrefs.SetInt("Chapter3", 1);
    }

	[MenuItem("4Ever_Alone/UnlockChapter4")]
    private static void UnlockChapter4()
    {
        PlayerPrefs.SetInt("Chapter4", 1);
    }

	[MenuItem("4Ever_Alone/LockChapters")]
    private static void LockChapters()
    {
        PlayerPrefs.DeleteKey("Chapter1");
		PlayerPrefs.DeleteKey("Chapter2");
		PlayerPrefs.DeleteKey("Chapter3");
		PlayerPrefs.DeleteKey("Chapter4");
    }
}
