using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FollowCameraRotation))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] bool isBillboarded = true;
    [SerializeField] bool shouldShowHealthNumbers = true;

    float finalValue;
    float animationSpeed = 0.1f;
    float leftoverAmount = 0f;

    // Caches
    HealthSystem healthSystem;
    Image image;
    Text text;
    FollowCameraRotation followCameraRotation;

    private void Start()
    {

        healthSystem = GetComponentInParent<HealthSystem>();
        if (healthSystem == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
                healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        }
        if(healthSystem != null)
        healthSystem.OnCurrentHealthChanged.AddListener(ChangeHealthFill);
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
        followCameraRotation = GetComponent<FollowCameraRotation>();
    }

    void Update()
    {
        if (healthSystem == null)
        {
            healthSystem = GetComponentInParent<HealthSystem>();
            if (healthSystem == null)
            {
                healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
            }
            image = GetComponentInChildren<Image>();
            text = GetComponentInChildren<Text>();
            followCameraRotation = GetComponent<FollowCameraRotation>();
        }
        animationSpeed = healthSystem.AnimationDuration;

        if (!healthSystem.HasAnimationWhenHealthChanges)
        {
            image.fillAmount = healthSystem.CurrentHealthPercentage / 100;
            //Debug.Log("healthbar: "+image.fillAmount);
        }

        text.text = $"{healthSystem.CurrentHealth} / {(int)healthSystem.MaximumHealth}";

        text.enabled = shouldShowHealthNumbers;

        followCameraRotation.enabled = isBillboarded;
    }

    private void ChangeHealthFill(CurrentHealth currentHealth)
    {
        if (!healthSystem.HasAnimationWhenHealthChanges) return;

        StopAllCoroutines();
        StartCoroutine(ChangeFillAmount(currentHealth));
    }

    private IEnumerator ChangeFillAmount(CurrentHealth currentHealth)
    {
        finalValue = currentHealth.percentage / 100;

        float cacheLeftoverAmount = this.leftoverAmount;

        float timeElapsed = 0;

        while (timeElapsed < animationSpeed)
        {
            float leftoverAmount = Mathf.Lerp((currentHealth.previous / healthSystem.MaximumHealth) + cacheLeftoverAmount, finalValue, timeElapsed / animationSpeed);
            this.leftoverAmount = leftoverAmount - finalValue;
            image.fillAmount = leftoverAmount;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        this.leftoverAmount = 0;
        image.fillAmount = finalValue;

    }

    public void SetHealthSystem()
    {
        if (healthSystem == null)
        {
            healthSystem = GetComponentInParent<HealthSystem>();
            if (healthSystem == null)
            {
                healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
            }
            healthSystem.OnCurrentHealthChanged.AddListener(ChangeHealthFill);
        }
    }
}