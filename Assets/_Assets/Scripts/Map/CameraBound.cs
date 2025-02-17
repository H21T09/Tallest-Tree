using UnityEngine;

public class CameraBound : MonoBehaviour
{
    public Transform player;  
    public Vector2 minBounds; 
    public Vector2 maxBounds; 

    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.position;


        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + cam.orthographicSize * cam.aspect, maxBounds.x - cam.orthographicSize * cam.aspect);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + cam.orthographicSize, maxBounds.y - cam.orthographicSize);

        transform.position = newPosition;
    }
}
