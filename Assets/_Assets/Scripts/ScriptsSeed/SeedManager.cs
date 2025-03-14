using UnityEngine;

public class SeedManager : MonoBehaviour
{
    public static SeedManager Instance;
    public int seed;
    public int totalseed;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        LoadGold();
    }

    public void AddSeed(int amount)
    {
        totalseed += amount;
        seed += amount;
        SaveGold();
        GoogleIntegration.Instance.UpdateLeaderboard();
        if (totalseed >= 1000)
        {
            GoogleIntegration.Instance.UnlockAchievement("CgkIxs6L2YscEAIQAw");
        }
    }

    public void SpendSeed(int amount)
    {
        if (seed >= amount)
        {
            seed -= amount;
            SaveGold();
        }
    }

    void SaveGold()
    {
        PlayerPrefs.SetInt("TotalSeed",totalseed);
        PlayerPrefs.SetInt("Seed", seed);
        PlayerPrefs.Save();
        
    }

    void LoadGold()
    {
        seed = PlayerPrefs.GetInt("Seed", 0); // Mặc định là 0 nếu chưa có dữ liệu
        totalseed = PlayerPrefs.GetInt("TotalSeed", 0); // Mặc định là 0 nếu chưa có dữ liệu
        
    }

    public int GetSeed()
    {
        return seed;
    }

    [ContextMenu("Reset")]

    public void Reset()
    {
        {
            PlayerPrefs.DeleteKey("Seed");
            PlayerPrefs.Save();
        }
    }
}
