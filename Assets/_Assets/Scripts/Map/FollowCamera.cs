using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }
    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            transform.position = new Vector3(0, cameraTransform.position.y + 6 , transform.position.z);
        }
    }
}