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

    public GameObject NotEnoughEnergy;

    public AudioClip soundEffect;
    public AudioSource audioSource;

    public AudioClip soundEffect1;
    public AudioSource audioSource1;

    public AudioClip soundEffect2;
    public AudioSource audioSource2;
    private void Awake()
    {
        
        audioSource.playOnAwake = false;
        audioSource.clip = soundEffect;

        audioSource1.playOnAwake = false;
        audioSource1.clip = soundEffect1;

        audioSource2.playOnAwake = false;
        audioSource2.clip = soundEffect2;


    }


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
        audioSource2.Play();
        // Lưu chỉ số button mà người chơi đã chọn
        PlayerPrefs.SetInt("LastSelectedButton", selectedWay);
        PlayerPrefs.Save();

        Debug.Log("Selected Way: " + selectedWay);
        UpdateUI();
    }

    void OpenChoose()
    {
        PanelChoose.SetActive(true);
        audioSource.Play();
    }

    void PlayGame()
    {
        EnergyManager energyManager = FindObjectOfType<EnergyManager>(); // Tìm script EnergyManager trong scene
        audioSource1.Play();
        if (energyManager != null)
        {
            if (energyManager.currentEnergy > 0)
            {
                energyManager.UseEnergy(); // Gọi UseEnergy() để trừ năng lượng
            }
            else
            {
                NotEnoughEnergy.SetActive(true);
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
