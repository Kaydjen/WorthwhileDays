using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(LoadQuadrants))]
[RequireComponent(typeof(QuadSpawnManager))]
public class QuadrantsHub : MonoBehaviour
{
    private LoadQuadrants _quadrantsLoader;  // скрипт с методом загрузки префабов из директории
    private QuadSpawnManager _quadSpawnManager; // скрипт с методом спавна квадрантов

    [SerializeField] private List<string> _pathsToQuadrants = new List<string>();   // Директория к префабам квадрантов
    [SerializeField] private string _pathToStartQuadrants;
    private List<QuadrantsList> _quadrants = new List<QuadrantsList>(); // префабы квадрантов разных биомов в двумерном списке
    private List<QuadrantsList> _quadrantsTemporary = new List<QuadrantsList>(); // изменяющиеся списки префабов
    private List<GameObject> _startQuadrants = new List<GameObject>();

    [SerializeField] private GameObject _player;
    [SerializeField] private float _quadrantSize_X = 200f; // размер квадрантов по оси X
    [SerializeField] private float _quadrantSize_Z = 200f; // размер квадрантов по оси Z
    private float _quadrantSpawnHight = 0f; // размер квадрантов по оси Z
    private int _currentQuadrantsTypeNumber = 0; // тип квадрантов, который нужно спавнить в данный момент
    private int _currentPlayerPosition_X = 0; // текущая позиция игрока по оси X
    private int _currentPlayerPosition_Z = 0; // текущая позиция игрока по оси Z

    private void Awake()
    {
        InitializeComponents(); // инициализация всех необходимых компонентов
        UnloadingFromDirectory(_quadrants); // выгрузка префабов из каталога в списки _quadrants
        UnloadingFromDirectory(_quadrantsTemporary); // выгрузка префабов из каталога в списки _quadrantsTemporary
        _startQuadrants = _quadrantsLoader.LoadPrefabs(_pathToStartQuadrants).ToList(); // выгрузка префабов из каталога в списки _startQuadrants

        _currentQuadrantsTypeNumber = Random.Range(0, _quadrants.Count); 
        if(_currentQuadrantsTypeNumber >= _startQuadrants.Count) // проверка стартовых квадрантов на наличие квадранта такого же типа как и у обычных квадрантов
            _currentQuadrantsTypeNumber = 0; // если стартовые квадранты не имеют такой же тип как и обычные, спавним деффолтный квадрант
        Instantiate(_startQuadrants[_currentQuadrantsTypeNumber], Vector3.zero, Quaternion.identity); // спавн стартового квадранта
        // размещение игрока
        Vector3 playerStartPos = new Vector3(_quadrantSize_X * _currentPlayerPosition_X, _quadrantSpawnHight + 10f, _quadrantSize_Z * _currentPlayerPosition_Z);
        Instantiate(_player, playerStartPos, Quaternion.identity);
    }
    /// <summary>
    /// Rus: Mетод где происходит попытка получить все необходимые компоненты
    /// Eng: Attempts to initialize all required components for QuadrantsHub.
    /// Logs an error if QuadrantsLoader component is missing or if _pathsToQuadrants is not properly initialized.
    /// </summary>
    private void InitializeComponents()
    {
        if (!TryGetComponent(out _quadrantsLoader)) // попытка инициализации компонента QuadrantsLoader
            Debug.LogError("QuadrantsHub: QuadrantsLoader component is missing. Invalid");
        if (!TryGetComponent(out _quadSpawnManager)) // попытка инициализации компонента QuadrantsSpawner
            Debug.LogError("QuadrantsHub: QuadSpawnManager component is missing. Invalid");
        if (_pathsToQuadrants == null)   // проверка инициализированы ли список  перед использованием. Это поможет избежать NullReferenceException в случае неожиданных ситуаций
            Debug.LogError("QuadrantsHub: _pathsToQuadrants or _quadrantsLoader is not initialized properly.");
    }
    /// <summary>
    /// Rus: Mетод для Выгрузки префабов из каталога в списки
    /// Eng: Loads prefabs from specified directories into the _quadrants list.
    /// </summary>
    /// <remarks>
    /// This method iterates through each directory path in _pathsToQuadrants and loads prefabs using QuadrantsLoader.
    /// Loaded prefabs are added to _quadrants based on their corresponding index.
    /// If no prefabs are loaded for a directory path, a warning is logged.
    /// </remarks>
    private void UnloadingFromDirectory(List<QuadrantsList> quadrants)
    {
        // цикл для выгрузки из директорий в списки префабов
        for (int i = 0; i < _pathsToQuadrants.Count; i++)
        {
            List<GameObject> prefs = _quadrantsLoader.LoadPrefabs(_pathsToQuadrants[i]).ToList(); // выгружаем из директории все префабы в временный список

            if (i >= quadrants.Count) // Проверяем, достаточно ли элементов в quadrants, чтобы добавить новые префабы
                quadrants.Add(new QuadrantsList()); // Добавляем новый элемент QuadrantsList, если он не существует

            if (prefs.Count > 0)
                quadrants[i].Prefs.AddRange(prefs); // из временного списка при условии что временный список prefs не пустой и в нем есть префабы     
            else
                Debug.LogError($"No prefs found for index {i} in for in QuadrantsHub - UnloadingFromDirectory. Make sure _pathsToQuadrants and _quadrantsLoader are initialized properly.");
        }
    }

