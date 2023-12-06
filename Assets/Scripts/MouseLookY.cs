using UnityEngine;

public class MouseLookY : MonoBehaviour
{
    public float sensitivityVertical = 2.0f;
    public float minimumVertical = -45.0f;
    public float maximumVertical = 45.0f;
    private float _verticalRotate;

    private void Update()
    {
        _verticalRotate -= Input.GetAxis("Mouse Y") * sensitivityVertical * Time.deltaTime;
        _verticalRotate = Mathf.Clamp(_verticalRotate, minimumVertical, maximumVertical);

        var horizontalRotate = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(_verticalRotate, horizontalRotate, 0);
    }
}