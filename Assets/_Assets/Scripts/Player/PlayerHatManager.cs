using System.Collections.Generic;
using UnityEngine;

public class PlayerHatManager : MonoBehaviour
{
    public Transform hatParent; // Vị trí đặt mũ trên player
    public List<HatData> allHats; // Danh sách tất cả mũ

    private GameObject currentHat;

    void Start()
    {
        int selectedHatID = PlayerPrefs.GetInt("SelectedHatID", -1);
        if (selectedHatID != -1)
        {
            HatData hatToEquip = allHats.Find(h => h.id == selectedHatID);
            if (hatToEquip != null)
            {
                EquipHat(hatToEquip);
            }
        }
    }

    void EquipHat(HatData hatData)
    {
        if (currentHat != null)
        {
            Destroy(currentHat); // Xóa mũ cũ nếu có
        }

        currentHat = Instantiate(hatData.hatPrefab, hatParent);
        currentHat.transform.localPosition = hatData.hatOffset; // Đảm bảo đúng vị trí
        currentHat.transform.localRotation = Quaternion.identity;
    }
}
