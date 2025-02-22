using UnityEngine;
using UnityEngine.UI;

public class ButtonChooseMenu : MonoBehaviour
{
    public Button[] buttons; // Mảng chứa các button
    private Button selectedButton; // Button đang được chọn

    void Start()
    {
        InitializeButtons();
    }

    void InitializeButtons()
    {
        // Nếu game vừa được mở lại từ đầu, reset về button đầu tiên
        if (PlayerPrefs.GetInt("GameRestarted", 1) == 1)
        {
            PlayerPrefs.SetInt("SelectedButtonIndex", 0);
            PlayerPrefs.SetInt("GameRestarted", 0);
            PlayerPrefs.Save();
        }

        int selectedIndex = PlayerPrefs.GetInt("SelectedButtonIndex", 0);

        // Nếu index không hợp lệ, reset về button đầu tiên
        if (selectedIndex < 0 || selectedIndex >= buttons.Length)
        {
            selectedIndex = 0;
        }

        // Đặt button theo trạng thái đã lưu hoặc mặc định là button đầu tiên
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == selectedIndex)
            {
                SelectButton(buttons[i]);
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
            selectedButton.image.color = new Color(0.3f, 0.3f, 0.3f);
        }

        // Cập nhật button mới được chọn
        selectedButton = newButton;
        selectedButton.image.color = Color.white; // Màu sáng khi chọn

        // Lưu lại index của button đã chọn vào PlayerPrefs
        int selectedIndex = System.Array.IndexOf(buttons, selectedButton);
        PlayerPrefs.SetInt("SelectedButtonIndex", selectedIndex);
        PlayerPrefs.Save(); // Lưu ngay lập tức
    }

    void OnApplicationQuit()
    {
        // Đánh dấu game đã bị đóng hoàn toàn để reset button lần sau
        PlayerPrefs.SetInt("GameRestarted", 1);
        PlayerPrefs.Save();
    }
}
