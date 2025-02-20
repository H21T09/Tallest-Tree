using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class EnergyManager : MonoBehaviour
{
    public int maxEnergy = 5;
    public int currentEnergy;
    public TMP_Text energyText;
    public TMP_Text timerText;
    private DateTime lastUsedTime;
    private bool isRegenerating = false;
    private int regenerationTime = 3600; // 1 giờ (3600 giây) để hồi toàn bộ 5 năng lượng

    
    void Start()
    {
        LoadEnergy();
        StartCoroutine(UpdateTimerCoroutine());
        UpdateUI();
    }

    public void UseEnergy()
    {
        if (currentEnergy > 0)
        {
            currentEnergy--;
            PlayerPrefs.SetInt("Energy", currentEnergy);
            PlayerPrefs.SetString("LastUsedTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }

        if (currentEnergy == 0 && !isRegenerating)
        {
            StartCoroutine(StartEnergyRegeneration());
        }

        UpdateUI();
    }

    private IEnumerator StartEnergyRegeneration()
    {
        isRegenerating = true;

        // Lấy thời gian sử dụng cuối cùng từ PlayerPrefs
        lastUsedTime = DateTime.Parse(PlayerPrefs.GetString("LastUsedTime", DateTime.Now.ToString()));

        while (true)
        {
            TimeSpan timePassed = DateTime.Now - lastUsedTime;
            if (timePassed.TotalSeconds >= regenerationTime)
            {
                currentEnergy = maxEnergy;
                PlayerPrefs.SetInt("Energy", currentEnergy);
                PlayerPrefs.Save();
                break;
            }
            yield return new WaitForSeconds(10);
        }

        isRegenerating = false;
        UpdateUI();
    }


    private void LoadEnergy()
    {
        currentEnergy = PlayerPrefs.GetInt("Energy", maxEnergy);
        string lastUsed = PlayerPrefs.GetString("LastUsedTime", "");

        if (!string.IsNullOrEmpty(lastUsed))
        {
            lastUsedTime = DateTime.Parse(lastUsed);
            TimeSpan timePassed = DateTime.Now - lastUsedTime;

            if (timePassed.TotalSeconds >= regenerationTime)
            {
                currentEnergy = maxEnergy;
                PlayerPrefs.SetInt("Energy", currentEnergy);
                PlayerPrefs.Save();
            }
            else if (currentEnergy == 0)
            {
                StartCoroutine(StartEnergyRegeneration());
            }
        }
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        while (true)
        {
            if (currentEnergy == 0)
            {
                TimeSpan timePassed = DateTime.Now - lastUsedTime;
                int secondsRemaining = regenerationTime - (int)timePassed.TotalSeconds;

                if (secondsRemaining <= 0)
                {
                    currentEnergy = maxEnergy;
                    PlayerPrefs.SetInt("Energy", currentEnergy);
                    PlayerPrefs.Save();
                    UpdateUI();
                    yield break;
                }

                UpdateTimerUI(secondsRemaining);
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateUI()
    {
        energyText.text = currentEnergy + "/" + maxEnergy;
        if (currentEnergy == 0)
        {
            timerText.gameObject.SetActive(true);
            energyText.gameObject.SetActive(false);
            StartCoroutine(UpdateTimerCoroutine());
        }
        else
        {
            timerText.gameObject.SetActive(false);
            energyText.gameObject.SetActive(true);
        }
    }

    private void UpdateTimerUI(int secondsRemaining)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(secondsRemaining);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    [ContextMenu("reset")]
    public void ResetEnergy()
    {
        PlayerPrefs.DeleteKey("Energy");
        PlayerPrefs.DeleteKey("LastUsedTime");
        PlayerPrefs.Save();
    }
}
