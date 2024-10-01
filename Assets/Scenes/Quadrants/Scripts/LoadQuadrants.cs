using UnityEngine;

/// <summary>
/// This class handles the loading of prefabs from specified directories using Unity's Resources system.
/// </summary>
public class LoadQuadrants : MonoBehaviour
{
    /// <summary>
    /// Method for loading prefabs from the current directory to an array or list
    /// </summary>
    /// <param name="path">Path to prefabs you want to load</param>
    /// <returns>A list of loaded GameObjects</returns>
    public GameObject[] LoadPrefabs(string path) {
        try {
            GameObject[] prefs = Resources.LoadAll<GameObject>(path); // выгружает из директории все найденные там префабы и записывает их в массив prefs

            // проверка на корректность загрузки префабов в массив
            if (prefs.Length > 0) Debug.Log("Prefabs loaded successfully! Number of loaded prefabs: " + prefs.Length);
            else Debug.LogError("Prefabs not found at path: " + path);

            return prefs;
        } 
        catch (System.Exception e) {
            Debug.LogError($"LoadQuadrants: LoadPrefabs: Failed to load prefabs from path: {path}. Error: {e.Message}");
            return new GameObject[0]; // Возвращаем пустой массив, если произошла ошибка
        }
    }
}