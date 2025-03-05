using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HatType { Purchasable, EventReward }
[CreateAssetMenu(fileName = "NewHat", menuName = "Hat Shop/Hat Data")]
public class HatData : ScriptableObject
{
    public int id;
    public GameObject hatPrefab; // Tham chiếu đến Prefab của mũ
    public int price;
    public Vector3 hatOffset;
    public HatType hatType;
}
