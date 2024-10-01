
using UnityEngine;

public class Fire_Orb : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        Destroy(gameObject);
    }
}
