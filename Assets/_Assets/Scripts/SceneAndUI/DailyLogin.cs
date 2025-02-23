using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DailyLogin : MonoBehaviour
{
    public int LastDate;
    private int seed;
    private int Energy;

    public int Day_1;
    public GameObject Ready_1;
    public GameObject NotReady_1;
    public GameObject IsClaimed_1;

    public int Day_2;
    public GameObject Ready_2;
    public GameObject NotReady_2;
    public GameObject IsClaimed_2;
    
    public int Day_3;
    public GameObject Ready_3;
    public GameObject NotReady_3;
    public GameObject IsClaimed_3;
    
    public int Day_4;
    public GameObject Ready_4;
    public GameObject NotReady_4;
    public GameObject IsClaimed_4;
    
    public int Day_5;
    public GameObject Ready_5;
    public GameObject NotReady_5;
    public GameObject IsClaimed_5;

    public int Day_6;
    public GameObject Ready_6;
    public GameObject NotReady_6;
    public GameObject IsClaimed_6;


    public int Day_7;
    public GameObject Ready_7;
    public GameObject NotReady_7;
    public GameObject IsClaimed_7;




    private void Start()
    {
        Day_1 = PlayerPrefs.GetInt("Day_1");
        Day_2 = PlayerPrefs.GetInt("Day_2");
        Day_3 = PlayerPrefs.GetInt("Day_3");
        Day_4 = PlayerPrefs.GetInt("Day_4");
        Day_5 = PlayerPrefs.GetInt("Day_5");
        Day_6 = PlayerPrefs.GetInt("Day_6");
        Day_7 = PlayerPrefs.GetInt("Day_7");
        LastDate = PlayerPrefs.GetInt("LastDate");
        seed = PlayerPrefs.GetInt("Seed", 0);
        Energy = PlayerPrefs.GetInt("Energy", 0);
        Reward();
        Debug.Log("Day1" + Day_1);
        Debug.Log("Day2" + Day_2);
        Debug.Log(System.DateTime.Now.Day);
        

        if (LastDate != System.DateTime.Now.Day)
        {
            if (Day_1 == 0)
            {
                Day_1 = 1;
            }
            else if (Day_2 == 0 )  
            {
                Day_2 = 1;     
            }
            else if (Day_3 == 0)
            {
                Day_3 = 1;
            }
            else if (Day_4 == 0)
            {
                Day_4 = 1;
            }
            else if (Day_5 == 0)
            {
                Day_5 = 1;
            }
            else if (Day_6 == 0)
            {
                Day_6 = 1;
            }
            else if (Day_7 == 0)
            {
                Day_7 = 1;
            }


        }
        Reward();
        Debug.Log("Day1" + Day_1);

    }

    public void Reward()
    {
        //day1
        if(Day_1 == 1)
        {
            Ready_1.SetActive(true);
            NotReady_1.SetActive(false);
            IsClaimed_1.SetActive(true);
        }
        if(Day_1 == 2)
        {
            IsClaimed_1.SetActive(false);
        }


        //day2
        if (Day_2 == 0)
        {
            Ready_2.SetActive(false);
            NotReady_2.SetActive(true);
            IsClaimed_2.SetActive(true);
        }
        if (Day_2 == 1)
        {
            Ready_2.SetActive(true);
            NotReady_2.SetActive(false);
            IsClaimed_2.SetActive(true);
        }
        if (Day_2 == 2)
        {
            IsClaimed_2.SetActive(false);
        }

        //day3
        if (Day_3 == 0)
        {
            Ready_3.SetActive(false);
            NotReady_3.SetActive(true);
            IsClaimed_3.SetActive(true);
        }
        if (Day_3 == 1)
        {
            Ready_3.SetActive(true);
            NotReady_3.SetActive(false);
            IsClaimed_3.SetActive(true);
        }
        if (Day_3 == 2)
        {
            IsClaimed_3.SetActive(false);
        }

        //day4
        if (Day_4 == 0)
        {
            Ready_4.SetActive(false);
            NotReady_4.SetActive(true);
            IsClaimed_4.SetActive(true);
        }
        if (Day_4 == 1)
        {
            Ready_4.SetActive(true);
            NotReady_4.SetActive(false);
            IsClaimed_4.SetActive(true);
        }
        if (Day_4 == 2)
        {
            IsClaimed_4.SetActive(false);
        }

        //day5
        if (Day_5 == 0)
        {
            Ready_5.SetActive(false);
            NotReady_5.SetActive(true);
            IsClaimed_5.SetActive(true);
        }
        if (Day_5 == 1)
        {
            Ready_5.SetActive(true);
            NotReady_5.SetActive(false);
            IsClaimed_5.SetActive(true);
        }
        if (Day_5 == 2)
        {
            IsClaimed_5.SetActive(false);
        }


        //day6
        if (Day_6 == 0)
        {
            Ready_6.SetActive(false);
            NotReady_6.SetActive(true);
            IsClaimed_6.SetActive(true);
        }
        if (Day_6 == 1)
        {
            Ready_6.SetActive(true);
            NotReady_6.SetActive(false);
            IsClaimed_6.SetActive(true);
        }
        if (Day_6 == 2)
        {
            IsClaimed_6.SetActive(false);
        }

        //day7
        if (Day_7 == 0)
        {
            Ready_7.SetActive(false);
            NotReady_7.SetActive(true);
            IsClaimed_7.SetActive(true);
        }
        if (Day_7 == 1)
        {
            Ready_7.SetActive(true);
            NotReady_7.SetActive(false);
            IsClaimed_7.SetActive(true);
        }
        if (Day_7 == 2)
        {
            IsClaimed_7.SetActive(false);
        }
    }

    public void GetReward_1()
    {
        if(Day_1 == 1)
        {
            LastDate = System.DateTime.Now.Day;
            PlayerPrefs.SetInt("LastDate", LastDate);
            seed += 100;
            PlayerPrefs.SetInt("Seed", seed);
            PlayerPrefs.SetInt("TotalSeed", seed);
            Energy += 5;
            PlayerPrefs.SetInt("Energy", Energy);
            Day_1 = 2;
            PlayerPrefs.SetInt("Day_1", 2);
            PlayerPrefs.Save();
            Reward();
            EnergyManager.Instance.UpdateUI();
        }
        
    }

    public void GetReward_2()
    {
        if(Day_2 == 1)
        {
            LastDate = System.DateTime.Now.Day;
            PlayerPrefs.SetInt("LastDate", LastDate);
            seed += 150;
            PlayerPrefs.SetInt("Seed", seed);
            PlayerPrefs.SetInt("TotalSeed", seed);
            Energy += 5;
            PlayerPrefs.SetInt("Energy", Energy);
            Day_2 = 2;
            PlayerPrefs.SetInt("Day_2", 2);
            PlayerPrefs.Save();
            Reward();

            EnergyManager.Instance.UpdateUI();
        }
    }

    public void GetReward_3()
    {
        if (Day_3 == 1)
        {
            LastDate = System.DateTime.Now.Day;
            PlayerPrefs.SetInt("LastDate", LastDate);
            seed += 200;
            PlayerPrefs.SetInt("Seed", seed);
            PlayerPrefs.SetInt("TotalSeed", seed);
            Energy += 5;
            PlayerPrefs.SetInt("Energy", Energy);
            Day_3 = 2;
            PlayerPrefs.SetInt("Day_3", 2);
            PlayerPrefs.Save();
            Reward();

            EnergyManager.Instance.UpdateUI();
        }
    }

    public void GetReward_4()
    {
        if (Day_4 == 1)
        {
            LastDate = System.DateTime.Now.Day;
            PlayerPrefs.SetInt("LastDate", LastDate);
            seed += 250;
            PlayerPrefs.SetInt("Seed", seed);
            PlayerPrefs.SetInt("TotalSeed", seed);
            Energy += 5;
            PlayerPrefs.SetInt("Energy", Energy);
            Day_4 = 2;
            PlayerPrefs.SetInt("Day_4", 2);
            PlayerPrefs.Save();
            Reward();

            EnergyManager.Instance.UpdateUI();
        }
    }

    public void GetReward_5()
    {
        if (Day_5 == 1)
        {
            LastDate = System.DateTime.Now.Day;
            PlayerPrefs.SetInt("LastDate", LastDate);
            seed += 300;
            PlayerPrefs.SetInt("Seed", seed);
            PlayerPrefs.SetInt("TotalSeed", seed);
            Energy += 5;
            PlayerPrefs.SetInt("Energy", Energy);
            Day_5 = 2;
            PlayerPrefs.SetInt("Day_5", 2);
            PlayerPrefs.Save();
            Reward();

            EnergyManager.Instance.UpdateUI();
        }
    }

    public void GetReward_6()
    {
        if (Day_6 == 1)
        {
            LastDate = System.DateTime.Now.Day;
            PlayerPrefs.SetInt("LastDate", LastDate);
            seed += 500;
            PlayerPrefs.SetInt("Seed", seed);
            PlayerPrefs.SetInt("TotalSeed", seed);
            Energy += 5;
            PlayerPrefs.SetInt("Energy", Energy);
            Day_6 = 2;
            PlayerPrefs.SetInt("Day_6", 2);
            PlayerPrefs.Save();
            Reward();

            EnergyManager.Instance.UpdateUI();
        }
    }

    public void GetReward_7()
    {
        if (Day_7 == 1)
        {
            LastDate = System.DateTime.Now.Day;
            PlayerPrefs.SetInt("LastDate", LastDate);
            seed += 1000;
            PlayerPrefs.SetInt("Seed", seed);
            PlayerPrefs.SetInt("TotalSeed", seed);
            Energy += 5;
            PlayerPrefs.SetInt("Energy", Energy);
            Day_7 = 2;
            PlayerPrefs.SetInt("Day_7", 2);
            PlayerPrefs.Save();
            Reward();
            EnergyManager.Instance.UpdateUI();

        }
    }

    [ContextMenu("RESET")]
        private void Reset()
    {
        PlayerPrefs.DeleteKey("LastDate");
        PlayerPrefs.DeleteKey("Day_1");
        PlayerPrefs.DeleteKey("Day_2");
        PlayerPrefs.DeleteKey("Day_3");
        PlayerPrefs.DeleteKey("Day_4");
        PlayerPrefs.DeleteKey("Day_5");
        PlayerPrefs.DeleteKey("Day_6");
        PlayerPrefs.DeleteKey("Day_7");
    }
}
