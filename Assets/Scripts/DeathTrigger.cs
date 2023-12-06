using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public Restart restart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            restart.ResetPositionCheckpoint();
        }
    }
}