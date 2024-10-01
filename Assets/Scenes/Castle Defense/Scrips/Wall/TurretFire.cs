using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretFire : MonoBehaviour
{
    #region Serialize Variables 
    [SerializeField] private float _range = 10;
    [SerializeField] private float _shootDelay = 2;
    [SerializeField] private float _damage = 100f;
    [SerializeField] private Transform _firePosition;
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private GameObject _ganRotate; 
    #endregion

    #region Public Variables
    #endregion

    #region Private Variables
    private EnemyParameters _enemy;
    private Transform _closestEnemy;
    #endregion

    #region Help Variables
    private Vector3 _turretPosition;
    private float _distanceToEnemy;
    private float _closestDistanceToEnemy;
    private RaycastHit _hit;
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        _enemy = GameObject.Find("Game_manager").GetComponent<EnemyParameters>();
        _turretPosition = gameObject.transform.position;
        StartCoroutine(Fire());
    }
    private IEnumerator Fire()
    {
        while (true)
        {
            if (_enemy.EnemysTransform.Count != 0)
            {
                _closestEnemy = null;
                FindClosestEnemy();

                if (_closestEnemy != null)
                {
                    if (Physics.Raycast(_firePosition.position, (_closestEnemy.position - _firePosition.position).normalized, out _hit, _range))
                    {
                        if (_hit.transform.gameObject.tag == "Enemy")
                        {
                            _hit.transform.gameObject.GetComponent<EnemyHealth>().GetDamage(_damage);
                        }

                        GameObject hitEffectGameOgj = Instantiate(_hitEffect, _hit.point, Quaternion.LookRotation(_hit.normal));
                        Destroy(hitEffectGameOgj, 2f);

                        _ganRotate.SetActive(true);
                        Invoke(nameof(StopRotation), 1.5f);
                    }
                }
            }

            yield return new WaitForSeconds(_shootDelay);
        } 
    }
    #endregion

    #region Private Methods
    public void FindClosestEnemy()
    {
        _closestDistanceToEnemy = Vector3.Distance(_enemy.EnemysTransform[0].position, _turretPosition);

        foreach (Transform enemy in _enemy.EnemysTransform)
        {
            _distanceToEnemy = Vector3.Distance(enemy.position, _turretPosition);

            if (_distanceToEnemy < _closestDistanceToEnemy)
            {
                _closestDistanceToEnemy = _distanceToEnemy;
                _closestEnemy = enemy;
            }
        }
    }
    private void StopRotation()
    {
        _ganRotate.SetActive(false);
    }
    #endregion
}
