using TMPro;
using UnityEngine;

public class UIVelocity : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI velocity;
    [SerializeField] private Rigidbody rigidBody;

    private void Update()
    {
        var rigidBodyVel = rigidBody.velocity;
        var vel = new Vector3(rigidBodyVel.x, 0, rigidBodyVel.z).magnitude;
        velocity.text = "Velocity: " + Mathf.RoundToInt(vel * 48f);
    }
}