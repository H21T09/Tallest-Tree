using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using TMPro;
public class WaySelector : MonoBehaviour
{
    public List<Button> NumberWay;  // Danh sách các Button Way
    public Button playButton;
    public Button Choosing;
    private int selectedWay = 1;   // Lưu Way được chọn (-1 nghĩa là chưa chọn)
    public GameObject Effect;
    public Animator Trandition;
    public GameObject PanelChoose;

    public TMP_Text WayNumber;

    void Start()
    {
        // Gán sự kiện cho mỗi Button Way
        for (int i = 0; i < NumberWay.Count; i++)
        {
            int index = i; // Lưu index để tránh lỗi delegate
            NumberWay[i].onClick.AddListener(() => SelectWay(index));
        }
        Choosing.onClick.AddListener(OpenChoose);
        // Gán sự kiện cho Button Play
        playButton.onClick.AddListener(PlayGame);
    }

    private void Update()
    {
        WayNumber.text = ("STAGE " + selectedWay);
    }
    void SelectWay(int wayIndex)
    {
        selectedWay = wayIndex + 1;
        Debug.Log("Selected Way: " + selectedWay);
    }

    void OpenChoose()
    {
        PanelChoose.SetActive(true);
    }

    void PlayGame()
    {
        Effect.SetActive(true);
        Trandition.SetTrigger("End");
        StartCoroutine(WaitForAnimationAndLoadGame());
    }

    IEnumerator WaitForAnimationAndLoadGame()
    {
        
        yield return new WaitForSeconds(0.5f); 

        LoadGame();
    }

    void LoadGame()
    {
        string sceneName = "WayScene" + selectedWay;
        SceneManager.LoadScene(sceneName);
    }
}
