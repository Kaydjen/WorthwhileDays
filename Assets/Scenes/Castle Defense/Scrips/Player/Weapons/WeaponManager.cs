using System.Collections;

using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private GameObject[] _weapons;

    private int _currentWeaponIndex;

    private void Start()
    {
        _currentWeaponIndex = 0;


        // ref on the object with all players' weapons
        //Transform weaponsHolder = gameObject.transform.GetChild(0).GetChild(0);

        // check the count of weapons the player has
        int weaponCount = gameObject.transform.childCount;

        // initialize the _weapons array, setting its size according to the number of weapons
        _weapons = new GameObject[weaponCount];

        // put all the weapons in _weapons array
        for (int i = 0; i < weaponCount; i++)
        {
            _weapons[i] = gameObject.transform.GetChild(i).gameObject;
        }

        if (_weapons.Length == 0)
            Debug.LogError("WeaponManager: Start: _weapons are empty");

        _weapons[_currentWeaponIndex].SetActive(true);
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            _weapons[_currentWeaponIndex].SetActive(false);

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) 
            {
                if ((_currentWeaponIndex + 1) < _weapons.GetLength(0))
                {
                    _currentWeaponIndex++;
                } // if_else 1
                else
                {
                    _currentWeaponIndex = 0;
                } // if_else 1
            } // if_else 0
            else 
            {
                if ((_currentWeaponIndex - 1) >= 0)
                {
                    _currentWeaponIndex--;
                } // if_else 1
                else
                {
                    _currentWeaponIndex = _weapons.GetLength(0) - 1;
                } // if_else 1
            } // if_else 0

            _weapons[_currentWeaponIndex].SetActive(true);
        }
    }
}



/*
 
 
         for(int i = 0; i < gameObject.transform.GetChild(0).GetChild(0).childCount; i++)
        {
            _weapons[i] = gameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
        }
 
 
 
 */
/*
 
 
using System.Collections;

using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] private GameObject[] _weapons;

    private int _currentWeaponIndex;

    private void Start()
    {
        _currentWeaponIndex = 0;

        _weapons[_currentWeaponIndex].SetActive(true);
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            _weapons[_currentWeaponIndex].SetActive(false);

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) 
            {
                if ((_currentWeaponIndex + 1) < _weapons.GetLength(0))
                {
                    _currentWeaponIndex++;
                } // if_else 1
                else
                {
                    _currentWeaponIndex = 0;
                } // if_else 1
            } // if_else 0
            else 
            {
                if ((_currentWeaponIndex - 1) >= 0)
                {
                    _currentWeaponIndex--;
                } // if_else 1
                else
                {
                    _currentWeaponIndex = _weapons.GetLength(0) - 1;
                } // if_else 1
            } // if_else 0

            _weapons[_currentWeaponIndex].SetActive(true);
        }
    }
}

 
 
 */

/*
 
 
 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] private WeaponHandler[] _weaponHandlers;

    private int _currentWeaponIndex;

    private void Start()
    {
        _currentWeaponIndex = 0;
        _weaponHandlers[_currentWeaponIndex].gameObject.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            _weaponHandlers[_currentWeaponIndex].gameObject.SetActive(false);

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) 
            {
                if ((_currentWeaponIndex + 1) < _weaponHandlers.GetLength(0))
                {
                    _currentWeaponIndex++;
                } // if_else 1
                else
                {
                    _currentWeaponIndex = 0;
                } // if_else 1
            } // if_else 0
            else 
            {
                if ((_currentWeaponIndex - 1) >= 0)
                {
                    _currentWeaponIndex--;
                } // if_else 1
                else
                {
                    _currentWeaponIndex = _weaponHandlers.GetLength(0) - 1;
                } // if_else 1
            } // if_else 0

            _weaponHandlers[_currentWeaponIndex].gameObject.SetActive(true);
        }
    }
}

 
 
 
 */