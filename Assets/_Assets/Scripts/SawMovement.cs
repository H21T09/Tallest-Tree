using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    public LineRenderer lineRenderer; // Đường dẫn
    public GameObject movingObject;  // Đối tượng cần di chuyển
    public float speed = 5f;         // Tốc độ di chuyển

    private List<Vector3> points;    // Các điểm trên LineRenderer
    private int currentIndex = 0;    // Chỉ số điểm hiện tại
    private bool movingForward = true; // Di chuyển tiến hoặc lùi

    void Start()
    {
        // Lấy các điểm từ LineRenderer
        points = new List<Vector3>();
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            points.Add(lineRenderer.GetPosition(i));
        }

        // Đảm bảo đối tượng bắt đầu tại điểm đầu tiên
        if (points.Count > 0)
        {
            movingObject.transform.position = points[0];
        }
    }

    void Update()
    {
        if (points == null || points.Count < 2) return;

        // Lấy vị trí hiện tại và vị trí đích
        Vector3 currentPosition = movingObject.transform.position;
        Vector3 targetPosition = points[currentIndex];

        // Di chuyển đối tượng về phía vị trí đích
        movingObject.transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        // Khi đối tượng đến gần điểm đích
        if (Vector3.Distance(movingObject.transform.position, targetPosition) < 0.1f)
        {
            if (movingForward)
            {
                currentIndex++;
                if (currentIndex >= points.Count)
                {
                    currentIndex = points.Count - 2; // Đi ngược lại
                    movingForward = false;
                }
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = 1; // Đi xuôi lại
                    movingForward = true;
                }
            }
        }
    }
}
