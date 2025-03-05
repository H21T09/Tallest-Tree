using UnityEngine;
using UnityEngine.UI;

public class HatButton : MonoBehaviour
{
    public HatData hatData;
    private Button button;
    private HatShopManager hatShopManager;

    void Start()
    {
        button = GetComponent<Button>();
        hatShopManager = FindObjectOfType<HatShopManager>();

        button.onClick.AddListener(OnHatButtonClick);

        // Cập nhật UI dựa vào dữ liệu đã lưu
        LoadUIState();
    }

    void OnHatButtonClick()
    {
        bool isOwned = hatShopManager.IsHatOwned(hatData.id);
        bool isEventHat = hatData.hatType == HatType.EventReward;
        bool canBuy = hatShopManager.seedManager.GetSeed() >= hatData.price;

        if (isOwned)
        {
            SetOn("Equip");
            SetOn("ItemOpen");
            SetOff("ImageForComplete");
            SetOff("ImageForPrice");
            hatShopManager.EquipHat(hatData);
            SaveUIState("Equipped");
        }
        else if (isEventHat)
        {
            Debug.Log("Mũ này là mũ sự kiện và chỉ có thể nhận từ sự kiện.");
        }
        else if (canBuy)
        {
            hatShopManager.BuyOrEquipHat(hatData);
            SetOn("ItemOpen");
            SetOn("Equip");
            SetOff("Item");
            SetOff("ImageForPrice");
            SaveUIState("Purchased");
        }
        else
        {
            Debug.Log("Không đủ seed để mua mũ này!");
        }
    }


    void LoadUIState()
    {
        string uiState = PlayerPrefs.GetString($"HatUIState_{hatData.id}", "Default");

        if (uiState == "Equipped")
        {
            SetOn("Equip");
            SetOn("ItemOpen");
            SetOff("ImageForComplete");
            SetOff("ImageForPrice");
        }
        else if (uiState == "Purchased")
        {
            SetOn("ItemOpen");
            SetOn("Equip");
            SetOff("Item");
            SetOff("ImageForPrice");
        }



    }

    void SaveUIState(string state)
    {
        PlayerPrefs.SetString($"HatUIState_{hatData.id}", state);
        PlayerPrefs.Save();
    }

    void SetOn(string childName)
    {
        Transform child = transform.Find(childName);
        if (child != null) child.gameObject.SetActive(true);
    }

    void SetOff(string childName)
    {
        Transform child = transform.Find(childName);
        if (child != null) child.gameObject.SetActive(false);
    }

    public void ResetUI()
    {
        SetOff("Equip");
        SetOff("ItemOpen");
        SetOn("Item");
        SetOn("ImageForPrice");

        Debug.Log($"Reset UI cho mũ {hatData.id}");
    }
}
