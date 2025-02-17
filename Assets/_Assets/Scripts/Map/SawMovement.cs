using UnityEngine;

public class SawMovement : MonoBehaviour
{
    public LineRenderer lineRenderer; // Tham chiếu đến LineRenderer
    public float speed = 2f; // Tốc độ di chuyển của máy cưa
    public float t = 0f; // Giá trị nội suy giữa hai điểm
    public bool movingForward = true; // Xác định hướng di chuyển
    public float FirstLimit;
    public float lastLimit;

    private void Awake()
    {
        Vector3 start = lineRenderer.GetPosition(0);
        transform.position = start ;
    }
    void Update()
    {
        SawMove();

    }

    protected virtual void SawMove()
    {
        if (lineRenderer == null) return;

        // Lấy vị trí đầu và cuối của LineRenderer
        Vector3 start = lineRenderer.GetPosition(0);
        Vector3 end = lineRenderer.GetPosition(1);

        // Di chuyển máy cưa dọc theo đường line
        t += (movingForward ? 1 : -1) * speed * Time.deltaTime;

        // Đảo chiều khi chạm giới hạn
        if (t >= lastLimit) { t = lastLimit; movingForward = false; }
        if (t <= FirstLimit) { t = FirstLimit; movingForward = true; }

        // Nội suy vị trí giữa 2 điểm
        Vector3 newPosition = Vector3.Lerp(start, end, t);
        transform.position = newPosition;
    }
    

    
}
