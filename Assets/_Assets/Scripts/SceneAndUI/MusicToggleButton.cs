using UnityEngine;
using UnityEngine.UI;

public class MusicToggleButton : MonoBehaviour
{
    public Button button;
    public GameObject onState;  // Icon trạng thái bật
    public GameObject offState; // Icon trạng thái tắt
    private bool isMuted;

    void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(ToggleMusic);
        }

        // Lấy trạng thái từ PlayerPrefs
        isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        UpdateButtonState();
    }

    void ToggleMusic()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        MusicManager.UpdateMusicState();
        // Gửi sự kiện để tất cả MusicManager trong scene cập nhật
        MusicManagerInGame.UpdateMusicState();

        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        if (onState != null) onState.SetActive(!isMuted);
        if (offState != null) offState.SetActive(isMuted);
    }
}
