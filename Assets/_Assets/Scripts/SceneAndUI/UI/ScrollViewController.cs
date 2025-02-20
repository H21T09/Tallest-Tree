using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform player; // Tham chiếu đến player
    public RectTransform content; // Tham chiếu đến content của ScrollView
    public float smoothTime = 0.5f; // Thời gian di chuyển mượt

    private float lastPlayerPosY; // Lưu giá trị posY cuối cùng của player
    private Vector2 targetPosition; // Vị trí đích của content
    private bool isUserScrolling = false; // Kiểm tra người dùng có cuộn không

    void Start()
    {
        // Tự tìm player trong scene nếu chưa được gán
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player"); // Tìm theo tag "Player"
            if (foundPlayer != null)
            {
                player = foundPlayer.GetComponent<RectTransform>();
            }
            else
            {
                Debug.LogWarning("Không tìm thấy Player trong scene! Kiểm tra lại tag hoặc gán thủ công.");
            }
        }

        if (player != null)
        {
            lastPlayerPosY = player.localPosition.y;

            // Căn chỉnh content ngay khi khởi động
            float targetContentY = Mathf.Clamp(Mathf.Abs(lastPlayerPosY) - 1000, 0, 2511);
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, targetContentY);
        }

        if (content != null)
        {
            targetPosition = content.anchoredPosition;
        }
    }

    void Update()
    {
        if (player == null || content == null) return;

        float currentPlayerPosY = player.localPosition.y;

        // Kiểm tra nếu player di chuyển, thì cho phép follow lại
        if (Mathf.Abs(currentPlayerPosY - lastPlayerPosY) > 0.01f)
        {
            isUserScrolling = false;
        }

        // Nếu người chơi đang cuộn thủ công, không follow player
        if (isUserScrolling) return;

        // Chỉ thực hiện nếu posY của player thay đổi
        if (!Mathf.Approximately(currentPlayerPosY, lastPlayerPosY))
        {
            // Tính toán vị trí mới của content
            float targetContentY = Mathf.Clamp(Mathf.Abs(currentPlayerPosY) - 1000, 0, 2511);
            targetPosition = new Vector2(content.anchoredPosition.x, targetContentY);

            // Lưu lại posY cuối cùng của player
            lastPlayerPosY = currentPlayerPosY;
        }

        // Di chuyển content từ từ về vị trí mới bằng Lerp
        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, Time.deltaTime / smoothTime);
    }

    // Khi người chơi chạm vào ScrollView, dừng follow player
    public void OnPointerDown(PointerEventData eventData)
    {
        isUserScrolling = true;
    }

    // Khi người chơi thả tay, vẫn giữ isUserScrolling = true, chỉ tắt khi player di chuyển
    public void OnPointerUp(PointerEventData eventData)
    {
        // Không làm gì, chỉ Update() mới có thể tắt isUserScrolling khi player di chuyển
    }
}
