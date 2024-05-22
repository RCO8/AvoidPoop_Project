using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStat;

    public CharacterStat CurrentStat { get; private set; }

    public List<CharacterStat> statModifiers = new List<CharacterStat>();

    public int currentBulletsPerShot;
    public bool canAttacked = true;

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        AttackSO attackSO = null;

        if (null != baseStat.attackSO)
            attackSO = Instantiate(baseStat.attackSO);

        CurrentStat = new CharacterStat { attackSO = attackSO };

        CurrentStat.statsChangeType = baseStat.statsChangeType;
        CurrentStat.maxHealth = baseStat.maxHealth;
        CurrentStat.speed = baseStat.speed;

        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;

        if (null != rangedAttackSO)
            currentBulletsPerShot = rangedAttackSO.numberOfBulletsPerShot;
        else
            currentBulletsPerShot = 1;
    }
}