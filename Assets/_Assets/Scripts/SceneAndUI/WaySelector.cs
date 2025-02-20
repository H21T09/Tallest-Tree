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
    private int selectedWay;   // Lưu chỉ số button cuối cùng
    public GameObject Effect;
    public Animator Trandition;
    public GameObject PanelChoose;
    public TMP_Text WayNumber;
   

    void Start()
    {
        // Load button cuối cùng mà người chơi đã chọn (mặc định là 1 nếu chưa có)
        selectedWay = PlayerPrefs.GetInt("LastSelectedButton", 1);
        

        // Gán sự kiện cho mỗi Button Way
        for (int i = 0; i < NumberWay.Count; i++)
        {
            int index = i;
            NumberWay[i].onClick.AddListener(() => SelectWay(index));
        }

        Choosing.onClick.AddListener(OpenChoose);
        playButton.onClick.AddListener(PlayGame);

        UpdateUI();
    }

    private void Update()
    {
        WayNumber.text = "STAGE " + selectedWay;
    }

    void SelectWay(int wayIndex)
    {
        selectedWay = wayIndex + 1;

        // Lưu chỉ số button mà người chơi đã chọn
        PlayerPrefs.SetInt("LastSelectedButton", selectedWay);
        PlayerPrefs.Save();

        Debug.Log("Selected Way: " + selectedWay);
        UpdateUI();
    }

    void OpenChoose()
    {
        PanelChoose.SetActive(true);
    }

    void PlayGame()
    {
        EnergyManager energyManager = FindObjectOfType<EnergyManager>(); // Tìm script EnergyManager trong scene

        if (energyManager != null)
        {
            if (energyManager.currentEnergy > 0)
            {
                energyManager.UseEnergy(); // Gọi UseEnergy() để trừ năng lượng
            }
            else
            {
                Debug.Log("Không đủ năng lượng để chơi!");
                return;
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy EnergyManager trong scene!");
            return;
        }

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

    void UpdateUI()
    {
        // Cập nhật UI để phản ánh button đã chọn
        for (int i = 0; i < NumberWay.Count; i++)
        {
            bool isSelected = (i + 1 == selectedWay);
            Color targetColor = isSelected ? Color.green : Color.white; // Ví dụ đổi màu để hiển thị button được chọn
            NumberWay[i].GetComponent<Image>().color = targetColor;
        }
    }
}
