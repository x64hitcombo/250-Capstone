using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Text;

public class LevelData : MonoBehaviour
{
    public GameObject player;

    DataHandler dataHandler;
    LevelController levelController;

	// Use this for initialization
	void Start ()
    {
        string[] fileNames = System.IO.Directory.GetFiles(".", "*.lvl");

        StringBuilder sb = new StringBuilder();
        foreach (string fileName in fileNames)
        {
            sb.AppendLine(fileName);
        }
        print(sb.ToString());

        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadScene()
    {
        //SceneManager.LoadScene("SampleScene");
    }

    public void LoadLevel(string levelName)
    {
        //SceneManager.LoadScene("SampleScene");
        DataHandler level = DataHandler.LoadFromFile(levelName + ".lvl");
        levelController.levelName = level.levelName;
        player.transform.position = level.PlayerPosition;

        ManagePlayerStats mps = player.GetComponent<ManagePlayerStats>();
        mps.currentHunger = level.PlayerHunger;
        mps.currentThirst = level.PlayerThirst;
        mps.currentExposure = level.PlayerExposure;
        mps.currentFatigue = level.PlayerFatigue;
        mps.currentStamina = level.PlayerStamina;
    }

    public void SaveLevel(string levelName)
    {
        DataHandler level = new DataHandler();
        
        //Translation from game object data into level data
        level.levelName = levelController.levelName;

        //Player Data
        level.PlayerPosition = levelController.playerPosition;
        level.PlayerHunger = levelController.playerHunger;
        level.PlayerThirst = levelController.playerThirst;
        level.PlayerStamina = levelController.playerStamina;
        level.PlayerFatigue = levelController.playerFatigue;
        level.PlayerExposure = levelController.playerExposure;

        //Puzzle Completion
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("PuzzleManager"))
        {
            PuzzleManagerData puzzleManager = new PuzzleManagerData();
            puzzleManager.currentObject = puzzle;
            //puzzleManager.isCompleted = puzzle.GetComponent<PuzzleMaster>().complete;
        }

        //Current Time
        MainTimeScript mainTimeScript = GameObject.FindGameObjectWithTag("TimeController").GetComponent<MainTimeScript>();
        level.CurrentTime = levelController.currentTime;

        //Inventory


        level.SaveToFile(level.levelName + ".lvl");
    }
}

[Serializable]
public class PuzzleManagerData
{
    public GameObject currentObject;
    public bool isCompleted;
}

public class InventoryData
{
    //Find the variables for the inventory slots data contained within
}

[Serializable]
public class DataHandler
{
    public string levelName;

    public Vector3 playerPosition;
    public float playerStamina;
    public float playerHunger;
    public float playerThirst;
    public float playerFatigue;
    public float playerExposure;

    public float currentTime;

    public string GetLevelName()
    {
        return levelName;
    }

    public Vector3 PlayerPosition
    {
        get
        {
            return playerPosition;
        }
        set
        {
            playerPosition = value;
        }
    }

    public float PlayerStamina
    {
        get
        {
            return playerStamina;
        }
        set
        {
            playerStamina = value;
        }
    }

    public float PlayerHunger
    {
        get
        {
            return playerHunger;
        }
        set
        {
            playerHunger = value;
        }
    }

    public float PlayerThirst
    {
        get
        {
            return playerThirst;
        }
        set
        {
            playerThirst = value;
        }
    }

    public float PlayerFatigue
    {
        get
        {
            return playerFatigue;
        }
        set
        {
            playerFatigue = value;
        }
    }

    public float PlayerExposure
    {
        get
        {
            return playerExposure;
        }
        set
        {
            playerExposure = value;
        }
    }

    public float CurrentTime
    {
        get
        {
            return currentTime;
        }
        set
        {
            currentTime = value;
        }
    }

    public void SaveToFile(string fileName)
    {
        System.IO.File.WriteAllText(fileName, JsonUtility.ToJson(this, true));
        MonoBehaviour.print(System.IO.Directory.GetCurrentDirectory());
    }

    public static DataHandler LoadFromFile(string fileName)
    {
        return JsonUtility.FromJson<DataHandler>(System.IO.File.ReadAllText(fileName));
    }
}
