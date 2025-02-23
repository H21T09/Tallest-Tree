using UnityEngine;
using System.Collections.Generic;

public class HatShopManager : MonoBehaviour
{
    public static HatShopManager Instance;
    public List<HatData> allHats;
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

        audioSource.playOnAwake = false;
        audioSource.clip = buyEffect;

        audioSource1.playOnAwake = false;
        audioSource1.clip = equipEffect1;
    }

    void Start()
    {
        LoadOwnedHats(); // Load danh sách mũ đã mua khi vào game


    }

    

    public void BuyOrEquipHat(HatData hatData)
    {
        if (ownedHats.Contains(hatData.id))
        {
            audioSource1.Play();
            EquipHat(hatData);
        }
        else if (seedManager.GetSeed() >= hatData.price)
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

        selectedHatID = PlayerPrefs.GetInt("SelectedHatID", -1);

        // Nếu chưa có mũ nào được chọn, chọn mũ đầu tiên trong danh sách
        if (selectedHatID == -1 && allHats.Count > 0)
        {
            HatData firstHat = allHats[0];
            if (ownedHats.Contains(firstHat.id)) // Kiểm tra xem mũ đầu tiên đã được mua chưa
            {
                EquipHat(firstHat);
            }
            else
            {
                // Nếu chưa sở hữu mũ đầu tiên, thêm nó vào danh sách và trang bị luôn
                ownedHats.Add(firstHat.id);
                SaveOwnedHats();
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
