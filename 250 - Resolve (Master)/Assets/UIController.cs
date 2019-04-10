using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public string levelName;

    private LevelController levelController;
    private LevelData levelData;

    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        levelData = GameObject.FindGameObjectWithTag("DataManager").GetComponent<LevelData>();
    }

    // Update is called once per frame
    void Update()
    {
        levelName = levelController.levelName;
    }

    public void SavelevelButton()
    {
        levelController.levelName = levelName;
        levelData.SaveLevel(levelName);
        print("The game has been saved");
    }

    public void LoadLevelButton()
    {
        levelData.LoadLevel(levelName);
        print("The game has been loaded");
    }
}
