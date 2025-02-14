using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SeedCounter : MonoBehaviour
{
    public TextMeshProUGUI seedText;
    public TextMeshProUGUI seedWinText;

    public int collectedSeeds = 0;
    public List<GameObject> totalSeeds;

    public int total;


    void Update()
    {
        UpdateSeedText();
        total = totalSeeds.Count;
    }

    public void CollectSeed()
    {
        collectedSeeds++;
        UpdateSeedText();
    }

    void UpdateSeedText()
    {
        seedText.text = collectedSeeds + " / " + total;
        seedWinText.text = collectedSeeds + " / " + total;

    }
}
