using UnityEngine;

public class SeedManager : MonoBehaviour
{
    private int seed;

    void Start()
    {
        LoadGold();
    }

    public void AddGold(int amount)
    {
        seed += amount;
        SaveGold();
    }

    public void SpendGold(int amount)
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

    public int GetGold()
    {
        return seed;
    }
}
