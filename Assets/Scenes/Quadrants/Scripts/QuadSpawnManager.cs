using System.Collections.Generic;
using UnityEngine;

public class QuadSpawnManager : MonoBehaviour {

    /// <summary>
    /// Spawns a random quadrant from the provided list of prefabs at a specified position.
    /// </summary>
    /// <param name="prefs">List of prefabs to spawn from.</param>
    /// <param name="quadrantSize_X">Size of each quadrant along the X axis.</param>
    /// <param name="quadrantSize_Z">Size of each quadrant along the Z axis.</param>
    /// <param name="coordinate_X">The X coordinate multiplier for the spawn position.</param>
    /// <param name="coordinate_Z">The Z coordinate multiplier for the spawn position.</param>
    /// <param name="direction_X">The X direction.</param>
    /// <param name="direction_Z">The Z direction.</param>
    /// <returns>Updated list of prefabs after one has been spawned and removed.</returns>
    public List<GameObject> Spawn(List<GameObject> prefs, float quadrantSize_X, float quadrantSize_Z, int coordinate_X, int coordinate_Z, int direction_X, int direction_Z, float spawnHight) {
        if (prefs == null || prefs.Count == 0) { // проверка есть ли в списке элементы во избежание ошибок
            Debug.LogError("QuadrantsSpawner: Spawn error - the prefs list is null or empty.");
            return prefs;
        }

        try { // пытаемся заспавнить квадрант и словить возможные ошибки
            int randomValue = Random.Range(0, prefs.Count); // рандомное значение для спавна рандомного квадранта
            // установка квадранта по определенным координатам с 0 вращением
            GameObject instantiatedPref = Instantiate(prefs[randomValue], new Vector3(quadrantSize_X * coordinate_X, spawnHight, quadrantSize_Z * coordinate_Z), Quaternion.identity);            

            instantiatedPref.GetComponentInChildren<DeterminePlayerCoordinates>().X = coordinate_X;
            instantiatedPref.GetComponentInChildren<DeterminePlayerCoordinates>().Z = coordinate_Z;

            prefs.RemoveAt(randomValue); // дабы уникнуть повторения одних и тех же префабов, удаляем заспавненый префаб из списка
        } 
        catch (System.Exception e) {
            Debug.LogError($"QuadrantsSpawner: Spawn error - failed to spawn prefab. Error: {e.Message}");
            return prefs; // Возвращаем список без изменений, если произошла ошибка
        }
        return prefs;
    }
}