using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackSO", menuName = "Controller/Attacks/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;
}