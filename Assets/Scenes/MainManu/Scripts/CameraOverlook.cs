using UnityEngine;
using UnityEngine.UIElements;

public class CameraOverlook : MonoBehaviour
{
    [SerializeField] private float _displacement = 3f;
    [SerializeField] private float _maxDisplacement = 3f;
    [SerializeField] private float _returnSpeed = 2f;

    private Vector3 _initialPosition;
    private float _mouseInputX;
    private float _mouseInputY;
    private float _mouseMoveX;
    private float _mouseMoveY;
    private void Start() => _initialPosition = transform.position;

    private void Update()
    {
        _mouseInputX = -Input.GetAxis("Mouse X");
        _mouseInputY = -Input.GetAxis("Mouse Y");

        _mouseMoveX = Mathf.Clamp(_mouseInputX * _displacement, -_maxDisplacement, _maxDisplacement);
        _mouseMoveY = Mathf.Clamp(_mouseInputY * _displacement, -_maxDisplacement, _maxDisplacement);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            new Vector3(_mouseMoveX, _mouseMoveY, transform.localPosition.z) + _initialPosition,
            Time.deltaTime * _returnSpeed);
    }
}



































#region Serialize Variables 
#endregion

#region Public Variables
#endregion

#region Private Variables
#endregion

#region Help Variables
#endregion

#region MonoBehaviour
#endregion

#region Public Methods
#endregion

#region Private Methods
#endregion