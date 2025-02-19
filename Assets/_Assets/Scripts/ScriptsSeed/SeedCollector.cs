using UnityEngine;

public class SeedCollector : MonoBehaviour
{
    private int seed;

    void Start()
    {
        seed = PlayerPrefs.GetInt("Seed", 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Seed"))
        {
            seed += 100;
            PlayerPrefs.SetInt("Seed", seed);
            PlayerPrefs.Save();
        }
    }
   

    public int GetGold()
    {
        return seed;
    }
}
