using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Button[] levelButtons;
    public ButtonTeleportManager teleportManager; // Thêm tham chiếu tới ButtonTeleportManager

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UnlockLevels();
    }

    void UnlockLevels()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = (i + 1 <= unlockedLevel);
            bool isCompleted = PlayerPrefs.GetInt("LevelCompleted_" + (i + 1), 0) == 1;
            levelButtons[i].interactable = isUnlocked;

            GameObject Awarded = FindChildByName(levelButtons[i].gameObject, "Awarded");
            GameObject Lock = FindChildByName(levelButtons[i].gameObject, "LOCK");
            GameObject Award = FindChildByName(levelButtons[i].gameObject, "Award");

            if (Awarded != null) Awarded.SetActive(isCompleted);
            if (Lock != null) Lock.SetActive(!isUnlocked);
            if (Award != null) Award.SetActive(isUnlocked && !isCompleted);
        }
    }

    public void CompleteLevel(int levelIndex)
    {
        // Đánh dấu màn đã hoàn thành
        PlayerPrefs.SetInt("LevelCompleted_" + levelIndex, 1);

        // Mở khóa màn tiếp theo nếu có
        int nextLevel = levelIndex + 1;
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (nextLevel > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
        }

        PlayerPrefs.Save();
        

        // Nếu có ButtonTeleportManager, gọi hàm teleport đến button tiếp theo
        if (teleportManager != null)
        {
            teleportManager.TeleportToNextButton();
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
        return null;
    }

    [ContextMenu("reset")]
    void ResetLevel()
    {
        PlayerPrefs.DeleteKey("UnlockedLevel");
        for (int i = 1; i <= levelButtons.Length; i++)
        {
            PlayerPrefs.DeleteKey("LevelCompleted_" + i);
        }
        PlayerPrefs.Save();
    }
}