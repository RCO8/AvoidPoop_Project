using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Controller/Items", order = 2)]

public class ItemSO : ScriptableObject
{
    [Header("Item Info")]
    public float increase;
    public ItemType type;
    public LayerMask target;
}