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

        button.onClick.AddListener(() => hatShopManager.BuyOrEquipHat(hatData));
    }
}
