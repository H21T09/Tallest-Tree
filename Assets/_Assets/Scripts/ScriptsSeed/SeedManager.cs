using UnityEngine;

public class SeedManager : MonoBehaviour
{
    public static SeedManager Instance;
    public int seed;

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
        seed += amount;
        SaveGold();
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
        PlayerPrefs.SetInt("Seed", seed);
        PlayerPrefs.Save();
    }

    void LoadGold()
    {
        seed = PlayerPrefs.GetInt("Seed", 0); // Mặc định là 0 nếu chưa có dữ liệu
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
