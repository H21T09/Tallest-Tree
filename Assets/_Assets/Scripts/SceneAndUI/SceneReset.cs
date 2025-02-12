using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour
{
    // Gọi hàm này để reset scene
    public void ResetScene()
    {
        
        string currentSceneName = SceneManager.GetActiveScene().name;

        
        SceneManager.LoadScene(currentSceneName);
    }
}
