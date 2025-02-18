using UnityEngine;

public class HatEquipper : MonoBehaviour
{
    public Transform hatParent; // Vị trí đặt mũ trên Player
    private GameObject currentHat;

    public void EquipHat(HatData hatData)
    {
        if (currentHat != null)
        {
            Destroy(currentHat); // Xóa mũ cũ nếu có
        }

        currentHat = Instantiate(hatData.hatPrefab, hatParent);
        currentHat.transform.localPosition = Vector3.zero; // Đảm bảo mũ ở đúng vị trí
    }
}
