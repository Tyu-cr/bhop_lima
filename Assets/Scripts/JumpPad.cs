using UnityEngine;

public class BigJumpTrigger : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float bigJumpForce = 30f;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerRigidbody.AddForce(new Vector3(0, bigJumpForce, 0), ForceMode.Impulse);
        }
    }
}