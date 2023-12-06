using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField] private Rigidbody player;
    public TimeTracker timeTracker;
    public PB pb;
    public Vector3 respawn = new Vector3(0, 0, 0);
    public Quaternion rotate = new Quaternion(0f, 0f, 0f, 0f);
    public Vector3 respawnCheckpoint = new Vector3(0, 0, 0);
    public Quaternion rotateCheckpoint = new Quaternion(0f, 0f, 0f, 0f);
    public bool restart;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        restart = true;
        player.isKinematic = true;
        player.velocity = Vector3.zero;
        transform.position = respawn;
        transform.rotation = rotate;
        timeTracker.Start();
        pb.Start();
        // player.isKinematic = false;
        restart = false;
    }
    
    public void ResetPositionCheckpoint()
    {
        restart = true;
        player.isKinematic = true;
        player.velocity = Vector3.zero;
        if (respawnCheckpoint == Vector3.zero)
        {
            transform.position = respawn;
            transform.rotation = rotate;
        }
        else
        {
            transform.position = respawnCheckpoint;
            transform.rotation = rotateCheckpoint;
        }
        // player.isKinematic = false;
        restart = false;
    }
}