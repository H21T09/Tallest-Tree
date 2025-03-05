using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;

public class playGamesManager : MonoBehaviour
{ 
    public TMP_Text text;
    void Start()
    {
        
    }

    public void SighIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string ImaUrl = PlayGamesPlatform.Instance.GetUserImageUrl();
            text.text = " success";
        }
        else
        {
            text.text = "fail";
        }
    }
}
