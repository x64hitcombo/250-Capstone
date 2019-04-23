using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DetermineLoad : MonoBehaviour
{
    public int load = 0; //1 = load; 0 = don't load

    private void Start()
    {
        load = PlayerPrefs.GetInt("load", 0);
    }

    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("load", load);
    }

    public void Load()
    {
        load = 1;
    }

    public void NoLoad()
    {
        load = 0;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("aiTestScene");
    }
}
