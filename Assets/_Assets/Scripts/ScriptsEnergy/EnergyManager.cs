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
    private int regenerationTime = 3600; // 1 giờ để hồi toàn bộ 5 năng lượng
    private int energyRegenInterval; // Thời gian hồi 1 năng lượng

    void Start()
    {
        energyRegenInterval = regenerationTime / maxEnergy; // Tính thời gian hồi 1 năng lượng
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

        if (currentEnergy < maxEnergy && !isRegenerating)
        {
            StartCoroutine(StartEnergyRegeneration());
        }

        UpdateUI();
    }

    private IEnumerator StartEnergyRegeneration()
    {
        isRegenerating = true;
        lastUsedTime = DateTime.Parse(PlayerPrefs.GetString("LastUsedTime", DateTime.Now.ToString()));

        while (currentEnergy < maxEnergy)
        {
            TimeSpan timePassed = DateTime.Now - lastUsedTime;
            int energyToRestore = (int)(timePassed.TotalSeconds / energyRegenInterval);

            if (energyToRestore > 0)
            {
                currentEnergy = Mathf.Min(currentEnergy + energyToRestore, maxEnergy);
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

    private void LoadEnergy()
    {
        currentEnergy = PlayerPrefs.GetInt("Energy", maxEnergy);
        string lastUsed = PlayerPrefs.GetString("LastUsedTime", "");

        if (!string.IsNullOrEmpty(lastUsed))
        {
            lastUsedTime = DateTime.Parse(lastUsed);
            TimeSpan timePassed = DateTime.Now - lastUsedTime;
            int energyToRestore = (int)(timePassed.TotalSeconds / energyRegenInterval);

            if (energyToRestore > 0)
            {
                currentEnergy = Mathf.Min(currentEnergy + energyToRestore, maxEnergy);
                PlayerPrefs.SetInt("Energy", currentEnergy);
                PlayerPrefs.SetString("LastUsedTime", DateTime.Now.ToString());
                PlayerPrefs.Save();
            }

            if (currentEnergy < maxEnergy)
            {
                StartCoroutine(StartEnergyRegeneration());
            }
        }
    }

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

    private void UpdateUI()
    {
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

    private void UpdateTimerUI(int secondsRemaining)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(secondsRemaining);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    [ContextMenu("Reset Energy")]
    public void ResetEnergy()
    {
        PlayerPrefs.DeleteKey("Energy");
        PlayerPrefs.DeleteKey("LastUsedTime");
        PlayerPrefs.Save();
    }
}
