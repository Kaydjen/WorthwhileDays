using UnityEngine;
using UnityEngine.AI;

public class SimpleAIBehavior : MonoBehaviour
{
    #region Serialize Variables 
    #endregion

    #region Public Variables
    #endregion

    #region Private Variables
    private NavMeshAgent _meshAgent;
    private Transform _goal;
    #endregion

    #region Help Variables
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        if (!TryGetComponent<NavMeshAgent>(out _meshAgent))
            Debug.LogError($"{gameObject.name}, {this.GetType().Name}, the NavMeshAgent is empty");

        _goal = GameObject.Find("Goal").GetComponent<Transform>();

        SendBotToTarger(_goal);
    }
    #endregion

    #region Public Methods
    public void SendBotToTarger(Transform target)
    {
        _meshAgent.destination = target.position;
    }

    public void AttackTargetBuilding()
    {
        _meshAgent.isStopped = true;
        Debug.Log("Attack The WAll!!!");
        // TODO: доделать атаку и снятие хп и защиты у башенки
    }
    #endregion

    #region Private Methods
    #endregion


}
