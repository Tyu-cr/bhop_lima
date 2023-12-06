using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public GameObject cube;
    public Restart restart;
    public bool start;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (start)
            {
                restart.respawn = cube.transform.position;
                restart.rotate = cube.transform.rotation;
            }
            else
            {
                restart.respawnCheckpoint = cube.transform.position;
                restart.rotateCheckpoint = cube.transform.rotation;
            }
        }
    }
}