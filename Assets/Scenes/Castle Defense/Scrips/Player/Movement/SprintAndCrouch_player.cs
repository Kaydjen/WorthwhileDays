using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch_player : MonoBehaviour
{
    #region Serialize Variables 
    [SerializeField] private float _sprintSpeed = 20f;
    [SerializeField] private float _crouchSpeed = 5f;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private KeyCode _sprintButton = KeyCode.LeftShift;
    [SerializeField] private KeyCode _crouchButton = KeyCode.LeftControl;
    [SerializeField] private Transform _lookPos;
    [SerializeField] private Vector3 _standPos_vector;
    [SerializeField] private Vector3 _crouchPos_vector;
    [SerializeField] private WeaponSway _sway;

    #endregion

    #region Public Variables
    #endregion

    #region Private Variables
    private Walk_player _movePlayer;
    #endregion

    #region Help Variables
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        if (!TryGetComponent(out _movePlayer))
            Debug.LogWarning("PlayerSprintAndCrouch: _movePlayer is null here");
        if (_lookPos == null)
            Debug.LogWarning("PlayerSprintAndCrouch: _lookPos is null here");
    }
    private void Update()
    {
        // TODO: _sway.IsRunning = false; этот момент надо подправить, когда плеер идет на вприсяде должна быть другая скорость
        if (Input.GetKey(_sprintButton)/* && !_isCrouching*/)
        {
            _movePlayer.Speed = _sprintSpeed;
            _sway.IsRunning = true;
        }
        else if (Input.GetKey(_crouchButton))
        {
            _lookPos.localPosition = _crouchPos_vector;
            _movePlayer.Speed = _crouchSpeed;
            _sway.IsRunning = false;
        }
        else
        {
            _lookPos.localPosition = _standPos_vector;
            _movePlayer.Speed = _moveSpeed;
            _sway.IsRunning = false;
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
