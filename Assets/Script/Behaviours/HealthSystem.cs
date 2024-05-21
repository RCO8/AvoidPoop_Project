using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;  //무적시간 정의할 때 사용 - 무적시간 있나..?

    private CharacterStatsHandler statsHandler;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    // 체력 변했을 때 할 행동
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibillityEnd;

    public float MaxHealth => statsHandler.CurrentStat.maxHealth;
    public float CurrentHP { get; private set; }

    private void Awake()
    {
        statsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        CurrentHP = MaxHealth;
    }

    private void Update()
    {
        if (isAttacked && timeSinceLastChange < healthChangeDelay)  //공격 받았을때
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false; //공격받은거 끝
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (timeSinceLastChange < healthChangeDelay)  //무적시간에 체력 안닳게 - 근데 무적시간 적용하나..?
        {
            return false;  //공격 안하고 끝나는 상황
        }

        timeSinceLastChange = 0f;
        CurrentHP += change;
        // 최솟값 0, 최댓값 MaxHealth
        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHealth);

        if (CurrentHealth <= 0f)  //죽었을때 - 체력이 0이거나 작음
        {
            CallDeath();
            return true;
        }
        
        if (0f <= change)
            OnHeal?.Invoke();
        else
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}
