using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    [Header("Shoot options")]
    [SerializeField] private Transform _gunPoint; // From where enemy will fire
    [SerializeField] private GameObject _archedProjectileBullet;  // The projectile prefab to be launched
    private Transform _target;  // The target that enemy must hit
    private void Start()
    {
        _target = GameObject.FindWithTag(Tags.PLAYER).transform;        
    }
    private void ArchedProjectileAttack()
    {
        BallisticVel(_target);

        GameObject targetGameObj = Instantiate(_archedProjectileBullet, _gunPoint.position, Quaternion.identity); // spawn bullet
        targetGameObj.GetComponent<Rigidbody>().velocity = BallisticVel(_target);  // add speed (velocity) to the bullet
    }

    Vector3 BallisticVel(Transform target)
    {
       Vector3 dir = target.position - transform.position; // get target direction
       float h = dir.y;  // get height difference
       dir.y = 0;  // retain only the horizontal direction
       float dist = dir.magnitude;  // get horizontal distance
       dir.y = dist;  // set elevation to 45 degrees
       dist += h;  // correct for different heights
       float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);
       return vel * dir.normalized;  // returns Vector3 velocity
    }
}
