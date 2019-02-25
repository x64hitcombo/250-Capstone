using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleObjectTemperature : MonoBehaviour
{
    public bool hot = false;
    public bool cold = false;
    public bool neither = true;

    public bool changeMaterialOnTemp = false;
    public bool changeColorOnTemp = false;

    private Material defaultMaterial;
    public Material hotMaterial;
    public Color hotColor;
    public Material coldMaterial;
    public Color coldColor;

    //Only needs to be seen for other scripts not in the inspector
    [HideInInspector]
    public bool changeColor = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeColor)
        {
            SetMaterialColorForTemperature();
        }
    }

    public void SetMaterialColorForTemperature()
    {
        if (changeColorOnTemp)
        {
            if (hot)
            {
                this.GetComponent<Renderer>().material.color = hotColor;
            }
            else if (cold)
            {
                this.GetComponent<Renderer>().material.color = coldColor;
            }
            else if (neither)
            {
                this.GetComponent<Renderer>().material = defaultMaterial;
            }
            changeColor = false;
        }
        else if (changeMaterialOnTemp)
        {
            if (hot)
            {
                this.GetComponent<Renderer>().material = hotMaterial;
            }
            else if (cold)
            {
                this.GetComponent<Renderer>().material = coldMaterial; 
            }
            else if (neither)
            {
                this.GetComponent<Renderer>().material = defaultMaterial;
            }
            changeColor = false;
        }
    }
}
