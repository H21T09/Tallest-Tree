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
            Vector3 newPos = Camera.main.transform.position;
            newPos.y= Mathf.Round(newPos.y*100)/100;
            transform.position = new Vector3(0, newPos.y + 6 , transform.position.z);
        }
    }
}