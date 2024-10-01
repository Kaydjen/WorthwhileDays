using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == Tags.PLAYER)
        {
            // TODO: нанести урон
        }
        else
        { 
            Destroy(gameObject);
        }
    }
}
