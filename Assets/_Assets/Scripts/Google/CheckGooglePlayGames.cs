using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using TMPro;

public class CheckGooglePlayGames : MonoBehaviour
{
    public Button loginButton;
    public TMP_Text buttonText; // Text để hiển thị trạng thái

    void Start()
    {
        // Cấu hình Google Play Games
        PlayGamesPlatform.Activate();

        // Kiểm tra trạng thái đăng nhập hiện tại
        UpdateButtonState();

        // Gán sự kiện khi bấm button
        if (loginButton != null)
            loginButton.onClick.AddListener(ToggleLogin);
    }

    void ToggleLogin()
    {
            SignIn();
    }

    void SignIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Đăng nhập thành công!");
            }
            else
            {
                Debug.Log("Đăng nhập thất bại!");
            }
            UpdateButtonState();
        });
    }


    void UpdateButtonState()
    {
        if (buttonText != null)
        {
            buttonText.text = Social.localUser.authenticated ? "LOGGED IN" : "NOT LOGGED IN YET";
        }
    }
}
