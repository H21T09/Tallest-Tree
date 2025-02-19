using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons; // Gán các button màn chơi trong Inspector

    void Start()
    {
        UnlockLevels();
    }

    void UnlockLevels()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Mặc định mở khóa màn 1

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = (i + 1 <= unlockedLevel);
            levelButtons[i].interactable = isUnlocked; // Mở khóa button

            GameObject Awarded = FindChildByName(levelButtons[i].gameObject, "Awarded");
            if (Awarded != null)
            {
                bool isCompleted = PlayerPrefs.GetInt("LevelCompleted_" + (i + 1), 0) == 0;
                Awarded.gameObject.SetActive(isCompleted);
            }

            GameObject Award = FindChildByName(levelButtons[i].gameObject, "Award");
            if (Award != null)
            {
                bool isCompleted = PlayerPrefs.GetInt("LevelCompleted_" + (i + 1), 0) == 1;
                Award.gameObject.SetActive(isCompleted);
            }

            GameObject Lock = FindChildByName(levelButtons[i].gameObject, "LOCK");
            if (Lock != null)
            {
                bool isCompleted = PlayerPrefs.GetInt("LevelCompleted_" + (i + 1), 0) == 0;
                Lock.gameObject.SetActive(isCompleted);
            }
        }
    }


    GameObject FindChildByName(GameObject parent, string childName)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.name == childName)
                return child.gameObject;
        }
        return null; // Trả về null nếu không tìm thấy
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }



[ContextMenu("reset")]
    void ResetLevel()
    {
        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.Save();
    }
}

