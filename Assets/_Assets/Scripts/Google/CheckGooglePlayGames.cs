using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using TMPro;

public class CheckGooglePlayGames : MonoBehaviour
{
    public TMP_Text statusText; // Kéo một Text UI vào đây để hiển thị kết quả

    void Start()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Đã kết nối với Google Play Games!");
                statusText.text = "";
            }
            else
            {
                Debug.Log("Chưa kết nối với Google Play Games!");
                statusText.text = "";
            }
        });
    }
}
