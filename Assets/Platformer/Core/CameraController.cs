using UnityEngine;

//JackHK

public class CameraController : MonoBehaviour
{
    public Transform mainCameraTarget;

    public float smoothSpeed = 0.2f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 targetPosition = mainCameraTarget.position + offset;
        Vector3 latePosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
        transform.position = latePosition; //gradually transforms to targetPosition
    }
}
