using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Controller/Attacks/Item", order = 0)]

public class ItemSO : ScriptableObject
{
    [Header("Item Info")]
    public float increase;
    public ItemType type;
}