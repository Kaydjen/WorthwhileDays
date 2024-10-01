using UnityEngine;

public class QuadrantsFirstPlay : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {

        }
    }
}
