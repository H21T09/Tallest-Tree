using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform; 

    void FixedUpdate()
    {
        if (cameraTransform != null)
        {
            transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y + (5.894445f), transform.position.z);
        }
    }
}