using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private CharacterStatsHandler statsHandler;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

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
        if (isAttacked && (timeSinceLastChange < healthChangeDelay))
        {
            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibillityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (timeSinceLastChange < healthChangeDelay)
            return false;

        timeSinceLastChange = 0f;

        CurrentHP += change;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHealth);

        if (0f >= CurrentHP)
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