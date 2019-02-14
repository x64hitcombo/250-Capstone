using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePlayerStats : MonoBehaviour
{
    public float maxValue = 100;
    public float hpRefill = 5;

    [Header("Hunger")]
    public float hungerDrain = 0.3f; //use fractions (1 will be max)
    public Image hungerBar;
    private bool hDebuff1 = false;
    private bool hDebuff2 = false;
    [Header("Thirst")]
    public float thirstDrain = .4f;
    public Image thirstBar;
    private bool tDebuff1 = false;
    private bool tDebuff2 = false;
    [Header("Fatigue")]
    public float fatigueDrain = .1f;
    public Image fatigueBar;
    private bool fDebuff1 = false;
    private bool fDebuff2 = false;
    [Header("Exposure")]
    public float exposureRating = 0;
    public Slider exposureSlider;
    private bool eDebuff1 = false;
    private bool eDebuff2 = false;

    //public Image healthBarLeft;
    //public Image healthBarRight;
    [Header("Stamina")]
    public float staminaRefill;
    public Image staminaBar;
    

    //[HideInInspector]
    public float currentHunger, currentThirst, currentExposure, currentFatigue, currentStamina;

    public TemperatureManager tempManager;

	// Use this for initialization
	void Start ()
    {
        tempManager = this.gameObject.GetComponent<TemperatureManager>();
        currentHunger = maxValue;
        currentThirst = maxValue;
        //currentHealth = maxValue;
        currentFatigue = maxValue;
        currentStamina = maxValue;

    }

    // Update is called once per frame
    void Update ()
    {
        ManageDecreaseOverTime();
        HandleBarDisplays();
        currentExposure = tempManager.currentTemperature + exposureRating; //This will change when adding clothing
	}

    public void ManageDecreaseOverTime()
    {
        this.gameObject.GetComponent<Health>().curHealth += Time.deltaTime * hpRefill;

        currentHunger -= Time.deltaTime * hungerDrain;
        if (currentHunger < 0 )
        {
            currentHunger = 0;
        }

        if (currentHunger <= (maxValue / 2) && currentHunger >= (maxValue / 4))
        {
            hDebuff1 = true;
        }
        else if (currentHunger <= (maxValue / 4))
        {
            hDebuff2 = true;
        }

        currentThirst -= Time.deltaTime * thirstDrain;
        if (currentThirst < 0)
        {
            currentThirst = 0;
        }

        if (currentThirst <= (maxValue / 2) && currentThirst >= (maxValue / 4))
        {
            tDebuff1 = true;
        }
        else if (currentThirst <= (maxValue / 4))
        {
            tDebuff2 = true;
        }

        currentFatigue -= Time.deltaTime * fatigueDrain;

        if (currentFatigue <= (maxValue / 2))
        {
            fDebuff1 = true;
        }
        else if (currentFatigue <= (maxValue / 4))
        {
            fDebuff2 = true;
        }
    }

    public void HandleDebuffs()
    {
        float drainValueOne = .2f;
        float drainValueTwo = .3f;

        if (hDebuff1)
        {
            fatigueDrain += drainValueOne; //this may be changed later
        }

        if (hDebuff2)
        {
            fatigueDrain += drainValueTwo;
            hpRefill = 0;
        }

        if (tDebuff1)
        {
            fatigueDrain = +drainValueOne; //this may be changed later
        }
        if (tDebuff2)
        {
            fatigueDrain = +drainValueTwo;
            this.gameObject.GetComponent<PlayerController>().baseMovementSpeed = -.2f;
        }
        if (fDebuff1)
        {
            this.gameObject.GetComponent<PlayerController>().baseMovementSpeed = -this.gameObject.GetComponent<PlayerController>().baseMovementSpeed * drainValueOne;
        }
        if (fDebuff2)
        {
            this.gameObject.GetComponent<Health>().maxHealth = -maxValue * drainValueTwo;
        }
        if (eDebuff1)
        {

        }
        if (eDebuff2)
        {

        }
    }

    public void HandleBarDisplays()
    {
        thirstBar.fillAmount = currentThirst / maxValue;
        hungerBar.fillAmount = currentHunger / maxValue;
        fatigueBar.fillAmount = currentFatigue / maxValue;
        exposureSlider.value = currentExposure / maxValue;
        //healthBarLeft.fillAmount = currentHealth / maxValue;
        //healthBarRight.fillAmount = currentHealth / maxValue;
    }

    
}
