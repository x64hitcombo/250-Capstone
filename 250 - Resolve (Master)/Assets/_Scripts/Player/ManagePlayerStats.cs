using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagePlayerStats : MonoBehaviour
{
    public float maxValue = 100;
    public float hpRefill = 5;

    [Header("Hunger")]
    public float hungerDrain = 0.3f; //use fractions (1 will be max)
    public Image hungerBar;
    public bool hDebuff1 = false;
    public bool hDebuff2 = false;
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
    public float currentHunger, currentHealth, currentThirst, currentExposure, currentFatigue, currentStamina;

    public TemperatureManager tempManager;

    public SleepMenuScript sleepUI;
    public GameObject craftingUI;
    public GameObject savingUI;
    public Inventory playerInv;

    public bool atfire = false;
    public int targetID = 35;
    public int replacementID = 19;

    // Use this for initialization
    void Start()
    {
        tempManager = this.gameObject.GetComponent<TemperatureManager>();
        currentHunger = maxValue;
        currentThirst = maxValue;
        currentFatigue = maxValue;
        currentStamina = maxValue;
        playerInv = GameObject.FindObjectOfType<Canvas>().GetComponentInChildren<Inventory>(true);
    }

    // Update is called once per frame
    void Update()
    {
        ManageDecreaseOverTime();
        ManageIncreaseOverTime();
        HandleBarDisplays();
        //ProximityField();
        if (atfire == false)
        {
            currentExposure = tempManager.currentTemperature + exposureRating; //This will change when adding clothing

        }
        else
        {
            currentExposure = 60;
        }
    }

    public void ManageDecreaseOverTime()
    {
        this.gameObject.GetComponent<Health>().curHealth += Time.deltaTime * hpRefill;

        currentHunger -= Time.deltaTime * hungerDrain;
        if (currentHunger < 0)
        {
            currentHunger = 0;
        }

        if (currentHunger <= (maxValue / 2))
        {
            hDebuff1 = true;
        }
        else if (currentHunger <= (maxValue / 4) && currentHunger >= (maxValue / 2))
        {
            hDebuff2 = true;
        }
        else
        {
            hDebuff1 = false;
            hDebuff2 = false;
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
        else
        {
            tDebuff1 = false;
            tDebuff2 = false;
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
        else
        {
            fDebuff1 = false;
            fDebuff2 = false;
        }
        if (currentFatigue <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }

    public void ManageIncreaseOverTime()
    {
        //Handle Stamina refill when below 100 and make sure it stays within 0-100
        if (currentStamina < maxValue)
        {
            currentStamina += Time.deltaTime * staminaRefill;
        }

        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
        else if (currentStamina > maxValue)
        {
            currentStamina = maxValue;
        }

        //Change Color based on usability
        if (currentStamina == 0)
        {
            this.GetComponent<PlayerController>().canSprint = false;
            //wait till 20% full - turn bar red
            if (currentStamina <= maxValue / 5)
            {
                staminaBar.color = Color.red;
            }
        }

        if (currentStamina <= maxValue / 5)
        {
            staminaBar.color = Color.red;
        }
        else if (currentStamina > maxValue / 5)
        {
            staminaBar.color = Color.green;
            this.GetComponent<PlayerController>().canSprint = true;

        }

    }

    public void Sleep()
    {
        currentFatigue = maxValue;
        currentHunger -= 15;
        currentThirst -= 20;
    }

    public void HandleDebuffs()
    {
        float drainValueOne = .2f;
        float drainValueTwo = .3f;

        if (hDebuff1)
        {
            fatigueDrain = fatigueDrain + drainValueOne; //this may be changed later
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

    public void HandleReplinishment(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Hunger")
            {
                if ((currentHunger + item.itemAttributes[i].attributeValue) > maxValue)
                    currentHunger = maxValue;
                else
                    currentHunger += item.itemAttributes[i].attributeValue;
            }

            if (item.itemAttributes[i].attributeName == "Thirst")
            {
                if ((currentThirst + item.itemAttributes[i].attributeValue) > maxValue)
                    currentThirst = maxValue;
                else
                    currentThirst += item.itemAttributes[i].attributeValue;
            }

            if (item.itemAttributes[i].attributeName == "Health")
            {
                if ((this.gameObject.GetComponent<Health>().curHealth + item.itemAttributes[i].attributeValue) > maxValue)
                    this.gameObject.GetComponent<Health>().curHealth = maxValue;
                else
                    this.gameObject.GetComponent<Health>().curHealth += item.itemAttributes[i].attributeValue;
            }
        }
    }

    public void HandleBarDisplays()
    {
        thirstBar.fillAmount = currentThirst / maxValue;
        hungerBar.fillAmount = currentHunger / maxValue;
        fatigueBar.fillAmount = currentFatigue / maxValue;
        exposureSlider.value = currentExposure / maxValue;
        staminaBar.fillAmount = currentStamina / maxValue;
        //healthBarLeft.fillAmount = currentHealth / maxValue;
        //healthBarRight.fillAmount = currentHealth / maxValue;

        //HP management has moved to the Health script
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<proxTarget>())
        {
            sleepUI.gameObject.SetActive(false);
            atfire = false;
            GameObject.FindWithTag("Canvas").GetComponent<UIToggle>().windows.Remove(craftingUI);
            craftingUI.gameObject.SetActive(false);
            GameObject.FindWithTag("Canvas").GetComponent<UIToggle>().windows.Remove(savingUI);
            savingUI.gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<proxTarget>())
        {
            if (other.GetComponent<proxTarget>().Type == proxTarget.targetType.Fire)
            {
                atfire = true;
                    GameObject.FindWithTag("Canvas").GetComponent<UIToggle>().windows.Add(craftingUI);
                    GameObject.FindWithTag("Canvas").GetComponent<UIToggle>().windows.Add(savingUI);

            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<proxTarget>())
        {
            if (other.GetComponent<proxTarget>().Type == proxTarget.targetType.Bed)
            {
                Debug.Log("bed here");
                if (Input.GetButtonDown("Interact"))
                {
                    sleepUI.ToggleUI();
                }
            }

            if (other.GetComponent<proxTarget>().Type == proxTarget.targetType.water)
            {
                Debug.Log("Water here");
                if (Input.GetButtonDown("Interact"))
                {
                    playerInv.updateItemList();

                    foreach (Item item in playerInv.ItemsInInventory)
                    {
                        Debug.Log("searching");
                        if (item.itemID == targetID)
                        {
                            Debug.Log("Found");
                            item.itemValue--;
                            if (item.itemValue <= 0)
                            {
                                playerInv.deleteItemFromInventoryWithGameObject(item);
                            }
                            //playerInv.addItemToInventory(replacementID, 1);
                            //playerInv.updateItemList();
                            //playerInv.stackableSettings();

                            bool check = playerInv.checkIfItemAllreadyExist(replacementID, 1);
                            if (check)
                            {
                                playerInv.updateItemList();
                            }
                            else if (playerInv.ItemsInInventory.Count < (playerInv.width * playerInv.height))
                            {
                                playerInv.addItemToInventory(replacementID, 1);
                                playerInv.updateItemList();
                                playerInv.stackableSettings();
                            }
                        }
                    }
                }
            }
        }
    }
}
