using UnityEngine;
using System.Collections.Generic;

public class HatShopManager : MonoBehaviour
{
    public static HatShopManager Instance;
    public List<HatData> allHats;
    public HashSet<int> eventHats = new HashSet<int>(); // Lưu ID mũ nhận từ sự kiện

    public Transform hatParent; // Vị trí đeo mũ trên Player
    public SeedManager seedManager;

    private GameObject currentHat;
    public SpriteRenderer HatDefault;
    public SpriteRenderer Strap;

    public HashSet<int> ownedHats = new HashSet<int>(); // Lưu ID các mũ đã mua
    private int selectedHatID = -1; // Lưu ID mũ đang đeo

    public AudioClip buyEffect;
    public AudioSource audioSource;

    public AudioClip equipEffect1;
    public AudioSource audioSource1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        audioSource.playOnAwake = false;
        audioSource.clip = buyEffect;

        audioSource1.playOnAwake = false;
        audioSource1.clip = equipEffect1;
    }

    void Start()
    {
        LoadOwnedHats(); // Load danh sách mũ đã mua khi vào game
    }

    public bool CanPurchaseHat(HatData hatData)
    {
        return hatData.hatType == HatType.Purchasable;
    }

    public void BuyOrEquipHat(HatData hatData)
    {
        if (ownedHats.Contains(hatData.id))
        {
            audioSource1.Play();
            EquipHat(hatData);
        }
        else if (hatData.hatType == HatType.Purchasable) // Chỉ mua nếu là mũ có thể mua
        {
            if (seedManager.GetSeed() >= hatData.price)
            {
                audioSource.Play();

                seedManager.SpendSeed(hatData.price);
                ownedHats.Add(hatData.id);
                SaveOwnedHats();
                EquipHat(hatData);
            }
            else
            {
                Debug.Log("Không đủ seed để mua mũ này!");
            }
        }
        else
        {
            Debug.Log("Mũ này chỉ có thể nhận qua sự kiện!");
        }
    }


    public void EquipHat(HatData hatData)
    {
        if (currentHat != null)
        {
            Destroy(currentHat);
            HatDefault.enabled = false;
            Strap.enabled = false;
        }

        currentHat = Instantiate(hatData.hatPrefab, hatParent);
        currentHat.transform.localPosition = hatData.hatOffset;
        currentHat.transform.localRotation = Quaternion.identity;

        selectedHatID = hatData.id;
        PlayerPrefs.SetInt("SelectedHatID", selectedHatID);
        PlayerPrefs.SetString($"HatUIState_{hatData.id}", "Equipped"); // Lưu trạng thái UI
        PlayerPrefs.Save();
    }




    void LoadOwnedHats()
    {
        string savedHats = PlayerPrefs.GetString("OwnedHats", "");
        if (!string.IsNullOrEmpty(savedHats))
        {
            string[] ids = savedHats.Split(',');
            foreach (var id in ids)
            {
                if (int.TryParse(id, out int hatId))
                {
                    ownedHats.Add(hatId);
                }
            }
        }

        // Kiểm tra xem có mũ sự kiện nào đã nhận chưa
        string eventHats = PlayerPrefs.GetString("EventHatsReceived", ""); // Lưu danh sách ID mũ sự kiện
        if (!string.IsNullOrEmpty(eventHats))
        {
            string[] eventHatIDs = eventHats.Split(',');
            bool equippedEventHat = false; // Biến kiểm tra có trang bị mũ sự kiện hay không

            foreach (var id in eventHatIDs)
            {
                if (int.TryParse(id, out int eventHatID) && !ownedHats.Contains(eventHatID))
                {
                    HatData eventHat = allHats.Find(h => h.id == eventHatID);
                    if (eventHat != null)
                    {
                        ownedHats.Add(eventHatID);
                        equippedEventHat = true; // Đánh dấu đã trang bị mũ sự kiện
                    }
                }
            }

            SaveOwnedHats();

            // Nếu chưa có mũ nào được trang bị, trang bị một mũ sự kiện
            if (selectedHatID == -1 && equippedEventHat)
            {
                HatData firstEventHat = allHats.Find(h => ownedHats.Contains(h.id) && h.hatType == HatType.EventReward);
                if (firstEventHat != null)
                {
                    EquipHat(firstEventHat);
                }
            }

            // Xóa danh sách mũ sự kiện đã nhận để tránh nhận lại
            PlayerPrefs.DeleteKey("EventHatsReceived");
            PlayerPrefs.Save();
        }

        selectedHatID = PlayerPrefs.GetInt("SelectedHatID", -1);

        // Nếu không có mũ nào được trang bị, chọn mũ mặc định
        if (selectedHatID == -1 && allHats.Count > 0)
        {
            HatData firstHat = allHats[0];
            if (ownedHats.Contains(firstHat.id))
            {
                EquipHat(firstHat);
            }
        }
        else if (selectedHatID != -1)
        {
            HatData lastHat = allHats.Find(h => h.id == selectedHatID);
            if (lastHat != null)
            {
                EquipHat(lastHat);
            }
        }
    }





    void SaveOwnedHats()
    {
        string ownedHatsString = string.Join(",", ownedHats);
        PlayerPrefs.SetString("OwnedHats", ownedHatsString);
        PlayerPrefs.SetInt("OwnedHatCount", ownedHats.Count); // Lưu số lượng mũ đã mua
        PlayerPrefs.Save();
    }

    public bool IsHatOwned(int hatId)
    {
        return ownedHats.Contains(hatId);
    }

    [ContextMenu("reset")]
    public void ResetPurchasedHats()
    {
        ownedHats.Clear();
        PlayerPrefs.DeleteKey("OwnedHats");
        PlayerPrefs.DeleteKey("SelectedHatID");

        // Xóa tất cả trạng thái UI của các mũ đã lưu
        foreach (HatData hat in allHats)
        {
            PlayerPrefs.DeleteKey($"HatUIState_{hat.id}");
        }

        PlayerPrefs.Save();

        Debug.Log("Đã reset danh sách mũ và trạng thái UI!");

        // Cập nhật lại UI của tất cả các button mũ
        HatButton[] hatButtons = FindObjectsOfType<HatButton>();
        foreach (HatButton button in hatButtons)
        {
            button.ResetUI(); // Gọi phương thức reset UI trên từng nút
        }
       
    }

}
