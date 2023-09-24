using UnityEngine;

public class ManeuveringDeviceProjectile : MonoBehaviour
{
    public bool collide = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "wall")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            collide = true;
        }
    }
}
