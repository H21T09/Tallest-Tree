using UnityEngine;

public class SeedCollector : MonoBehaviour
{
    private int seed;
    public AudioClip soundEffect;

    public AudioSource audioSource;
    void Start()
    {
        seed = PlayerPrefs.GetInt("Seed", 0);
        
        audioSource.playOnAwake = false;
        audioSource.clip = soundEffect;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Seed"))
        {
            SeedManager.Instance.AddSeed(1);
            audioSource.Play();
        }
    }
   

    public int GetGold()
    {
        return seed;
    }
}
