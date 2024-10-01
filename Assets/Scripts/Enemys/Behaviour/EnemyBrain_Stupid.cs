using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain_Stupid : MonoBehaviour
{
    [Header("Animations settings")]
    [SerializeField] private string _parameterName = "Idle_to_Walk";
    [SerializeField] private float _acceleration = 1.6f;

    [Header("Time between shots")]
    [SerializeField] private float _timeBtwShoot = 2f;

    [Header("How often will enemy update position where the player is")]
    [SerializeField] private float _pathUpdateDelay = 0.2f;

    private float _velocity = 0f; // set the idle to walk animation float value (frm 0 to 1, where 0 - idle and 1 - walk)
    private float _brakingDistance; // distance until the enemy stops in front of the player
    private float _pathUpdateTimer;  
    private float _shootingTimer;
    private Transform _target;
    private NavMeshAgent _navMesh_Agent;
    private Animator _enemyAnimator;

    private void Awake()
    {
        _navMesh_Agent = GetComponent<NavMeshAgent>();
        _enemyAnimator = GetComponent<Animator>();

        if (_navMesh_Agent == null || _enemyAnimator == null)
        {
            Debug.LogError("Required components in EnemyBrain_Stupid are missing  -  estroying enemy object");
            Destroy(gameObject);
            return; 
        }
    }
    private void Start()
    {
        _brakingDistance = _navMesh_Agent.stoppingDistance;
        _target = GameObject.FindWithTag(Tags.PLAYER).transform;
    }
    private void Update()
    {
        bool isInRange = Vector3.Distance(transform.position, _target.position) <= _brakingDistance;

        if (isInRange) // look at the player
        {
            LookAtTarget();
            if (_velocity > 0f )
                _velocity -= Time.deltaTime * _acceleration;

            _shootingTimer += Time.deltaTime;
            if (_shootingTimer > _timeBtwShoot) // attack the player
            {
                _shootingTimer = 0f;
                _enemyAnimator.Play("Meenon_Attack");
                this.enabled = false;
            }
        }
        else // go to the player
        {
            UpdatePath();
            if (_velocity < 1f)
                _velocity += Time.deltaTime * _acceleration;
        }

        _enemyAnimator.SetFloat(_parameterName, _velocity);        
    }
    private void LookAtTarget()
    {
        Vector3 lookPos = _target.position - transform.position;
        lookPos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }
    private void UpdatePath()
    {
        if(Time.time >= _pathUpdateTimer)
        {
            _pathUpdateTimer = Time.time + _pathUpdateDelay;
            _navMesh_Agent.SetDestination(_target.position);
        }
    }
}

/*
 
 using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain_Stupid : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent _navMesh_Agent;
    [HideInInspector] public Animator _enemyAnimator;

    [SerializeField] private string _parameterName = "Idle_to_Walk";
    [SerializeField] private float _acceleration = 1.6f;
    [SerializeField] private float _deceleration = 1.5f;
    [SerializeField] private float _timeBtwShoot = 2f;
    private float _velocity = 0f;

    private Transform _target;
    private float _shootingDistance;
    private float _pathUpdateDeadline;

    private float _shootingTimer;

    [Header("Stats")]
    public float _pathUpdateDelay = 0.2f;

    private void Awake()
    {
        if (!TryGetComponent<NavMeshAgent>(out _navMesh_Agent))
            Debug.LogError("Class: EnemyBrain_Stupid | Method: Awake | _navMesh_Agent  is null");
        if (!TryGetComponent<Animator>(out _enemyAnimator))
            Debug.LogError("Class: EnemyBrain_Stupid | Method: Awake | _enemyAnimator is null");
    }
    private void Start()
    {
        _shootingDistance = _navMesh_Agent.stoppingDistance;
        _target = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        bool isInRange = Vector3.Distance(transform.position, _target.position) <= _shootingDistance;

        if (isInRange)
        {
            LookAtTarget();
            if (_velocity > 0f )
                _velocity -= Time.deltaTime * _acceleration;

            _shootingTimer += Time.deltaTime;
            if (_shootingTimer > _timeBtwShoot)
            {
                _shootingTimer = 0f;
                _enemyAnimator.Play("Meenon_Attack");
            }
        }
        else
        {
            UpdatePath();
            if (_velocity < 1f)
                _velocity += Time.deltaTime * _acceleration;
        }

        _enemyAnimator.SetFloat(_parameterName, _velocity);        
    }
    private void LookAtTarget()
    {
        Vector3 lookPos = _target.position - transform.position;
        lookPos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }
    private void UpdatePath()
    {
        if(Time.time >= _pathUpdateDeadline)
        {
            _pathUpdateDeadline = Time.time + _pathUpdateDelay;
            _navMesh_Agent.SetDestination(_target.position);
        }
    }
}
 
 
 */
/* failed way
    1.  _enemyReferences.EnemyAnimator.CrossFadeInFixedTime("Attack", 0.1f);
    2.
        {
            _enemyReferences.EnemyAnimator.SetFloat("Idle_to_Walk", _enemyReferences.NavMesh_Agent.desiredVelocity.sqrMagnitude);
            Debug.Log(_enemyReferences.EnemyAnimator.GetFloat("Idle_to_Walk"));
        }
 */