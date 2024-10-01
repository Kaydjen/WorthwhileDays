using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections;

[Singleton]
public class EnemyParameters : MonoBehaviour
{
    #region Serialize Variables 
    [SerializeField] private List<DifficultyThresholdDays> _wavesParameters = new List<DifficultyThresholdDays>();
    [SerializeField] private List<Transform> _spawnersPos = new List<Transform>();
    [SerializeField] private int _timeToStartFirstWave = 5;
    #endregion

    #region Public Variables
    public List<Transform> EnemysTransform = new List<Transform>();
    #endregion

    #region Private Variables
    private int _stageWave;
    private int _currentDay;
    #endregion

    #region Help Variables
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        StartCoroutine(Spawn());
    }
    #endregion

    #region Private Methods
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(_timeToStartFirstWave);

        while (true)
        {
            int index;
            for (int i = 0; i < _wavesParameters[_stageWave].Enemys.Count; i++)
            {
                if (_wavesParameters[_stageWave].Enemys[i].CountToSpawn != 0)
                {
                    for (int j = 0; j < _wavesParameters[_stageWave].Enemys[i].CountToSpawn; j++)
                    {
                        index = j % _spawnersPos.Count;

                        // TODO: проблема с частотой спавна, боты могут спавниться друг в друге не успевая отойти и тем самым нарушая строй
                        if (index == 0)
                            yield return new WaitForSeconds(1.5f);

                        GameObject newEnemy = Instantiate(_wavesParameters[_stageWave].Enemys[i].Pref, _spawnersPos[index].position, Quaternion.identity);
                       
                        if(newEnemy) EnemysTransform.Add(newEnemy.transform);
                    }
                }
                yield return new WaitForSeconds(_wavesParameters[_stageWave].Enemys[i].TimeToNextWave);
            }

            if (_currentDay == _wavesParameters[_stageWave].StageWave)
                _stageWave++;

            _currentDay++;

            yield return new WaitForSeconds(_wavesParameters[_stageWave].TimeToNextDay);
        }
    }
    #endregion
}




/*
 
 
 
 
 
 using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections;

[Singleton]
public class EnemyParameters : MonoBehaviour
{
    [SerializeField] private List<DifficultyThresholdDays> _wavesParameters = new List<DifficultyThresholdDays>();
    private int _currentStageIndex = 0;
    private int _currentDay = 0;

    private int _numberOfDaysBeforeNextStage = 0;
    private int _intervalBetweenWaves = 0;

    public int CountOfWaves
    {
        get
        {
            return Random.Range(_wavesParameters[_currentStageIndex].MinimumWaves, _wavesParameters[_currentStageIndex].MaximumWaves); ;
        }
    }

    public event System.Action UpdateSpawnersParametersEvent;

    public TMP_Text _timerText; // Reference to the UI Text
    private float _gameTimeSpeed = 1f; // Speed factor of game time 
    private float _elapsedGameTime = 0f; // Total elapsed time in game

    void Update()
    {
        // Increase game time based on the speed factor
        _elapsedGameTime += Time.deltaTime * _gameTimeSpeed;

        int gameMinutes = Mathf.FloorToInt(_elapsedGameTime / 60f);
       

        // Update the timer text in "MM:SS" format
        _timerText.text = string.Format("{0:00}:{1:00}", gameMinutes, Mathf.FloorToInt(_elapsedGameTime % 60f));
    }
    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        while (true)
        {
            if (_currentDay == _wavesParameters[_currentStageIndex].StageDay)
            {

                UpdateTimings();

                UpdateSpawnersParametersEvent.Invoke();

                yield return new WaitForNextFrameUnit();

                // TЕODO: подумать тут на счет логики, не уверен что день норм будет отбновлятся. Плюсом надо еще запилить показ дня
                if (_currentStageIndex + 1 < _wavesParameters.Count)
                    _currentStageIndex++;
            }

            yield return new WaitForSeconds(_intervalBetweenWaves);
        }
    }


    private void UpdateTimings()
    {
        _numberOfDaysBeforeNextStage = _wavesParameters[1 + _currentStageIndex].StageDay - _wavesParameters[_currentStageIndex].StageDay;
        _intervalBetweenWaves = (_wavesParameters[1 + _currentStageIndex].IntervalBetweenWaves - _wavesParameters[_currentStageIndex].IntervalBetweenWaves) / _numberOfDaysBeforeNextStage;
    }



 
 
 
 
 */
/*
 
 
 using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

[Singleton]
public class EnemyParameters : MonoBehaviour
{
    [SerializeField] private List<DifficultyThresholdDays> _wavesParameters = new List<DifficultyThresholdDays>();
    private int _currentStageIndex = 0;
    private int _currentDay = 0;

    private int _numberOfDaysBeforeNextStage = 0;
    private float _intervalBetweenWaves = 0f;
    private float _timeToNextWave = 0f;


    public event System.Action UpdateSpawnersParametersEvent;

    public TMP_Text _timerText; // Reference to the UI Text
    private float _gameTimeSpeed = 1f; // Speed factor of game time 
    private float _elapsedGameTime = 0f; // Total elapsed time in game

    void Update()
    {
        // Increase game time based on the speed factor
        _elapsedGameTime += Time.deltaTime * _gameTimeSpeed;
        _timeToNextWave += Time.deltaTime * _gameTimeSpeed;

        int gameMinutes = Mathf.FloorToInt(_elapsedGameTime / 60f);
       

        // Update the timer text in "MM:SS" format
        _timerText.text = string.Format("{0:00}:{1:00}", gameMinutes, Mathf.FloorToInt(_elapsedGameTime % 60f));
  
    //TУODO: there can be bug that nesseccery time can be skipped duo to update lags 
        if(_timeToNextWave >= _intervalBetweenWaves)
        {
            _timeToNextWave = 0f;


        }
        else if (_currentDay == _wavesParameters[_currentStageIndex].StageDay && gameMinutes == _wavesParameters[_currentStageIndex].IntervalBetweenWaves)
        {
            Invoke(nameof(NextStageDay), 5);

            UpdateTimings();

            UpdateSpawnersParametersEvent.Invoke();
        }
    }
    private void Start()
    {
        UpdateTimings();
    }
    private void UpdateTimings()
    {
        _numberOfDaysBeforeNextStage = _wavesParameters[1 + _currentStageIndex].StageDay - _wavesParameters[_currentStageIndex].StageDay;
        _intervalBetweenWaves = (_wavesParameters[1 + _currentStageIndex].IntervalBetweenWaves - _wavesParameters[_currentStageIndex].IntervalBetweenWaves) / _numberOfDaysBeforeNextStage;
    }
    private void NextStageDay()
    {
        if(_currentStageIndex + 1 < _wavesParameters.Count)
            _currentStageIndex++;
    }
}
 
 
 */

//   byte countOfEnemies = Convert.ToByte(UnityEngine.Random.Range(_daysParam[_currentDay].EnemysStats[].MinSpawnAmount, _daysParam[_currentDay].MaximumWaves));