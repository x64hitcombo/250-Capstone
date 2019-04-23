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
    public LevelController levelController;

    public DetermineLoad determineLoad;

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

        if (determineLoad.load == 1)
        {
            LoadLevel(levelController.levelName);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadScene()
    {
        determineLoad.load = 1;
        SceneManager.LoadScene("aiTestScene");
    }

    public void LoadLevel(string levelName)
    {
        DataHandler level = DataHandler.LoadFromFile(levelController.levelName + ".lvl");
        levelController.levelName = level.levelName;
        player.transform.position = level.PlayerPosition;

        ManagePlayerStats mps = player.GetComponent<ManagePlayerStats>();
        mps.currentHunger = level.PlayerHunger;
        mps.currentThirst = level.PlayerThirst;
        mps.currentExposure = level.PlayerExposure;
        mps.currentFatigue = level.PlayerFatigue;
        mps.currentStamina = level.PlayerStamina;

        MainTimeScript mainTimeScript = GameObject.FindGameObjectWithTag("TimeController").GetComponent<MainTimeScript>();
        if (mainTimeScript != null)
        {
            levelController.currentTime = level.CurrentTime;
        }

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("PuzzleMaster"))
        {
            foreach (PuzzleManagerData pmd in level.puzzles)
            {
                if (puzzle == pmd.currentObject)
                {
                    if (pmd.isCompleted)
                    {
                        puzzle.GetComponent<PuzzleMaster>().complete = true;
                    }
                    else if (!pmd.isCompleted)
                    {
                        puzzle.GetComponent<PuzzleMaster>().complete = false;
                    }
                }
            }
        }

        determineLoad.load = 0;
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
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("PuzzleMaster"))
        {
            PuzzleManagerData newPuzzle = new PuzzleManagerData();
            newPuzzle.currentObject = puzzle;
            newPuzzle.isCompleted = puzzle.GetComponent<PuzzleMaster>().complete;
            level.puzzles.Add(newPuzzle);
        }

        //Current Time
        MainTimeScript mainTimeScript = GameObject.FindGameObjectWithTag("TimeController").GetComponent<MainTimeScript>();
        if (mainTimeScript != null)
        {
            level.CurrentTime = levelController.currentTime;
        }

        //Inventory
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            print("Found an item to save");
            InventoryManagerData newItem = new InventoryManagerData();
            newItem.slotNum = item.transform.parent.name;
            newItem.slotItem = item;
            level.inventoryData.Add(newItem);
        }

        level.SaveToFile(level.levelName + ".lvl");
    }
}

[Serializable]
public class PuzzleManagerData
{
    public GameObject currentObject;
    public bool isCompleted;
}

public class InventoryManagerData
{
    //Find the variables for the inventory slots data contained within
    public string slotNum;
    public GameObject slotItem;
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

    public List<PuzzleManagerData> puzzles = new List<PuzzleManagerData>();
    public List<InventoryManagerData> inventoryData = new List<InventoryManagerData>();

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
        MonoBehaviour.print("Save Complete!");
    }

    public static DataHandler LoadFromFile(string fileName)
    {
        return JsonUtility.FromJson<DataHandler>(System.IO.File.ReadAllText(fileName));
    }
}
