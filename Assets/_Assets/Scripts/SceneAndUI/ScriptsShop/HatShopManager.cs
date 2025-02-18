using UnityEngine;
using System.Collections.Generic;

public class HatShopManager : MonoBehaviour
{
    public List<HatData> allHats;
    public Transform hatParent; // Vị trí đeo mũ trên Player
    public SeedManager seedManager; // Quản lý seed (gán trong Inspector)

    private GameObject currentHat;
    public SpriteRenderer HatDefault;
    public SpriteRenderer Strap;

    private HashSet<int> ownedHats = new HashSet<int>(); // Lưu ID các mũ đã mua
    private int selectedHatID = -1; // Lưu ID mũ đang đeo

    void Start()
    {
        LoadOwnedHats(); // Load danh sách mũ đã mua khi vào game
    }

    public void BuyOrEquipHat(HatData hatData)
    {
        if (ownedHats.Contains(hatData.id))
        {
            EquipHat(hatData); // Nếu đã mua thì chỉ đeo vào
        }
        else if (seedManager.GetSeed() >= hatData.price)
        {
            // Nếu đủ seed, thực hiện mua
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

    private void EquipHat(HatData hatData)
    {
        if (currentHat != null)
        {
            Destroy(currentHat); // Xóa mũ cũ nếu có
            HatDefault.enabled = false;
            Strap.enabled = false;
        }


        currentHat = Instantiate(hatData.hatPrefab, hatParent);
        currentHat.transform.localPosition = hatData.hatOffset; // Giữ đúng vị trí gốc
        currentHat.transform.localRotation = Quaternion.identity; // Đảm bảo không xoay sai hướng

        selectedHatID = hatData.id;
        PlayerPrefs.SetInt("SelectedHatID", selectedHatID);
    }


    private void LoadOwnedHats()
    {
        string savedHats = PlayerPrefs.GetString("OwnedHats", "");
        if (!string.IsNullOrEmpty(savedHats))
        {
            string[] ids = savedHats.Split(',');
            foreach (var id in ids)
            {
                ownedHats.Add(int.Parse(id));
            }
        }

        // Load mũ cuối cùng đã chọn
        selectedHatID = PlayerPrefs.GetInt("SelectedHatID", -1);
        if (selectedHatID != -1)
        {
            HatData lastHat = allHats.Find(h => h.id == selectedHatID);
            if (lastHat != null)
            {
                EquipHat(lastHat);
            }
        }
    }

    private void SaveOwnedHats()
    {
        PlayerPrefs.SetString("OwnedHats", string.Join(",", ownedHats));
    }
    [ContextMenu ("reset")]
    public void ResetPurchasedHats()
    {
        ownedHats.Clear(); // Xóa danh sách mũ đã mua trong bộ nhớ
        PlayerPrefs.DeleteKey("OwnedHats"); // Xóa dữ liệu lưu trong PlayerPrefs
        PlayerPrefs.DeleteKey("SelectedHatID"); // Xóa mũ đang đeo
        PlayerPrefs.Save();

        Debug.Log("Đã reset danh sách mũ!");
    }

}
