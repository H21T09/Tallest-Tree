using UnityEngine;

public class GameCompletionManager : MonoBehaviour
{
    public string sceneName; // Đặt tên scene này trong Inspector
    public int SeedReward = 50; // Phần thưởng lần đầu
    public int EnergyReward = 1;
    public GameObject FirstWin;
    public GameObject PerfectWin;
    private int totalSeeds;

    private void Start()
    {
        totalSeeds = GameObject.FindGameObjectsWithTag("Seed").Length;
    }
    public void CompleteLevel()
    {
        string key = "Completed_" + sceneName;

        if (!PlayerPrefs.HasKey(key)) // Nếu chưa từng hoàn thành
        {
            PlayerPrefs.SetInt(key, 1); // Đánh dấu đã hoàn thành
            PlayerPrefs.Save();
            FirstWin.SetActive(true);
            // Thêm phần thưởng lần đầu tiên
            SeedManager.Instance.AddSeed(SeedReward);
            EnergyManager.Instance.AddEnergy(EnergyReward);
        }
        else
        {
            Debug.Log($"{sceneName} đã hoàn thành trước đó, không nhận thêm phần thưởng.");
        }
    }

    public void CollectSeed()
    {
        string key1 = "FirstTimeReward_" + sceneName;
        if (!PlayerPrefs.HasKey(key1)) // Nếu chưa từng nhận thưởng
        {
            PlayerPrefs.SetInt(key1, 1); // Đánh dấu đã nhận thưởng
            PlayerPrefs.Save();
            PerfectWin.SetActive(true);
            // Thêm phần thưởng
            SeedManager.Instance.AddSeed(SeedReward);
            EnergyManager.Instance.AddEnergy(EnergyReward);
        }
        else
        {
            Debug.Log($"{sceneName} đã từng hoàn thành trước đó, không nhận phần thưởng.");
        }
    }

    [ContextMenu("Reset")]

    public void Reset()
    {
        PlayerPrefs.DeleteKey("FirstTimeReward_" + sceneName);
        PlayerPrefs.DeleteKey("Completed_" + sceneName);
        PlayerPrefs.Save();
    }
}
