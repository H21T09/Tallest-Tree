using UnityEngine;
using UnityEngine.UI;

public class ButtonLogger : MonoBehaviour
{
    public Button myButton; // Reference to the UI Button

    void Start()
    {
        if (myButton != null)
        {
            myButton.onClick.AddListener(LogMessage);
        }
    }

     public void LogMessage()
    {
        Debug.Log("Button Clicked!");
    }
}