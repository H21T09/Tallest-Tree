using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Achievements : MonoBehaviour
{
    private int Seed;
    private int Energy;

    //phần Seed
    public int SeedCount_1;
    public GameObject Ready_1;
    public GameObject NotReady_1;
    public GameObject Button_1;
    public GameObject Claimed_1;
    public GameObject NOTClaimed_1;
    public TMP_Text Text_1;
    public Slider slider_1;

    //Phần Mũ
    public int HatCount_1;
    public GameObject Ready_2;
    public GameObject NotReady_2;
    public GameObject Button_2;
    public GameObject Claimed_2;
    public GameObject NOTClaimed_2;
    public TMP_Text Text_2;
    public Slider slider_2;

    //Phần Way
    public int WayCount_1;
    public GameObject Ready_3;
    public GameObject NotReady_3;
    public GameObject Button_3;
    public GameObject Claimed_3;
    public GameObject NOTClaimed_3;
    public TMP_Text Text_3;
    public Slider slider_3;


    public void Start()
    {
        Check_1();
        Check_2();
        Check_3();
    }
    
    void Check_1()
    {
        SeedCount_1 = PlayerPrefs.GetInt("TotalSeed");
        slider_1.value = SeedCount_1;
        Text_1.text = SeedCount_1 + "/1000";
        if (SeedCount_1 >= 1000)
        {
            Ready_1.SetActive(true);
            NotReady_1.SetActive(false);
        }
    }

    public void GetGatherSeed_1()
    {
        if (SeedCount_1 < 1000) return;
        Button_1.SetActive(false);
        NOTClaimed_1.SetActive(false);
        Claimed_1.SetActive(true);
        SeedManager.Instance.AddSeed(500);
        
    }

    void Check_2()
    {
        HatCount_1 = PlayerPrefs.GetInt("OwnedHatCount", 0);
        slider_2.value = HatCount_1;
        Text_2.text = HatCount_1 + "/5";
        if (HatCount_1 >= 5)
        {
            Ready_1.SetActive(true);
            NotReady_1.SetActive(false);
        }
    }

    public void GetHatReward_2()
    {
        if (HatCount_1 < 5) return;
        Button_2.SetActive(false);
        NOTClaimed_2.SetActive(false);
        Claimed_2.SetActive(true);
        SeedManager.Instance.AddSeed(500);

    }

    void Check_3()
    {
        WayCount_1 = LevelManager.Instance.GetCompletedLevels();
        slider_3.value = WayCount_1;
        Text_3.text = WayCount_1 + "/8";
        if (WayCount_1 >= 8)
        {
            Ready_1.SetActive(true);
            NotReady_1.SetActive(false);
        }
    }

    public void GetWayReward_1()
    {
        if (WayCount_1 < 8) return;
        Button_3.SetActive(false);
        NOTClaimed_3.SetActive(false);
        Claimed_3.SetActive(true);
        SeedManager.Instance.AddSeed(250);

    }
}
