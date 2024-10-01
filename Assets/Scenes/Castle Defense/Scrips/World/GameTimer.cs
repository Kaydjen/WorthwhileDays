using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


internal class GameTimer : MonoBehaviour
{
    #region Serialize Variables 
    [SerializeField] private float _gameTimeSpeed = 1f; // Speed factor of game time 
    #endregion

    #region Public Variables
    public TMP_Text _timerText; // Reference to the UI Text
    #endregion

    #region Private Variables
    private float _elapsedGameTime = 0f; // Total elapsed time in game
    #endregion

    #region Help Variables
    #endregion

    #region MonoBehaviour
    void Update()
    {
        // Increase game time based on the speed factor
        _elapsedGameTime += Time.deltaTime * _gameTimeSpeed;

        int gameMinutes = Mathf.FloorToInt(_elapsedGameTime / 60f);


        // Update the timer text in "MM:SS" format
        _timerText.text = string.Format("{0:00}:{1:00}", gameMinutes, Mathf.FloorToInt(_elapsedGameTime % 60f));
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

