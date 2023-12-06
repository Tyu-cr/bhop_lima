using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    public bool invertY;
    private float _rotationY;
    
    public PauseMenu pauseMenu;
    public EndTrigger endTrigger;

    private void Start()
    {
        pauseMenu = pauseMenu.GetComponent<PauseMenu>();
        endTrigger = endTrigger.GetComponent<EndTrigger>();
        
        if ((pauseMenu.pause) || (endTrigger.finish))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    private void Update()
    {
        if ((pauseMenu.pause) || (endTrigger.finish))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        var ySens = sensitivityY;
        if (invertY)
        {
            ySens *= -1f;
        }

        if ((pauseMenu.pause) || (endTrigger.finish)) return;

        if (axes == RotationAxes.MouseXAndY)
        {
            var rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            _rotationY += Input.GetAxis("Mouse Y") * ySens;
            _rotationY = Mathf.Clamp(_rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-_rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            _rotationY += Input.GetAxis("Mouse Y") * ySens;
            _rotationY = Mathf.Clamp(_rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-_rotationY, transform.localEulerAngles.y, 0);
        }
    }
}