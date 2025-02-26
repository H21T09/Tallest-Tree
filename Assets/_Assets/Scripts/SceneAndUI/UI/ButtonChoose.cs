using UnityEngine;
using UnityEngine.UI;

public class ButtonChoose : MonoBehaviour
{
    public Button[] buttons; // Mảng chứa các button
    private Button selectedButton; // Button đang được chọn

    void Start()
    {
        // Mặc định chọn button đầu tiên
        InitializeButtons();
    }

    void InitializeButtons()
    {
        // Đặt button đầu tiên sáng màu, các button còn lại tối màu
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == 0)
            {
                SelectButton(buttons[i]); // Chọn button đầu tiên
            }
            else
            {
                buttons[i].image.color = new Color(0.3f, 0.2f, 0.2f); // Tắt màu các button khác
            }
        }
    }

    public void SelectButton(Button newButton)
    {
        if (selectedButton != null)
        {
            // Đổi màu button trước đó về bình thường
            selectedButton.image.color = new Color(0.2f, 0.2f, 0.2f); // Màu bạc nhạt

        }

        // Cập nhật button mới được chọn
        selectedButton = newButton;
        selectedButton.image.color = Color.white; // Màu sáng khi chọn
    }
}
