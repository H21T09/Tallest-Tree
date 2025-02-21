using UnityEngine;
using UnityEngine.UI;

public class ToggleObjects : MonoBehaviour
{
    public GameObject[] objects; // Mảng chứa 3 GameObject cần bật/tắt
    public Button[] buttons;     // Mảng chứa 3 Button điều khiển

    private void Start()
    {
        // Gán sự kiện cho mỗi button
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Lưu index để tránh lỗi delegate
            buttons[i].onClick.AddListener(() => ToggleObject(index));
        }
    }

    void ToggleObject(int activeIndex)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == activeIndex); // Chỉ bật object tại index, còn lại tắt
        }
    }
}
