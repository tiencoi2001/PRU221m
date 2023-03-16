using Assets.Scripts.Entity.State;
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public bool IsAlive;
    public float CurrentHealth;
    public float MaximumHealth;

    public bool HasAnimationWhenHealthChanges = false;
    public float AnimationDuration = 0.1f;

    public float CurrentHealthPercentage
    {
        get
        {
            return (CurrentHealth / MaximumHealth) * 100;
        }
    }

    public OnCurrentHealthChanged OnCurrentHealthChanged;
    public OnIsAliveChanged OnIsAliveChanged;
    public OnMaximumHealthChanged OnMaximumHealthChanged;

    public GameObject HealthBarPrefabToSpawn;

    public void AddToMaximumHealth(float value)
    {
        float cachedMaximumHealth = MaximumHealth;

        MaximumHealth += value;
        OnMaximumHealthChanged.Invoke(new MaximumHealth(cachedMaximumHealth, MaximumHealth));
    }

    public void AddToCurrentHealth(float value)
    {
        if (value == 0) return;

        float cachedCurrentHealth = CurrentHealth;

        if (value > 0)
        {
            GotHealedFor(value);
        }
        else
        {
            GotHitFor(damage: value);
        }

        OnCurrentHealthChanged.Invoke(new CurrentHealth(cachedCurrentHealth, CurrentHealth, CurrentHealthPercentage));
    }

    public void GotHealedFor(float value)
    {
        CurrentHealth += value;

        if (CurrentHealth > MaximumHealth)
        {
            CurrentHealth = MaximumHealth;
        }

        if (!IsAlive)
        {
            ReviveWithCustomHealth(CurrentHealth);
        }
    }

    public void GotHitFor(float damage)
    {
        if (!IsAlive) { return; }

        float absoluteValue = (int) Mathf.Abs(damage);
        DecreaseCurrentHealthBy(absoluteValue);
    }

    void DecreaseCurrentHealthBy(float value)
    {
        CurrentHealth -= value;

        if (CurrentHealth <= 0)
        {
            IsAlive = false;
            AudioManager.Instance.PlayAudioOneShot((AudioClip)Resources.Load("Audios/KillSound"), 0.1f);
            StartCoroutine(InvokeOnIsAliveChangedWithDelay());
        }
    }
    IEnumerator InvokeOnIsAliveChangedWithDelay()
    {
        yield return new WaitForSeconds(0f); // wait for 0.5 seconds
        OnIsAliveChanged.Invoke(IsAlive);
    }

    public void ReviveWithMaximumHealth()
    {
        Revive(MaximumHealth);
    }

    public void ReviveWithCustomHealth(float healthWhenRevived)
    {
        Revive(healthWhenRevived);
    }

    public void ReviveWithCustomHealthPercentage(float healthPercentageWhenRevived)
    {
        Revive(MaximumHealth * (healthPercentageWhenRevived / 100));
    }

    void Revive(float health)
    {
        float previousHealth = CurrentHealth;

        CurrentHealth = health;
        IsAlive = true;

        OnIsAliveChanged.Invoke(IsAlive);
        OnCurrentHealthChanged.Invoke(new CurrentHealth(previousHealth, CurrentHealth, CurrentHealthPercentage));
    }

    public void Kill()
    {
        float previousHealth = CurrentHealth;
        
        CurrentHealth = 0;
        IsAlive = false;

        //ChangeState(new DieState(this));
        AudioManager.Instance.PlayAudioOneShot((AudioClip)Resources.Load("Audios/KillSound"), 0.1f);
        OnIsAliveChanged.Invoke(IsAlive);
        OnCurrentHealthChanged.Invoke(new CurrentHealth(previousHealth, CurrentHealth, CurrentHealthPercentage));
    }

    //public void ChangeState(EnemyState newState)
    //{
    //    if (currentState != null)
    //    {
    //        currentState.Exit();
    //    }

    //    currentState = newState;

    //    if (currentState != null)
    //    {
    //        currentState.Enter();
    //    }
    //}

    //public void Dead(bool isAlive)
    //{
    //    if (isAlive)
    //        gameObject.SetActive(true);
    //    else
    //        gameObject.SetActive(false);
    //}
}