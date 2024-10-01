using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Walk_player : MonoBehaviour
{
    #region Serialize Variables 
    [SerializeField] private float _speed = 10f;
    [SerializeField] private WeaponSway _sway;
    #endregion

    #region Public Variables
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = Mathf.Clamp(value, 0f, 200f);
        }
    }
    #endregion

    #region Private Variables
    private CharacterController _controller;
    #endregion

    #region Help Variables
    private float _inputX;
    private float _inputZ;
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        if (!TryGetComponent<CharacterController>(out _controller))
            Debug.LogError($"{gameObject.name}, {this.GetType().Name}, the CharacterController is empty");
    }
    private void Update()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputZ = Input.GetAxis("Vertical");

        _controller.Move((transform.right * _inputX + transform.forward * _inputZ) * _speed * Time.deltaTime);

        // =====  sway =====
        if (_inputX != 0 || _inputZ != 0)
            _sway.IsWalking = true;
        else
            _sway.IsWalking = false;
    }
    #endregion
}


/*
 
1. Get all components
2. Set private speed
3. Set public speed set it min (0) and max(200)
4. Get user input
5. Set movement in c.controller
 
 */