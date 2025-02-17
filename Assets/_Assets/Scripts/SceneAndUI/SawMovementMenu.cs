using UnityEngine;

public class SawMovementMenu : MonoBehaviour
{
    public Transform saw;  // Đối tượng Saw
    private Vector3 lastPosition;

    void Start()
    {
        if (saw == null)
        {
            Debug.LogError("Saw chưa được gán vào script!");
            return;
        }

        lastPosition = saw.position;
    }

    void LateUpdate()
    {
        // Giữ nguyên vị trí của Saw mà không bị ảnh hưởng bởi Scroll View
        transform.position += saw.position - lastPosition;
        lastPosition = saw.position;
    }
}
