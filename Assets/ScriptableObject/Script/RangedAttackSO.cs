using UnityEngine;

[CreateAssetMenu (fileName = "RangedAttackSO", menuName = "Controller/Attacks/Ranged", order = 1)]
public class RangedAttackSO : AttackSO
{
    [Header("Ranged Attack Info")]
    public float spread;
    public int numberOfBulletsPerShot;
    public float multipleBulletsAngle;
    public Color bulletColor;
}