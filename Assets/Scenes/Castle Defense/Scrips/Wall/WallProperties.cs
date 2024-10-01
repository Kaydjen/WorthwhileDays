using System;
using UnityEngine;

// The wall health and armor, methods to take damage from outside
public class WallProperties : MonoBehaviour
{
    #region Serialize Variables 
    [SerializeField] private UInt32 _maxWallHealth = 500;
    #endregion

    #region Private Variables
    private UInt32 _currentWallHealth;
    #endregion

    #region Help Variables
    #endregion

    #region MonoBehaviour
    private void Start()=> _currentWallHealth = _maxWallHealth; 
    #endregion

    #region Public Methods
    /// <summary>
    /// Take damage from outside
    /// </summary>
    /// <param name="damage"> how much damage will take wall</param>
    public void TakeDamage(UInt32 damage)
    {
        // if the wall has enough health to take the damage and dont die it will
        if(_currentWallHealth - damage >= 0)
        {
            _currentWallHealth -= damage;
        }
        else // in another way it will die
        {
            // DOIT: write the Death behaviour 
            Debug.Log($"The wall {gameObject.name} died! ");
            Destroy(gameObject);
        }
    }
    #endregion

    #region Private Methods
    #endregion

    



}
