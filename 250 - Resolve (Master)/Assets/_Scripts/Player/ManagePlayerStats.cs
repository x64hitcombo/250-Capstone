using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePlayerStats : MonoBehaviour
{
    public float maxValue = 100;
    [Header("Hunger")]
    public float hungerDrain = 0.5f; //use fractions (1 will be max)
    public Image hungerBar;
    [Header("Thirst")]
    public float thirstDrain = .8f;
    public Image thirstBar;
    [Header("Fatigue")]
    public float fatigueDrain = .2f;
    public Image fatigueBar;

    public float exposureRating = 0;
    public Slider exposureBar;

    public float hpRefill;
    public Image healthBarLeft;
    public Image healthBarRight;

    public float staminaRefill;
    public Image staminaBar;
    

    //[HideInInspector]
    public float currentHealth, currentHunger, currentThirst, exposure, currentFatigue, currentStamina;

    public TemperatureManager tempManager;

	// Use this for initialization
	void Start ()
    {
        tempManager = this.gameObject.GetComponent<TemperatureManager>();
        currentHunger = maxValue;
        currentThirst = maxValue;
        currentHealth = maxValue;
        currentFatigue = maxValue;
        currentStamina = maxValue;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ManageDecreaseOverTime();
        HandleBarDisplays();
        exposure = tempManager.currentTemperature + exposureRating; //This will change when adding clothing
	}

    public void ManageDecreaseOverTime()
    {
        currentHunger -= Time.deltaTime * hungerDrain;
        currentThirst -= Time.deltaTime * thirstDrain;
        currentFatigue -= Time.deltaTime * fatigueDrain; //this may be changed later
    }

    public void HandleBarDisplays()
    {
        thirstBar.fillAmount = currentThirst / maxValue;
        hungerBar.fillAmount = currentHunger / maxValue;
        fatigueBar.fillAmount = currentFatigue / maxValue;
        exposureBar.value = exposure / maxValue;
        //healthBarLeft.fillAmount = currentHealth / maxValue;
        //healthBarRight.fillAmount = currentHealth / maxValue;
    }
}
