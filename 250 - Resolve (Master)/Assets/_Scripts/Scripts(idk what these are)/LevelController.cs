using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public string levelName;
    public GameObject player;
    public Vector3 playerPosition;
    public float playerStamina;
    public float playerHunger;
    public float playerThirst;
    public float playerFatigue;
    public float playerExposure;

    private ManagePlayerStats mps;

    // Use this for initialization
    void Start ()
    {
        mps = player.GetComponent<ManagePlayerStats>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerPosition = player.transform.position;
        playerStamina = mps.currentStamina;
        playerHunger = mps.currentHunger;
        playerThirst = mps.currentThirst;
        playerFatigue = mps.currentFatigue;
        playerExposure = mps.currentExposure;
	}
}
