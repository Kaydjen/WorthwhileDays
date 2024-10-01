using UnityEngine;

public class QuadrantsSideTrigger : MonoBehaviour {
    [Tooltip("Стороны в которые может пойти игрок по координатам X")]
    [SerializeField] private int _side_X = 0; // стороны в которые может пойти игрок по координатам X

    [Tooltip("Стороны в которые может пойти игрок по координатам Y")]
    [SerializeField] private int _side_Z = 0; // стороны в которые может пойти игрок по координатам Y
    [SerializeField] private float _quadsHight = 0f;

    private QuadrantsHub _hub; // ссылка на компонент QuadrantsHub, управляющий квадрантами

    private void Start() {
        // Ищем объект с именем "_spawnQuadrant" и получаем компонент QuadrantsHub
        GameObject hubObject = GameObject.Find(ObjectsNames.WORLD_HUB);
        if (hubObject != null) {
            _hub = hubObject.GetComponent<QuadrantsHub>();
            if (_hub == null) 
                Debug.LogWarning($"QuadrantsSideTrigger: QuadrantsHub component not found on {hubObject.name}");            
        } 
        else {
            Debug.LogWarning($"QuadrantsSideTrigger: GameObject {ObjectsNames.WORLD_HUB} not found");
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        // Если игрок входит в триггер, вызываем метод ManageQuadrantsSpawn у QuadrantsHub
        if (other.CompareTag(Tags.PLAYER))
            _hub?.InstantiateNewQuadrant(_side_X, _side_Z, _quadsHight);
    }
}