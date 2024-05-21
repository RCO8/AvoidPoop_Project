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
    public event Action OnInvincibilityEnd;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => statsHandler.CurrentStat.maxHealth;

    private void Awake()
    {
        statsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        CurrentHealth = statsHandler.CurrentStat.maxHealth;
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
        CurrentHealth += change;
        // �ּڰ� 0, �ִ� MaxHealth
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
     
        if (CurrentHealth <= 0f)  //�׾����� - ü���� 0�̰ų� ����
        {
            CallDeath();
            return true;
        }

        if (change >= 0)
        {
            OnHeal?.Invoke();
        }
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
