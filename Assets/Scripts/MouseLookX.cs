using UnityEngine;

public class MouseLookX : MonoBehaviour
{
    public float sensitivityHorizontal = 2.0f;
    public PauseMenu pauseMenu;

    private void Start()
    {
        if (pauseMenu.pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        var body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    private void Update()
    {
        if (pauseMenu.pause) return;
        
        var horizontalRotate = Input.GetAxis("Mouse X") * sensitivityHorizontal;

        transform.Rotate(0, horizontalRotate * Time.deltaTime, 0);
    }
}