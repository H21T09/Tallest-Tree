using UnityEngine;
using UnityEngine.UI;

public class AudioToggleButton : MonoBehaviour
{
    public Button button;
    public GameObject onState;  // Icon trạng thái bật
    public GameObject offState; // Icon trạng thái tắt
    private bool isMuted;

    void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(ToggleAudio);
        }

        // Lấy trạng thái từ PlayerPrefs
        isMuted = PlayerPrefs.GetInt("AudioMuted", 0) == 1;
        UpdateButtonState();
    }

    void ToggleAudio()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("AudioMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

        // Gửi sự kiện để tất cả AudioManager trong scene cập nhật
        AudioManager.UpdateAudioState();

        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        if (onState != null) onState.SetActive(!isMuted);
        if (offState != null) offState.SetActive(isMuted);
    }
}
