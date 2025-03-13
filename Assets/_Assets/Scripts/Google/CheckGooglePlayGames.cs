using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using TMPro;

public class CheckGooglePlayGames : MonoBehaviour
{
    public Button loginButton;

    void Start()
    {
        // Cấu hình Google Play Games
        PlayGamesPlatform.Activate();

        // Gán sự kiện khi bấm button
        if (loginButton != null)
            loginButton.onClick.AddListener(ToggleLogin);
    }

    void ToggleLogin()
    {
            SignIn();
    }

    public void SignIn()
    {
        if (!Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("Đăng nhập thành công! ID: " + Social.localUser.id);
                }
                else
                {
                    Debug.Log("Đăng nhập thất bại.");
                }
            });
        }
    }


    
}
