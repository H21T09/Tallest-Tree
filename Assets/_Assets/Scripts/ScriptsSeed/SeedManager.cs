using UnityEngine;

public class SeedManager : MonoBehaviour
{
    public int seed =10000;

    void Start()
    {
        LoadGold();
    }

    public void AddGold(int amount)
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


}
