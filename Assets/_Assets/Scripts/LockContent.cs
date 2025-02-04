using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockContent : MonoBehaviour
{
    private RectTransform rect;
    public float minY = 0f;
    public float maxY = 3100f;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        
        // Lấy giá trị Width hiện tại để giữ nguyên kích thước
        float width = rect.rect.width;

        // Gán giá trị Left và Right
        rect.offsetMin = new Vector2(0, rect.offsetMin.y); // Giữ nguyên Left
        rect.offsetMax = new Vector2(0, rect.offsetMax.y); // Giữ nguyên Right
        

        // Nếu cần, có thể khóa luôn scale và position
        rect.localScale = Vector3.one;
        rect.anchoredPosition = new Vector2(0, rect.anchoredPosition.y);


        // Lấy vị trí hiện tại của content
        Vector2 pos = rect.anchoredPosition;

        // Giữ posY trong khoảng từ minY đến maxY
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Cập nhật lại vị trí
        rect.anchoredPosition = pos;
    }
}