    /// <summary>
    /// Rus: Управляет процессом спавна квадрантов.
    /// Eng: Manages the spawning process of quadrants
    /// </summary>
    /// <param name="direction_X">Left or right direction</param>
    /// <param name="direction_Z">Up or down direction</param>
    public void InstantiateNewQuadrant(int direction_X, int direction_Z, float quadsHight)
    {
        if (_quadrantsTemporary[_currentQuadrantsTypeNumber].Prefs.Count > 0) { // проверяет пустой ли список префабов квадрантов
            // установка координат по которых заспавниться квадрант
            int x = _currentPlayerPosition_X - direction_X;
            int z = _currentPlayerPosition_Z - direction_Z;

            List<GameObject> temporaryPrefs = _quadSpawnManager.Spawn( 
                _quadrantsTemporary[_currentQuadrantsTypeNumber].Prefs,
                _quadrantSize_X,
                _quadrantSize_Z,
                x,
                z,
                direction_X,
                direction_Z,
                quadsHight
            );

            if (temporaryPrefs.Any())
            { // проверка заспавнился ли префаб
                _quadrantsTemporary[_currentQuadrantsTypeNumber].Prefs.Clear(); // очищаем текущий список
                _quadrantsTemporary[_currentQuadrantsTypeNumber].Prefs.AddRange(temporaryPrefs); // и заполняем его заново
            }
            else
            { 
                RefillTemporaryQuadrantsList(direction_X, direction_Z,  quadsHight);
            }
        }
        else
        { // если список пустой, заново наполняем его префабами
            RefillTemporaryQuadrantsList(direction_X, direction_Z,  quadsHight);
        }
    }
    /// <summary>
    /// Rus: Метод для заполнения временного списка префабов.
    /// Eng: Refills the temporary quadrants list with prefabs from the main quadrants list.
    /// </summary>
    private void RefillTemporaryQuadrantsList(int direction_X, int direction_Z, float quadsHight)
    {
        _quadrantsTemporary[_currentQuadrantsTypeNumber].Prefs.AddRange(_quadrants[_currentQuadrantsTypeNumber].Prefs); // заполняет список квадрантов заново
        _currentQuadrantsTypeNumber = Random.Range(0, _quadrants.Count);
        if(!_quadrantsTemporary[_currentQuadrantsTypeNumber].Prefs.Any()){
            if(_quadrants[_currentQuadrantsTypeNumber].Prefs.Any()){
                _quadrantsTemporary[_currentQuadrantsTypeNumber].Prefs.AddRange(_quadrants[_currentQuadrantsTypeNumber].Prefs);
            }
            else{
                Debug.LogError($"_quadrants[{_currentQuadrantsTypeNumber}].Prefs == 0 ");
                _currentQuadrantsTypeNumber = 0;
            }                        
        }
        InstantiateNewQuadrant(direction_X, direction_Z,  quadsHight);
    }
    /// <summary>
    /// Rus: Метод для изменения текущих координат игрока.
    /// Eng: Updates the current player coordinates
    /// </summary>
    /// <param name="x">New Player x coordinate</param>
    /// <param name="z">New Player z coordinate</param>
    public void ChangeCurrentPlayerCoordinates(int x, int z)
    {
        _currentPlayerPosition_X = x;
        _currentPlayerPosition_Z = z;
    }
}


// Класс для хранения списка префабов квадрантов
[System.Serializable]
public class QuadrantsList
{
    public List<GameObject> Prefs = new List<GameObject>();
}
