using UnityEngine;
using UnityEngine.UI;

public class ButtonChooseMenu : MonoBehaviour
{
    public Button[] buttons; // Mảng chứa các button
    private Button selectedButton; // Button đang được chọn

    void Start()
    {
        // Mặc định chọn button đầu tiên hoặc lấy lại trạng thái từ PlayerPrefs
        InitializeButtons();
    }

    void InitializeButtons()
    {
        // Lấy index của button đã chọn từ PlayerPrefs (nếu có)
        int selectedIndex = PlayerPrefs.GetInt("SelectedButtonIndex", 0); // Mặc định là button đầu tiên

        // Đặt button theo trạng thái đã lưu
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == selectedIndex)
            {
                SelectButton(buttons[i]); // Chọn button đã lưu
            }
            else
            {
                buttons[i].image.color = new Color(0.3f, 0.3f, 0.3f); // Tắt màu các button khác
            }
        }
    }

    public void SelectButton(Button newButton)
    {
        if (selectedButton != null)
        {
            // Đổi màu button trước đó về bình thường
            selectedButton.image.color = new Color(0.3f, 0.3f, 0.3f); // Màu bạc nhạt
        }

        // Cập nhật button mới được chọn
        selectedButton = newButton;
        selectedButton.image.color = Color.white; // Màu sáng khi chọn

        // Lưu lại index của button đã chọn vào PlayerPrefs
        int selectedIndex = System.Array.IndexOf(buttons, selectedButton);
        PlayerPrefs.SetInt("SelectedButtonIndex", selectedIndex);
        PlayerPrefs.Save(); // Lưu ngay lập tức
    }
}
