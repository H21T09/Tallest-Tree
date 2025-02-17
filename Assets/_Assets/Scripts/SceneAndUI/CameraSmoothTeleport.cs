using UnityEngine;

public class CameraSmoothTeleport : MonoBehaviour
{
    public Transform player;  // Tham chiếu tới player
    public float smoothSpeed = 0.125f;  // Tốc độ mượt mà
    public Vector3 offset;  // Độ lệch camera từ player

    private Vector3 targetPosition;

    void Start()
    {
        // Đặt vị trí ban đầu của camera
        targetPosition = player.position + offset;
        targetPosition.x = transform.position.x;  // Khóa trục Y
        transform.position = targetPosition;
    }

    void Update()
    {
        // Kiểm tra nếu player đã teleport đến vị trí mới
        if (player.position != targetPosition)
        {
            // Cập nhật vị trí mục tiêu của camera
            targetPosition = player.position + offset;

            // Giữ nguyên trục Y của camera
            targetPosition.x = transform.position.x;

            // Di chuyển camera mượt mà đến vị trí mới
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
