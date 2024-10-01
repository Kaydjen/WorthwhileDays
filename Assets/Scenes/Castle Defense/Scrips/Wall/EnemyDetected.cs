using UnityEngine;

// when the wall detected the enemy near it
public class EnemyDetected  : MonoBehaviour
{
    #region Serialize Variables 
    [Tooltip("The radius within which the wall detects enemies and forces them to attack it")]
    [SerializeField] private float _checkRadius;
    [Tooltip("The enemy layer mask ensures the check sphere only detects enemies")]
    [SerializeField] private LayerMask _layerMask;
    #endregion

    #region Private Variables
    private bool _doesSphereCheck = true; // check if the sphere can detect enemies
    #endregion

    #region MonoBehaviour
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy detected");

        // TODO: тут проблема в том, что когда ботов очень много (100) при атаке стены те боты которые пришли позже других и еще не начали атаку толкают тех которые уже атакуют и они выталкиваются за противоположной стороной стены
        // make enemy to attack the wall
        other.gameObject.GetComponent<SimpleAIBehavior>().AttackTargetBuilding();

        // Make other enemies move and attack the wall. Ensure that this action occurs no more than once every 5 seconds
        if (_doesSphereCheck)
            ForceEnemiesInRangeToAttackWall();
    }
    #endregion

    #region Private Methods
    private void ForceEnemiesInRangeToAttackWall()
    {
        _doesSphereCheck = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _checkRadius, _layerMask);

        foreach (var enemy in hitColliders)
        {
            // Direct each enemy to the wall by updating their destination
            enemy.gameObject.GetComponent<SimpleAIBehavior>().SendBotToTarger(transform);
        }

        Invoke(nameof(ResetAttack), 3.5f);
    }

    private void ResetAttack()
    {
        _doesSphereCheck = true;
    }
    #endregion
}
















































