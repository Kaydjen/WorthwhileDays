using UnityEngine;

public class EnemyAnimFunctions : MonoBehaviour
{
    private EnemyBrain_Stupid _brain;
    private void Start()
    {
        _brain = GetComponent<EnemyBrain_Stupid>();
    }
    private void ActivateEnemy()
    {
        _brain.enabled = true;
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
