using System;
using UnityEngine;

public class DeterminePlayerCoordinates : MonoBehaviour
{
    [NonSerialized] public int X = 0; // координаты квадранта, получаем когда спавним его - X
    [NonSerialized] public int Z = 0; // координаты квадранта, получаем когда спавним его - Y
    private QuadrantsHub _hub; // класс где лежат все основные команды по упралению квадрантами

    private void Start()
    { 
        // Ищем объект с именем "_spawnQuadrant" и получаем компонент QuadrantsHub
        GameObject hubObject = GameObject.Find(ObjectsNames.WORLD_HUB);
        if (hubObject != null) {
            _hub = hubObject.GetComponent<QuadrantsHub>();
            if (_hub == null) 
                Debug.LogWarning($"QuadrantsSideTrigger: QuadrantsHub component not found on {hubObject.name}");            
        } 
        else {
            Debug.LogWarning($"QuadrantsSideTrigger: GameObject '{ObjectsNames.WORLD_HUB}' not found");
        }
    }
    private void OnTriggerEnter(Collider other) // изменяем текущие координаты игрока если он заходит в зону триггер
    {
        if (other.CompareTag(Tags.PLAYER))
            _hub?.ChangeCurrentPlayerCoordinates(X, Z);
    }
}
