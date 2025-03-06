using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs đã được xóa hoàn toàn!");
    }
}