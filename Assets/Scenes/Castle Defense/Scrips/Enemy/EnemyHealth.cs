using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float _currentHealth = 100f;
    public float SetHealth
    {
        set { _currentHealth = value > 0 ? value : 100; }
    }

    private Animator _enemyAnimator;
    [SerializeField] private string DeathAnimationName;
    private void Start()
    {
        if (!TryGetComponent(out _enemyAnimator))
            Debug.LogError("EnemyHealth: _enemyAnimator component is missing. Invalid");
    }

    public void GetDamage(float count)
    {
        _currentHealth -= count;
        if (_currentHealth <= 0)
            Death();
    }
    public void Death()
    {
      //  GameObject.Find(ObjectsNames.ENEMY_PARAMETERS).GetComponent<EnemyParameters>().IncreaseCountOfKilledEnemys();
        _enemyAnimator.Play(DeathAnimationName);
    }
}


