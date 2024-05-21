using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;  //�����ð� ������ �� ��� - �����ð� �ֳ�..?

    private CharacterStatsHandler statsHandler;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    // ü�� ������ �� �� �ൿ
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
        if (isAttacked && timeSinceLastChange < healthChangeDelay)  //���� �޾�����
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false; //���ݹ����� ��
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (timeSinceLastChange < healthChangeDelay)  //�����ð��� ü�� �ȴ�� - �ٵ� �����ð� �����ϳ�..?
        {
            return false;  //���� ���ϰ� ������ ��Ȳ
        }

        timeSinceLastChange = 0f;
        CurrentHP += change;
        // �ּڰ� 0, �ִ� MaxHealth
        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHealth);

        if (CurrentHealth <= 0f)  //�׾����� - ü���� 0�̰ų� ����
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
