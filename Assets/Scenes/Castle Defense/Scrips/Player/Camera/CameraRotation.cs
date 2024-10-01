using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    #region Serialize Variables 
    [Header("Mouse sensitivity: ")]
    [SerializeField][Range(0, 10)] private float _sensitivity = 1f;
    [Header("Main player obj (body): ")]
    [SerializeField] private Transform _bodyPl;
    #endregion

    #region Public Variables
    #endregion

    #region Private Variables
    private float _rotationX;
    private float _rotationY;
    #endregion

    #region Help Variables
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * _sensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _sensitivity * 100f * Time.deltaTime;

        // Accumulate the rotation values and then apply them in 1*
        _rotationY -= mouseY;

        // limit cam rotation to make it more realistic
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);

        // 1*
        transform.localRotation = Quaternion.Euler(_rotationY, 0f, 0f);
        _bodyPl.Rotate(Vector3.up * mouseX);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion

}

