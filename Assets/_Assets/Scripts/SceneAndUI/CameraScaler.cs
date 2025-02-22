using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private float targetAspect = 9f / 16f; // Tỉ lệ 1080x1920

    void Start()
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float scaleHeight = screenAspect / targetAspect;

        Camera cam = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            cam.orthographicSize = cam.orthographicSize / scaleHeight;
        }
    }
}
