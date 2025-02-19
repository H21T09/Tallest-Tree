using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SeedDisplay : MonoBehaviour
{
    public TMP_Text SeedText;

    
    private void Update()
    {
        int seed = PlayerPrefs.GetInt("Seed", 0);
        SeedText.text = seed.ToString();
    }
}
