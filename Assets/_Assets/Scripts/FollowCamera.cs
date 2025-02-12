using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
    void Update()
    {
        if (cameraTransform != null)
        {
            transform.position = new Vector3(0, cameraTransform.position.y + 6 , transform.position.z);
        }
    }
}