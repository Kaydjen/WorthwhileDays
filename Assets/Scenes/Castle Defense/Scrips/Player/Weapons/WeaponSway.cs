using UnityEngine;

//TODO: переделать систему полностью, она работает дико не оптимизированно, особенно прыжке 
internal class WeaponSway : MonoBehaviour
{
    #region Serialize Variables 
    [Header("Mouse sway")]
    [SerializeField] private float _displacement_mouse = 0.02f; // Defines the mouse movement sensitivity coefficient for object displacement. The higher the value, the stronger the object's reaction to mouse movement
    [SerializeField] private float _maxDisplacement_mouse = 0.06f;
    [SerializeField] private float _returnSpeed_mouse = 6f;

    [Header("Walk sway")]
    [SerializeField] private float _oscillationSpeed_walk = 0.3f;
    [SerializeField] private float _oscillationAmplitude_walk = 0.05f;
    [Header("Run sway")]
    [SerializeField] private float _oscillationSpeed_run = 1f;
    [SerializeField] private float _oscillationAmplitude_run = 0.1f;
    [SerializeField] private float _smooth = 5f;
    #endregion

    #region Public Variables
    public bool IsWalking = false;
    public bool IsRunning = false;
    #endregion

    #region Private Variables
    private Vector3 _initialPosition;
    private float _mouseInputX;
    private float _mouseInputY;
    #endregion

    #region Help Variables
    private float _mouseMoveX;
    private float _mouseMoveY;
    private float _speed;
    private float _amplitude;
    private float _oscillation;
    private Vector3 _targetPosition;
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        _initialPosition = transform.localPosition;
    }
    private void Update()
    {
        Sway();
        if (IsWalking)
            MoveSway();
    }
    #endregion

    #region Private Methods
    private void Sway()
    {
        _mouseInputX = -Input.GetAxis("Mouse X");
        _mouseInputY = -Input.GetAxis("Mouse Y");

        _mouseMoveX = Mathf.Clamp(_mouseInputX * _displacement_mouse, -_maxDisplacement_mouse, _maxDisplacement_mouse);
        _mouseMoveY = Mathf.Clamp(_mouseInputY * _displacement_mouse, -_maxDisplacement_mouse, _maxDisplacement_mouse);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            new Vector3(_mouseMoveX, _mouseMoveY, 0) + _initialPosition,
            Time.deltaTime * _returnSpeed_mouse);
    }
    private void MoveSway()
    {
        _speed = IsRunning ? _oscillationSpeed_run : _oscillationSpeed_walk;
        _amplitude = IsRunning ? _oscillationAmplitude_run : _oscillationAmplitude_walk;

        _oscillation = Mathf.PingPong(Time.time * _speed, _amplitude * 2) - _amplitude;

        _targetPosition = new Vector3(_initialPosition.x, _initialPosition.y + _oscillation, _initialPosition.z);

        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, Time.deltaTime * _smooth);
    }
    #endregion
}
/*MoveSway()
 
         float speed = IsRunning ? _oscillationSpeed_run : _oscillationSpeed_walk;
        float amplitude = IsRunning ? _oscillationAmplitude_run : _oscillationAmplitude_walk;

        // Используйте Mathf.PingPong для более плавного колебания
        float oscillation = Mathf.PingPong(Time.time * speed, amplitude * 2) - amplitude;

        Vector3 targetPosition = new Vector3(_initialPosition.x, _initialPosition.y + oscillation, _initialPosition.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * _smooth);
 
 
 */
/*
 
 
 using UnityEngine;

internal class WeaponSway : MonoBehaviour
{
    #region Serialize Variables 
    [Header("Mouse sway")]
    [SerializeField] private float _displacement_mouse = 0.02f; // Defines the mouse movement sensitivity coefficient for object displacement. The higher the value, the stronger the object's reaction to mouse movement
    [SerializeField] private float _maxDisplacement_mouse = 0.06f; 
    [SerializeField] private float _returnSpeed_mouse = 6f;

    [Header("Walk sway")]
    [SerializeField] private float _oscillationSpeed_walk = 0.5f;
    [SerializeField] private float _oscillationAmplitude_walk = 0.05f;
    [SerializeField] private float _smooth_walk = 6f;
    [Header("Run sway")]
    [SerializeField] private float _oscillationSpeed_run = 1f;
    [SerializeField] private float _oscillationAmplitude_run = 0.1f;
    [SerializeField] private float _smooth_run = 6f;
    #endregion

    #region Public Variables
    public bool IsWalking;
    public bool IsRunning;
    #endregion

    #region Private Variables
    private Vector3 _initialPosition;
    private Vector3 _finalPosition;
    private float _mouseInputX;
    private float _mouseInputY;
    #endregion

    #region Help Variables
    private float _mouseMoveX;
    private float _mouseMoveY;
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        _initialPosition = transform.localPosition;
    }
    private void Update()
    {
        Sway();
        if(IsWalking)
            MoveSway();
    }
    #endregion

    #region Private Methods
    private void Sway()
    {
        _mouseInputX = -Input.GetAxis("Mouse X");
        _mouseInputY = -Input.GetAxis("Mouse Y");

        _mouseMoveX = Mathf.Clamp(_mouseInputX * _displacement_mouse, -_maxDisplacement_mouse, _maxDisplacement_mouse);
        _mouseMoveY = Mathf.Clamp(_mouseInputY * _displacement_mouse, -_maxDisplacement_mouse, _maxDisplacement_mouse);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition, 
            new Vector3(_mouseMoveX, _mouseMoveY, 0) + _initialPosition, 
            Time.deltaTime * _returnSpeed_mouse);
    }

    private void MoveSway()
    {
        // TODO: there can be problem because of Time.time
        // calculate a new gun position
        float oscillation = Mathf.Sin(
            Time.time * 
            (IsRunning ? _oscillationSpeed_run : _oscillationSpeed_walk)) *
            (IsRunning ? _oscillationAmplitude_run : _oscillationAmplitude_walk);

        // set the new gun position
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, oscillation, 0) + _initialPosition, Time.deltaTime * _smooth_walk);
    }
    #endregion
}

 
 
 
 */
/*
 
     #region Pubilc Methods
    public void MoveSway_Walk()
    {
        // Рассчитываем циклическое движение по оси Y с помощью синуса
        float oscillation = Mathf.Sin(Time.time * _oscillationSpeed_walk) * _oscillationAmplitude_walk;

        // Интерполяция к новой позиции
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, oscillation, 0) + _initialPosition, Time.deltaTime * _smooth_walk);
    }
    public void MoveSway_Run()
    {
        // Рассчитываем циклическое движение по оси Y с помощью синуса
        float oscillation = Mathf.Sin(Time.time * _oscillationSpeed_run) * _oscillationAmplitude_run;

        // Интерполяция к новой позиции
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, oscillation, 0) + _initialPosition, Time.deltaTime * _smooth_run);
    }
    #endregion
 
 
 */
/*
 
 
     private void SmoothGanMovement_walk()
    {
        // Рассчитываем циклическое движение по оси Y с помощью синуса
        float oscillation = Mathf.Sin(Time.time * _oscillationSpeed) * _oscillationAmplitude;

        // Интерполяция к новой позиции
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, oscillation, 0) + _initialPosition, Time.deltaTime * _smoothAmount_b);
    }
 
 
 */