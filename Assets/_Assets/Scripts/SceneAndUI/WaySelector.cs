using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class WaySelector : MonoBehaviour
{
    public List<Button> NumberWay;  // Danh sách các Button Way
    public Button playButton;       // Button Play
    private int selectedWay = 1;   // Lưu Way được chọn (-1 nghĩa là chưa chọn)
    public GameObject Effect;
    public Animator Trandition;
    void Start()
    {
        // Gán sự kiện cho mỗi Button Way
        for (int i = 0; i < NumberWay.Count; i++)
        {
            int index = i; // Lưu index để tránh lỗi delegate
            NumberWay[i].onClick.AddListener(() => SelectWay(index));
        }

        // Gán sự kiện cho Button Play
        playButton.onClick.AddListener(PlayGame);
    }

    void SelectWay(int wayIndex)
    {
        selectedWay = wayIndex + 1;
        Debug.Log("Selected Way: " + selectedWay);
    }

    void PlayGame()
    {
        Effect.SetActive(true);
        Trandition.SetTrigger("End");
        StartCoroutine(WaitForAnimationAndLoadGame());
    }

    IEnumerator WaitForAnimationAndLoadGame()
    {
        
        yield return new WaitForSeconds(1f); 

        LoadGame();
    }

    void LoadGame()
    {
        string sceneName = "WayScene" + selectedWay;
        SceneManager.LoadScene(sceneName);
    }
}
