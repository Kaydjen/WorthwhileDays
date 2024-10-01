using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponShoot : MonoBehaviour
{
    #region Serialize Variables 
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private GansData _data;
    [SerializeField] private Transform _firePosition;
    [SerializeField] private LayerMask _layerMask;
    #endregion

    #region Public Variables
    #endregion

    #region Private Variables
    private bool _isReadyToShoot = true;
    private Animator _anim;
    #endregion

    #region Help Variables
    private RaycastHit _hit;
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        if (!TryGetComponent<Animator>(out _anim))
            Debug.LogError($"{gameObject.name}, {this.GetType().Name}, the Animator is empty");
    }
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _isReadyToShoot)
        { 
            _anim.SetTrigger(_data.FireAnimation);
          
            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out _hit, _data.Range, ~_layerMask))
            {
                Debug.Log(_hit.transform.name);

                if(_hit.transform.gameObject.tag == "Enemy")
                {
                    _hit.transform.GetComponent<EnemyHealth>().GetDamage(_data.Damage); 
                }

                GameObject hitEffectGameOgj = Instantiate(_data.HitEffect, _hit.point, Quaternion.LookRotation(_hit.normal));

                Destroy(hitEffectGameOgj, 2f);

                _isReadyToShoot = false;
                Invoke(nameof(ReturnAbility), _data.ShootDelay);
            }
        }
        // TODO: reload and magazine
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void ReturnAbility() => _isReadyToShoot = true;
    #endregion
}
