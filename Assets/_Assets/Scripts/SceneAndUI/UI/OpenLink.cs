using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public string urldiscord = "https://discord.com/invite/NBSaeah"; 
    public string urlprivacy = "https://nielswosylus.com/privacy-policy/"; 
    public string urlTerm = "https://nielswosylus.com/terms-conditions/"; 
    public string urlGreen = "https://tree-nation.com/profile/emberfly-studios"; 
    public string urlTree = "https://greenpeace.ru/act/"; 

    public void OpenDiscord()
    {
        Application.OpenURL(urldiscord);
    }
    public void OpenPrivacy()
    {
        Application.OpenURL(urlprivacy);
    }
    public void OpenTerm()
    {
        Application.OpenURL(urlTerm);
    }

    public void OpenGreen()
    {
        Application.OpenURL(urlGreen);
    }

    public void OpenTree()
    {
        Application.OpenURL(urlTree);
    }
}
