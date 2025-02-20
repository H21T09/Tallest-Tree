using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SceneReset : MonoBehaviour
{
    public GameObject OutOfEnergy;
    // Gọi hàm này để reset scene
    public void ResetScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentEnergy = PlayerPrefs.GetInt("Energy", 5);
        if(currentEnergy > 0)
        {
            currentEnergy--;
            PlayerPrefs.SetInt("Energy", currentEnergy);
            PlayerPrefs.Save();
            SceneManager.LoadScene(currentSceneName);
        }
        else OutOfEnergy.SetActive(true);
    }
}
