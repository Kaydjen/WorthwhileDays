using UnityEngine;
using System.Collections.Generic;

public class QuadrantsSideKiller : MonoBehaviour
{
    [Tooltip("Массив имен выходов (SideTrigger) по часовой начиная с 9 часов")]
    [SerializeField] private List<GameObject> _exits = new List<GameObject>(); // Массив для хранения найденных дочерних объектов выходов
    [SerializeField] private LayerMask _layerToHit;

    private float _rayLength = 20f;

    public void Start()
    {
        foreach (var exit in _exits)
        {

            Ray ray = new Ray(transform.position, transform.forward);
    
            // Выполняем Raycast и обрабатываем результат
            if (Physics.Raycast(ray, out RaycastHit hit, _rayLength, _layerToHit))
            {
                Destroy(hit.collider.gameObject);
                Destroy(exit); // Удаляем сам объект exit, если столкнулись
            }
    
        }
    }
}


/*
 
 
 using UnityEngine;
using System.Collections.Generic;

public class QuadrantsSideKiller : MonoBehaviour
{
    [Tooltip("Массив имен выходов (SideTrigger) по часовой начиная с 9 часов")]
    [SerializeField] private List<GameObject> _exits = new List<GameObject>(); // Массив для хранения найденных дочерних объектов выходов
    [SerializeField] private LayerMask _layerToHit;

    private float _rayLength = 20f;

    public void Start()
    {
        foreach (var exit in _exits)
        {
            // Получаем дочерний объект у текущего exit
            Transform childTransform = exit.transform.GetChild(0);

            // Проверяем, есть ли дочерний объект с компонентом Transform
            if (childTransform != null)
            {
                // Создаем луч из позиции и направления дочернего объекта
                Ray ray = new Ray(childTransform.position, childTransform.forward);

                // Выполняем Raycast и обрабатываем результат
                if (Physics.Raycast(ray, out RaycastHit hit, _rayLength, _layerToHit))
                {
                    Destroy(hit.collider.gameObject);
                    Destroy(exit); // Удаляем сам объект exit, если столкнулись
                }
            }
        }
    }
}

 
 
 */