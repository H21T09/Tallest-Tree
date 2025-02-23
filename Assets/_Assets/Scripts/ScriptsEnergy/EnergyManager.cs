using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;

    public int maxEnergy = 5;  // maxEnergy có thể thay đổi nếu cần
    public int currentEnergy;
    public TMP_Text energyText;
    public TMP_Text timerText;
    private DateTime lastUsedTime;
    private bool isRegenerating = false;
    private int regenerationTime = 3600; // 1 giờ để hồi toàn bộ năng lượng (có thể điều chỉnh)
    private int energyRegenInterval; // Thời gian hồi 1 năng lượng

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        energyRegenInterval = regenerationTime / maxEnergy; // Tính thời gian hồi 1 năng lượng
        LoadEnergy();
        StartCoroutine(UpdateTimerCoroutine());
        UpdateUI();
    }


    // Hàm sử dụng năng lượng
    public void UseEnergy()
    {
        if (currentEnergy > 0)
        {
            currentEnergy--;
            PlayerPrefs.SetInt("Energy", currentEnergy);
            PlayerPrefs.SetString("LastUsedTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }

        // Kiểm tra và bắt đầu hồi năng lượng nếu chưa đạt max
        if (currentEnergy < maxEnergy && !isRegenerating)
        {
            StartCoroutine(StartEnergyRegeneration());
        }

        UpdateUI();
    }

    // Cập nhật hàm hồi năng lượng, cho phép cộng thêm năng lượng tự do
    private IEnumerator StartEnergyRegeneration()
    {
        if (currentEnergy >= maxEnergy) yield break;  // Nếu năng lượng >= maxEnergy thì không cần hồi

        isRegenerating = true;
        lastUsedTime = DateTime.Parse(PlayerPrefs.GetString("LastUsedTime", DateTime.Now.ToString()));

        while (currentEnergy < maxEnergy)
        {
            TimeSpan timePassed = DateTime.Now - lastUsedTime;
            int energyToRestore = (int)(timePassed.TotalSeconds / energyRegenInterval);

            if (energyToRestore > 0)
            {
                currentEnergy = Mathf.Min(currentEnergy + energyToRestore, maxEnergy);  // Cập nhật năng lượng
                lastUsedTime = DateTime.Now;
                PlayerPrefs.SetInt("Energy", currentEnergy);
                PlayerPrefs.SetString("LastUsedTime", lastUsedTime.ToString());
                PlayerPrefs.Save();
                UpdateUI();
            }

            if (currentEnergy >= maxEnergy)
                break;

            yield return new WaitForSeconds(10);
        }
        isRegenerating = false;
    }

    // Cập nhật năng lượng khi load từ PlayerPrefs
    private void LoadEnergy()
    {
        if (!PlayerPrefs.HasKey("Energy"))
        {
            PlayerPrefs.SetInt("Energy", maxEnergy); // Đặt giá trị mặc định
            PlayerPrefs.SetString("LastUsedTime", DateTime.Now.ToString()); // Đặt lại thời gian
            PlayerPrefs.Save();
        }

        currentEnergy = PlayerPrefs.GetInt("Energy");
        string lastUsed = PlayerPrefs.GetString("LastUsedTime", "");

        if (!string.IsNullOrEmpty(lastUsed))
        {
            lastUsedTime = DateTime.Parse(lastUsed);
            TimeSpan timePassed = DateTime.Now - lastUsedTime;
            int energyToRestore = (int)(timePassed.TotalSeconds / energyRegenInterval);

            if (energyToRestore > 0)
            {
                //currentEnergy = Mathf.Min(currentEnergy + energyToRestore, maxEnergy);
                //PlayerPrefs.SetInt("Energy", currentEnergy);
                PlayerPrefs.SetString("LastUsedTime", DateTime.Now.ToString());
                PlayerPrefs.Save();
            }

            if (currentEnergy < maxEnergy)
            {
                StartCoroutine(StartEnergyRegeneration());
            }
        }
    }


    // Cập nhật thời gian còn lại cho đến khi hồi năng lượng
    private IEnumerator UpdateTimerCoroutine()
    {
        while (true)
        {
            if (currentEnergy < maxEnergy)
            {
                TimeSpan timePassed = DateTime.Now - lastUsedTime;
                int secondsRemaining = energyRegenInterval - (int)(timePassed.TotalSeconds % energyRegenInterval);
                UpdateTimerUI(secondsRemaining);
            }
            yield return new WaitForSeconds(1);
        }
    }

    // Cập nhật UI
    public void UpdateUI()
    {
        currentEnergy = PlayerPrefs.GetInt("Energy",currentEnergy);
        energyText.text = currentEnergy + "/" + maxEnergy;
        if (currentEnergy > 0)
        {
            timerText.gameObject.SetActive(false);
            energyText.gameObject.SetActive(true);
        }
        else
        {
            timerText.gameObject.SetActive(true);
            energyText.gameObject.SetActive(false);
        }
    }

    // Cập nhật UI thời gian hồi năng lượng
    private void UpdateTimerUI(int secondsRemaining)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(secondsRemaining);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    [ContextMenu("Reset Energy")]
    public void ResetEnergy()
    {
        PlayerPrefs.DeleteKey("LastUsedTime");
        PlayerPrefs.SetString("LastUsedTime", DateTime.Now.ToString());
        PlayerPrefs.SetInt("Energy", maxEnergy);
        PlayerPrefs.Save();
        UpdateUI(); 
    }

    // Hàm để thêm năng lượng từ bên ngoài vào, không bị giới hạn
    public void AddEnergy(int amount)
    {
        currentEnergy += amount;
        PlayerPrefs.SetInt("Energy", currentEnergy);
        PlayerPrefs.Save();
        UpdateUI();
        
    }
}
